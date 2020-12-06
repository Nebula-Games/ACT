using ACT.Core.Extensions;
using ACT.Core.Types.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.ACTConsole;
using Newtonsoft.Json.Bson;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ACT.Core.Windows.Console
{

    public class ACTMenuSystemInitializationSettings
    {
        public System.ConsoleColor BackgroundColor = ConsoleColor.Black;
        public System.ConsoleColor ForegroundColor = ConsoleColor.White;
        public int Columns = 100;
        public int Rows = 40;
        public string ACTMenuFileFullPath_Override = "";
        public bool? RequireEnterToSelect_Override = null;
        public bool AutoRunMenu = false;
    }

    public static class ACTMenuSystem
    {
        #region Static Events

        /// <summary>   Event queue for all listeners interested in CustomMenuClicked events. </summary>
        public static event ACT.Core.Delegates.OnMenuItemClick CustomMenuClicked;

        public static Dictionary<string, I_ConsoleItemClicked> PluginCache = new Dictionary<string, I_ConsoleItemClicked>();

        public static void ClearCache()
        {

        }

        public static string LastMenuIdentificationHash = "";


        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Values that represent menu system modes. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/19/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------
        private enum MenuSystemMode
        {
            RunningMode = 1,
            AdminMode = 2,
            DebugMode = 3
        }

        /// <summary>
        /// Admin Functions
        /// </summary>
        private enum AdminFunctions
        {
            CreateNewMenu = 1,
            DeleteMenu = 2,
            ChangePassword = 3,
            ChangeDebugPassword = 4,
            Other = 999
        }

        private enum DebugFunctions
        {
            info = 1,
            other = 999
        }

        #region Static Properties / Variables

        #region Private Properties 

        /// <summary> Loaded Menu Data -- FQN = ACT.Core.Types.Console.ACTMenuData</summary>
        private static ACTMenuData _ACTMenuData = new ACTMenuData();

        /// <summary> Indicates if the Menu Data Was Loaded </summary>
        private static bool MenuDataLoaded = false;

        /// <summary> Request the Quit Trigger </summary>
        private static bool IsRunning = true;

        private static bool AdminOnlyMenus = false;

        /// <summary>   </summary>
        private static int _HelpCount = 0;

        /// <summary>   The current mode. </summary>
        private static MenuSystemMode _CurrentMode = MenuSystemMode.RunningMode;

        /// <summary>   Identifier for the last menu. </summary>
        private static int _LastMenuId = 0;

        /// <summary>   Identifier for the current menu. </summary>
        private static int _CurrentMenuId = 0;

        /// <summary>   The plugins. </summary>
        private static Dictionary<string, I_ConsoleItemClicked> _Plugins = new Dictionary<string, I_ConsoleItemClicked>();
        #endregion

        /// <summary>   The loaded menu. </summary>
        public static ACT.Core.Types.Console.ACTSubMenu CurrentSelectedMenu { get; private set; } = null;

        #endregion

        #region Loading Methods

        /// <summary>
        /// Force The Current System To Stop  Reset The Data
        /// </summary>
        /// <param name="SaveState"></param>
        public static void ForceStop(bool SaveState = false)
        {
            IsRunning = false;
            MenuDataLoaded = false;
            _ACTMenuData = null;
            _CurrentMenuId = 0;
            _CurrentMode = MenuSystemMode.RunningMode;
            _LastMenuId = 0;
            CurrentSelectedMenu = null;
        }

        /// <summary>
        /// Initializes the Menu System with the Specified Parameters
        /// </summary>
        /// <param name="BackgroundColor"></param>
        /// <param name="ForegroundColor"></param>
        /// <param name="Columns"></param>
        /// <param name="Rows"></param>
        /// <param name="ACTMenuFileLocation"></param>
        /// <param name="RequireEnterForMenuSelect"></param>
        /// <param name="BaseHelpText"></param>
        public static void InitMenuSystem(ACTMenuSystemInitializationSettings InitSettings, ACT.Core.Delegates.OnMenuItemClick MenuHandler = null, bool AutoRun = false)
        {
            // Always Call Force Stop
            ForceStop(false);

            if (IsRunning) { throw new Exception("System Is Already Running.  Please Force Stop"); }

            MenuDataLoaded = false;
            System.Console.BackgroundColor = InitSettings.BackgroundColor;
            System.Console.ForegroundColor = InitSettings.ForegroundColor;
            if (InitSettings.Columns > System.Console.LargestWindowWidth) { System.Console.WindowWidth = System.Console.LargestWindowWidth - 5; }
            else { System.Console.WindowWidth = InitSettings.Columns; }

            if (InitSettings.Rows > System.Console.LargestWindowHeight) { System.Console.WindowHeight = System.Console.LargestWindowHeight - 2; }
            else { System.Console.WindowHeight = InitSettings.Rows; }

            //Todo Dynamic File Names Using Configuration File and Static Variable - Static Sontructor
            var _MenuLocation = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat().FindFileReturnPath("ACTConsole.json", true);
            if (InitSettings.ACTMenuFileFullPath_Override.NullOrEmpty() == false)
            {
                if (InitSettings.ACTMenuFileFullPath_Override.FileExists()) { LoadMenu(InitSettings.ACTMenuFileFullPath_Override.ReadAllText()); }
            }

            LoadMenu(_MenuLocation.FindAndReadFile("ACTConsole.json", true));

            _CurrentMenuId = 0;

            if (MenuHandler != null) { CustomMenuClicked += MenuHandler; }

            if (AutoRun) { RunMenu(); }
        }

        /// <summary>
        /// Initializes the Menu System with the Specified Parameters and AutoRuns the Menu Engine
        /// </summary>
        /// <param name="BackgroundColor"></param>
        /// <param name="ForegroundColor"></param>
        /// <param name="Columns"></param>
        /// <param name="Rows"></param>
        /// <param name="ACTMenuFileLocation"></param>
        /// <param name="RequireEnterForMenuSelect"></param>
        /// <param name="BaseHelpText"></param>
        public static void InitMenuSystemAndRun(ACTMenuSystemInitializationSettings InitSettings, ACT.Core.Delegates.OnMenuItemClick MenuHandler = null)
        {
            InitMenuSystem(InitSettings, MenuHandler, true);
        }

        /// <summary>
        /// Load the Menu from the JSON Configuration Data
        /// </summary>
        /// <param name="JSONData"></param>
        public static void LoadMenu(string JSONData)
        {
            try
            {
                _ACTMenuData = ACT.Core.Types.Console.ACTMenuData.FromJson(JSONData);
                if (_ACTMenuData.SubMenus == null || _ACTMenuData.SubMenus.Count() == 0)
                {
                    throw new InvalidDataException("", null);
                }
            }
            catch (Exception ex) { throw new InvalidDataException("", ex); }

            MenuDataLoaded = true;
        }

        #endregion

        #region Draw and Main Loop

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draw the CurrentMenu. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="MenuId">   . </param>
        ///-------------------------------------------------------------------------------------------------
        private static void Draw()
        {
            if (_CurrentMode == MenuSystemMode.AdminMode) { DrawAdminMode(); }
            else if (_CurrentMode == MenuSystemMode.DebugMode) { DrawDebugMode(); }

            if (AdminOnlyMenus == true)
            {
                foreach (var _MItem in CurrentSelectedMenu.SubMenus.Where(x => x.Isadminfeature == true))
                {
                    if (_ACTMenuData.VerboseDebugging) { System.Console.WriteLine(_MItem.Shortcut + ": " + _MItem.Text + " (" + _MItem.Id.ToString() + ")"); }
                    else { System.Console.WriteLine(_MItem.Shortcut.ToString() + ": " + _MItem.Text); }
                }
            }
            else
            {
                foreach (var _MItem in CurrentSelectedMenu.SubMenus.Where(x => x.Isadminfeature == false))
                {
                    if (_ACTMenuData.VerboseDebugging) { System.Console.WriteLine(_MItem.Shortcut + ": " + _MItem.Text + " (" + _MItem.Id.ToString() + ")"); }
                    else { System.Console.WriteLine(_MItem.Shortcut.ToString() + ": " + _MItem.Text); }
                }
            }

            if (_CurrentMenuId != 0) { System.Console.WriteLine("Go Back (leave blank hit enter)"); }
            if (_CurrentMode == MenuSystemMode.AdminMode) { System.Console.WriteLine("~new : Create New Menu Item Here"); }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draw admin portal. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/21/2019. </remarks>
        ///
        /// <param name="DrawHeader">   (Optional) True to draw header. </param>
        ///-------------------------------------------------------------------------------------------------
        private static void DrawAdminMode()
        {
            System.Console.WriteLine("*****         ADMIN FUNCTIONS         *****");
            System.Console.WriteLine("");
            System.Console.WriteLine(" ~cap: Change Admin Password");
            System.Console.WriteLine(" ~cdp: Change Debug Password");
            System.Console.WriteLine(" ~cmi: Create Menu Item");
            System.Console.WriteLine(" ~dmi: Delete Menu Item");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Draw Debug portal. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/21/2019. </remarks>
        ///
        /// <param name="DrawHeader">   (Optional) True to draw header. </param>
        ///-------------------------------------------------------------------------------------------------
        private static void DrawDebugMode()
        {
            System.Console.WriteLine("*****         DEBUG FUNCTIONS         *****");
            System.Console.WriteLine("");
            System.Console.WriteLine(" ~dsmd: Debug Show Menu Details");

        }

        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Appends to log. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="Content">      The content. </param>
        /// <param name="NewFilePath">  (Optional) Full pathname of the new file. </param>
        ///-------------------------------------------------------------------------------------------------
        public static void AppendToLog(string Content, string NewFilePath = "")
        {
            //TODO Implement  _ACTMenuData.LoggerPluginInformation

            string _FilePath = "";

            if (NewFilePath == "" || NewFilePath.FileExists() == false)
            {
                _FilePath = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".log";
            }

            System.IO.File.AppendAllText(_FilePath, Content + Environment.NewLine + "----- " + DateTime.Now.ToString() + " -----");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>  Captures the selection. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <returns>   The selection. as a List of Characters
        ///     { "back" } = back;
        ///     null = exit menu right away;
        ///     
        /// </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string GetSelection()
        {
            // Manages exit - back - help
            start:

            System.Console.Write(":?> ");
            string _SelectionText = "";

            if (_ACTMenuData.RequireEnterKey) { _SelectionText = System.Console.ReadLine().Trim().ToLower(); }
            else { _SelectionText = System.Console.ReadKey().KeyChar.ToString().Trim().ToLower(); }

            /// HARDCODED EXIT TEXT
            if (_SelectionText == "exit") { return null; }
            if (_SelectionText == "back")
            {
                if (_CurrentMenuId == 0)
                {
                    var _BC = System.Console.BackgroundColor.ToColor();
                    ACT.Core.Windows.Console.Helper.WriteLine("You are at the Main Menu Level.", _BC.GetContrast(true).ToConsoleColor(), System.Console.BackgroundColor);
                }
                else { return "back"; }
            }

            if (_ACTMenuData.LogAllToConsole)
            {
                string _ToFile = _SelectionText;

                try { if (_ACTMenuData.EncryptionString.NullOrEmpty() == false) { _ToFile = _ToFile.EncryptString(_ACTMenuData.EncryptionString); } }
                catch (Exception ex) { ACT.Core.Helper.ErrorLogger.VLogError("ACTSystem.Console.MenuSystem", "Error Using Logger Encryption: " + ex.Message, ex, Enums.ErrorLevel.Severe); }

                AppendToLog(_ToFile, "");
            }

            /// Help Easter Egg
            if (_ACTMenuData.MenuTitle.Contains("ACT"))
            {
                if (_SelectionText == "?" || _SelectionText == "help")
                {
                    _HelpCount++;
                    if (_HelpCount > 50)
                    {
                        System.Console.WriteLine("I have already answered your question.");
                        goto start;
                    }
                    else if (_HelpCount > 100)
                    {
                        System.Console.WriteLine("Go fly A KITE! ;)");
                        goto start;
                    }
                    else if (_HelpCount > 1000)
                    {
                        System.Console.WriteLine("Fang Son of Great Fang, Wait For Reply, The Laws of the - Welcome To The Future. Present That at the door.");
                        goto start;
                    }
                    else
                    {
                        System.Console.WriteLine(_ACTMenuData.HelpText);
                        goto start;
                    }
                }
            }

            _HelpCount = 0;

            return _SelectionText;
        }
        private static bool BlockAdminLogin = false;

        /// <summary>
        /// Process Admin Login
        /// </summary>
        /// <returns></returns>
        private static bool AdminLogin()
        {
            if (BlockAdminLogin == false) { return false; }

            if (_CurrentMode != MenuSystemMode.AdminMode)
            {
                if (_ACTMenuData.Adminpassword.NullOrEmpty()) { return true; }
                else
                {
                    var _AdminTryCount = 0;

                    TryAdminLogin:

                    if (_AdminTryCount > 4)
                    {
                        Helper.WriteLine("Error - System Admin Login Blocked!", ConsoleColor.Red);
                        BlockAdminLogin = true;
                    }

                    System.Console.Clear();

                    System.Console.Write("Please Enter The Console Admin Password :> ");
                    string _PW = System.Console.ReadLine();
                    string _tPW = _PW.EncryptString();

                    if (_tPW == _ACTMenuData.Adminpassword)
                    {
                        Helper.WriteLine("Password Correct!", ConsoleColor.Red, System.Console.BackgroundColor);
                        Helper.WriteLine("Press Any Key To Enter The Admin Menu", ConsoleColor.Red, System.Console.BackgroundColor);
                        System.Console.ReadKey();
                        return true;
                    }
                    else
                    {
                        Helper.WriteLine("Invalid Password!! Press Any Key To Continue. Try: " + _AdminTryCount.ToString(), ConsoleColor.Red, System.Console.BackgroundColor);
                        System.Console.ReadKey();
                        _AdminTryCount++;
                        goto TryAdminLogin;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Admin Functions
        /// </summary>
        /// <param name="SelectionString"></param>
        /// <param name="AFunc"></param>
        private static void RunAdminFunctions(string SelectionString, AdminFunctions AFunc)
        {
            if (AFunc == AdminFunctions.CreateNewMenu)
            {
                System.Console.WriteLine("Please Enter the Menu Text.");
                System.Console.Write(":> ");
                string _MenuText = System.Console.ReadLine();
                System.Console.WriteLine("Please Enter the Menu Shortcut.");
                System.Console.Write(":> ");
                string _MenuShortcut = System.Console.ReadLine();
                System.Console.WriteLine("Please Enter the Menu Callback Method.");
                System.Console.Write(":> ");
                string _MenuCallbackMethod = System.Console.ReadLine();
                System.Console.WriteLine("Please Enter the Menu Plugin DLL (options).");
                System.Console.Write(":> ");
                string _MenuCallbackPluginDLL = System.Console.ReadLine();
                System.Console.WriteLine("Please Enter the Menu Plugin Class Name.");
                System.Console.Write(":> ");

                string _MenuCallbackClassName = System.Console.ReadLine();

                ACTSubMenu _newMenu = new ACTSubMenu();
                _newMenu.Text = _MenuText;
                _newMenu.Shortcut = _MenuShortcut;
                _newMenu.Executecallback = _MenuCallbackMethod;
                _newMenu.Dll = _MenuCallbackPluginDLL;
                _newMenu.FullClassName = _MenuCallbackClassName;
                _newMenu.Isadminfeature = false;
                _newMenu.Isdebugfeature = false;

                CurrentSelectedMenu.SubMenus.Add(_newMenu);

                System.Console.WriteLine("Menu Added!  You should now see the menu.  Press Any Key To Continue.");
                System.Console.ReadKey();
            }
            else if (AFunc == AdminFunctions.DeleteMenu)
            {
                startdeleteadminmenu:
                System.Console.WriteLine("Please Enter the Menu Key To Delete.");
                System.Console.Write(":> ");
                string _MenuKey = System.Console.ReadLine().Trim().ToLower();

                if (CurrentSelectedMenu.SubMenus.Exists(x => x.Shortcut.ToLower() == _MenuKey))
                {
                    CurrentSelectedMenu.SubMenus.Remove(CurrentSelectedMenu.SubMenus.First(x => x.Shortcut.ToLower() == _MenuKey));
                    System.Console.WriteLine("Menu Deleted; Press Any Key To Continue.");
                    System.Console.ReadKey();
                    return;
                }
                else { System.Console.WriteLine("Error Locating Item: Press Any Key To Continue"); goto startdeleteadminmenu; }
            }
            else if (AFunc == AdminFunctions.ChangePassword)
            {
                System.Console.WriteLine("Please Enter a New Admin Password.");
                System.Console.Write(":> ");
                string _PasswordKey = System.Console.ReadLine();
                _ACTMenuData.Adminpassword = _PasswordKey.EncryptString().ToBase64();
                System.Console.WriteLine("Password Saved! Press Any Key To Continue.");
                System.Console.ReadKey();
            }
            else if (AFunc == AdminFunctions.ChangeDebugPassword)
            {
                System.Console.WriteLine("Please Enter a New Debug Password.");
                System.Console.Write(":> ");
                string _PasswordKey = System.Console.ReadLine();
                _ACTMenuData.Debugpassword = _PasswordKey.EncryptString().ToBase64();
                System.Console.WriteLine("Debug Password Saved! Press Any Key To Continue.");
                System.Console.ReadKey();
            }
            else if (AFunc == AdminFunctions.Other)
            {
                if (SelectionString == "afo")
                {

                }
            }
        }

        /// <summary>
        /// Debug Functions
        /// </summary>
        /// <param name="SelectionString"></param>
        /// <param name="AFunc"></param>
        private static void RunDebugFunctions(string SelectionString, DebugFunctions AFunc)
        {
            if (AFunc == DebugFunctions.info)
            {
                System.Console.WriteLine("Info Here TODO.. Press Any Key To Continue.");
                System.Console.ReadKey();
            }
            else if (AFunc == DebugFunctions.other)
            {
                System.Console.WriteLine("OTHER TODO.  Press Any Key To Continue.");
                System.Console.ReadKey();
            }
        }

        /// <summary>
        ///  Draws the Admin Help Screen
        /// </summary>
        private static void DrawAdminHelp()
        {
            System.Console.Clear();
            System.Console.WriteLine("Admin Functions -- Trigger Character ~");
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("create new menu item: (~new)");
            System.Console.WriteLine("create new menu item: (~delete)");
            System.Console.WriteLine("create new menu item: (~cap)");
            System.Console.WriteLine("create new menu item: (~cdp)");
            System.Console.WriteLine("");
            System.Console.WriteLine("Press Any Key To Continue");
            System.Console.ReadKey();
        }

        /// <summary>
        ///  Draws the Debug Help Screen
        /// </summary>
        private static void DrawDebugHelp()
        {
            System.Console.Clear();
            System.Console.WriteLine("Debug Functions -- Trigger Character `");
            System.Console.WriteLine("-----------------------------------------");
            //System.Console.WriteLine("create new menu item: (~new)");
            //System.Console.WriteLine("create new menu item: (~delete)");
            //System.Console.WriteLine("create new menu item: (~cap)");
            System.Console.WriteLine("show debug info: (`info)");
            System.Console.WriteLine("");
            System.Console.WriteLine("Press Any Key To Continue");
            System.Console.ReadKey();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the menu operation. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------
        public static void RunMenu()
        {
            // Exit the Loop When The Request Quit is True
            while (IsRunning == false)
            {
                System.Console.Clear();

                Draw();

                var _SelectionText = GetSelection();

                if (_SelectionText == null) { IsRunning = false; return; }

                // check For Admin Mode Request
                if (_SelectionText == "admin login") { if (AdminLogin()) { _CurrentMode = MenuSystemMode.AdminMode; continue; } }
                else if (_SelectionText == "admin logout") { _CurrentMode = MenuSystemMode.RunningMode; continue; }

                // Process Sub Menu Collections
                var _SubMenu = CurrentSelectedMenu.SubMenus.Where(xi => xi.Shortcut.ToLower() == _SelectionText).FirstOrDefault();

                /// EXECUTE THE MENU CALLBACK
                if (_SubMenu.Executecallback.NullOrEmpty() == false)
                {
                    if (_SubMenu.Dll.NullOrEmpty() == true)
                    {
                        CustomMenuClicked?.Invoke(_SubMenu.Executecallback, _SubMenu.Id.ToString(), _SelectionText.Select(x => x.ToString()).ToArray());
                    }
                    else
                    {
                        string _DLLHash = (_SubMenu.Dll.EnsureEndsWith(".dll") + _SubMenu.FullClassName).ToBase64();
                        if (PluginCache.ContainsKey(_DLLHash))
                        {
                            PluginCache[_DLLHash].ExecuteMenuCommand(_SubMenu.Executecallback, _SelectionText.Select(x => x.ToString()).ToArray());
                        }
                        else
                        {
                            // Check CACHE
                            var _PluginDll = (ACT.Core.Interfaces.ACTConsole.I_ConsoleItemClicked)ACT.Core.Dynamic.Library.LoadDLL<ACT.Core.Interfaces.ACTConsole.I_ConsoleItemClicked>(_SubMenu.Dll.EnsureEndsWith(".dll"), _SubMenu.FullClassName, null);
                            _PluginDll.ExecuteMenuCommand(_SubMenu.Executecallback, _SelectionText.Select(x => x.ToString()).ToArray());
                            PluginCache.Add(_DLLHash, _PluginDll);
                        }
                    }
                }

                if (_SelectionText == "admin help") { DrawAdminHelp(); continue; }
                if (_SelectionText == "debug help") { DrawDebugHelp(); continue; }


                if (_CurrentMode == MenuSystemMode.AdminMode && _SelectionText.StartsWith("~"))
                {
                    if (_SelectionText.StartsWith("~cap")) { RunAdminFunctions(_SelectionText, AdminFunctions.ChangePassword); }
                    else if (_SelectionText.StartsWith("~cdp")) { RunAdminFunctions(_SelectionText, AdminFunctions.ChangeDebugPassword); }
                    else if (_SelectionText.StartsWith("~new")) { RunAdminFunctions(_SelectionText, AdminFunctions.CreateNewMenu); }
                    else if (_SelectionText.StartsWith("~delete")) { RunAdminFunctions(_SelectionText, AdminFunctions.DeleteMenu); }
                    else { RunDebugFunctions(_SelectionText, DebugFunctions.other); }
                }
                else if (_CurrentMode == MenuSystemMode.DebugMode && _SelectionText.StartsWith("`"))
                {
                    if (_SelectionText.StartsWith("`info")) { RunDebugFunctions(_SelectionText, DebugFunctions.info); }
                    else { RunDebugFunctions(_SelectionText, DebugFunctions.other); }
                }
                else
                {
                    // Move To Sub Menu
                    _LastMenuId = _CurrentMenuId;
                    _CurrentMenuId = _SubMenu.Id;
                    CurrentSelectedMenu = _SubMenu;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Request data. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="Prompt">   The prompt. </param>
        ///
        /// <returns>   A List&lt;string&gt; </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string RequestData(string Prompt)
        {
            System.Console.WriteLine(Prompt);
            return GetSelection();
        }
    }
}
