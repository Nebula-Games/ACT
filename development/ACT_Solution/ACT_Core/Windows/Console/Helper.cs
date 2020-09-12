///-------------------------------------------------------------------------------------------------
// file:	Windows\Console\Helper.cs
//
// summary:	Implements the helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using ACT.Core.Extensions;


namespace ACT.Core.Windows.Console
{
    /// <summary>
    /// Console Helper Class.  Making your console development Much Easier
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Find a window by the window caption
        /// </summary>
        /// <param name="WindowCaption">Window Caption To Find</param>
        /// <returns>IntPtr</returns>
        public static IntPtr FindWindow(string WindowCaption)
        {
            return ACT.Core.Windows.User32.ExternMethods.FindWindowByCaption(IntPtr.Zero, WindowCaption);
        }

        /// <summary>
        /// Show ConsoleWindow based on the IntPtr
        /// </summary>
        /// <param name="Handle">IntPtr</param>
        /// <returns>bool</returns>
        public static bool ShowConsoleWindow(IntPtr Handle) { return ACT.Core.Windows.User32.ExternMethods.ShowWindow(Handle); }

        /// <summary>
        /// Hide the console window
        /// </summary>
        /// <param name="Handle"></param>
        /// <returns></returns>
        public static bool HideConsoleWindow(IntPtr Handle) { return ACT.Core.Windows.User32.ExternMethods.HideWindow(Handle); }

        /// <summary>
        /// Gets a Password using Masking
        /// </summary>
        /// <param name="FontColor"></param>
        /// <param name="Prompt"></param>
        /// <param name="PasswordChar"></param>
        /// <param name="IllegalCharacters"></param>
        /// <returns></returns>
        public static string GetPassword(System.ConsoleColor FontColor, string Prompt, string PasswordChar, List<string> IllegalCharacters)
        {
            string pass = "";
            var _OriginalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = FontColor;
            System.Console.Write(Prompt);
            ConsoleKeyInfo key;

            do
            {
                key = System.Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    System.Console.Write(PasswordChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        System.Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            System.Console.ForegroundColor = _OriginalColor;
            return pass;
        }

        /// <summary>
        /// Get various inputs from the console.
        /// </summary>
        /// <param name="Prompts">List of Prompts using Tuple (Key,Prompt)</param>
        /// <param name="ClearWindow">Clear the Window When Starting</param>
        /// <param name="BaseForegroundColor">Base ForegroundColor</param>
        /// <param name="BackgroundColor">Base BackgroundColor</param>
        /// <param name="PromptDisplayColors">Array of Display Colors based on Keys</param>
        /// <param name="RegularExpressionData">List of Regular Expressions to Test</param>
        /// <returns>Dictionary of Key/Values</returns>
        public static Dictionary<string, string> GetInputs(List<(string Key, string Prompt)> Prompts, bool ClearWindow, System.ConsoleColor BaseForegroundColor = ConsoleColor.White,
            System.ConsoleColor BackgroundColor = ConsoleColor.Black, List<(string Key, System.ConsoleColor DisplayColor)> PromptDisplayColors = null, List<(string key, string RegularExpression)> RegularExpressionData = null)
        {
            if (ClearWindow) { System.Console.Clear(); }

            Dictionary<string, string> _tmpReturn = new Dictionary<string, string>();
            System.Console.BackgroundColor = BackgroundColor;
            System.Console.ForegroundColor = BaseForegroundColor;

            foreach (var itm in Prompts)
            {
                if (PromptDisplayColors != null)
                {
                    if (PromptDisplayColors.Exists(x => x.Key == itm.Key)) { System.Console.ForegroundColor = PromptDisplayColors.First(x => x.Key == itm.Key).DisplayColor; }
                }

                System.Console.WriteLine(itm.Prompt);
                _tmpReturn.Add(itm.Key, System.Console.ReadLine().Trim());
                System.Console.ForegroundColor = BaseForegroundColor;
            }

            return _tmpReturn;
        }

        /// <summary>
        /// Asks the user tp Confirm the Choices by Typing an Option
        /// </summary>
        /// <param name="Prompt">Prompt to show the user</param>
        /// <param name="Choices">Array of Choices</param>
        /// <returns></returns>
        public static string ConfirmChoice(string Prompt, string[] Choices = null, bool CaseSensitive = false)
        {
            System.Console.Write(Prompt + " (");
            string _Choices = "";
            foreach (string choice in Choices)
            {
                _Choices += choice + ", ";
            }
            _Choices = _Choices.TrimEnd(", ") + " )";

            System.Console.Write(_Choices);
        getSelection:
            System.Console.WriteLine("");
            System.Console.Write(":> ");
            var _Choice = System.Console.ReadLine();

            if (Choices.ToList().Exists(x => x.ToLower() == _Choice.ToLower()) == false) { goto getSelection; }
            else { return _Choice; }
        }

        /// <summary>
        /// Gets a File Path
        /// </summary>
        /// <param name="FontColor"></param>
        /// <param name="Prompt"></param>
        /// <param name="FileExtensions"></param>
        /// <param name="TestOnlyDirectory"></param>
        /// <returns></returns>
        public static string GetFilePath(System.ConsoleColor FontColor, string Prompt, List<string> FileExtensions, bool TestOnlyDirectory = false)
        {
            string pass = "";
            var _OriginalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = FontColor;
            System.Console.Write(Prompt);

        getfile:

            System.Console.WriteLine(Prompt);
            System.Console.WriteLine("Hit enter without typing a file to exit and abort.");
            string _tmpReturn = ReadLine(FontColor);

            if (TestOnlyDirectory == false)
            {
                if (_tmpReturn.FileExists() == false)
                {
                    System.Console.WriteLine("Unable to find the specified file");
                    goto getfile;
                }
            }
            else
            {
                if (_tmpReturn.GetDirectoryFromFileLocation().DirectoryExists() == false)
                {
                    System.Console.WriteLine("Unable to find the specified path");
                    goto getfile;
                }
            }

            if (FileExtensions.TrueForAll(x => _tmpReturn.ToLower().EndsWith(x.ToLower())) == false)
            {
                System.Console.WriteLine("File Type Not Allowed.");
                goto getfile;
            }


            System.Console.ForegroundColor = _OriginalColor;
            return pass;
        }

        /// <summary>
        /// Gets a Password using Masking
        /// </summary>
        /// <param name="FontColor"></param>
        /// <param name="Prompt"></param>
        /// <param name="FileExtensions"></param>
        /// <param name="Create"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(System.ConsoleColor FontColor, string Prompt, bool Create = false)
        {
            string _tmpReturn = "";
            var _OriginalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = FontColor;
            System.Console.Write(Prompt);

        getfile:

            System.Console.WriteLine(Prompt);
            System.Console.WriteLine("Type \"exit\" or nothing to exit and abort.");
            _tmpReturn = ReadLine(FontColor).Trim();

            if (_tmpReturn.NullOrEmpty() || _tmpReturn.ToLower() == "exit") { return ""; }

            if (Create == true)
            {
                try { _tmpReturn.CreateDirectoryStructure(null); }
                catch(Exception ex)
                {
                    System.Console.WriteLine("Unable to create/find the specified path: " + ex.Message);
                    goto getfile;
                }
            }
            else
            {
                if (_tmpReturn.GetDirectoryFromFileLocation().DirectoryExists() == false)
                {
                    System.Console.WriteLine("Unable to find the specified path");
                    goto getfile;
                }
            }
            
            System.Console.ForegroundColor = _OriginalColor;
            return _tmpReturn.EnsureDirectoryFormat();
        }


        /// <summary>
        /// Read Line
        /// </summary>
        /// <param name="FontColor"></param>
        /// <param name="MaskInput"></param>
        /// <param name="MaskCharacter"></param>
        /// <returns></returns>
        public static string ReadLine(System.ConsoleColor FontColor = ConsoleColor.White, bool MaskInput = false, string MaskCharacter = "*")
        {
            string _tmpReturn = "";
            var _OriginalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = FontColor;
            ConsoleKeyInfo key;

            do
            {
                key = System.Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    _tmpReturn += key.KeyChar;
                    if (MaskInput) { System.Console.Write(MaskCharacter); }
                    else { System.Console.Write(key.KeyChar); }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _tmpReturn.Length > 0)
                    {
                        _tmpReturn = _tmpReturn.Substring(0, (_tmpReturn.Length - 1));
                        System.Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            System.Console.ForegroundColor = _OriginalColor;
            return _tmpReturn;
        }
    }
}
