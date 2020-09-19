using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ACT.Core.Serialization;
using System.Linq;
using ACT.Core.Extensions;
using OfficeOpenXml.DataValidation;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Types.Console
{
    public class ACTMenuData
    {

        [Flags()]
        private enum LoopAction
        {
            CheckValidity,
            PrintValidity,
            LogValidity
        }

        /// <summary> List of Current Saved Mapped SubMenu to Parent Menu </summary>
        /// <remarks> Key = SubMenuID, Value = ParentMenuPointer</remarks>
        [JsonIgnore()]
        private SortedDictionary<int, ACTSubMenu> _MappedSubMenuToParentMenu = new SortedDictionary<int, ACTSubMenu>();

        /// <summary> List of Current Saved Mapped SubMenu </summary>
        /// <remarks> Key = MenuID, Value = SubMenuPointer</remarks>
        [JsonIgnore()]
        private SortedDictionary<int, ACTSubMenu> _MappedSubMenuCache = new SortedDictionary<int, ACTSubMenu>();

        /// <summary> Used For Basic Menu Count</summary>
        [JsonIgnore()]
        private int _TotalMenuItemCount = 0;

        [JsonIgnore()]
        // TMP Counter
        private static int __tmpCounter = 0;

        [JsonIgnore()]
        public int TotalMenuItemCount { get { return _TotalMenuItemCount; } }


        /*              START JSON PROPERTIES AND METHODS                    */


        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsonSerializationSettings.ParseStringConverter))]
        public int Id { get; set; }


        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("menutitle", NullValueHandling = NullValueHandling.Ignore)]
        public string MenuTitle { get; set; }

        [JsonProperty("verbosedebugging", NullValueHandling = NullValueHandling.Ignore)]
        public bool VerboseDebugging = false;

        [JsonProperty("logalltoconsole", NullValueHandling = NullValueHandling.Ignore)]
        public bool LogAllToConsole = false;

        [JsonProperty("loggerplugininfo", NullValueHandling = NullValueHandling.Ignore)]
        public LoggerPluginInfo LoggerPluginInformation { get; set; }

        [JsonProperty("encryptionstring", NullValueHandling = NullValueHandling.Ignore)]
        public string EncryptionString { get; set; }

        [JsonProperty("shortcut", NullValueHandling = NullValueHandling.Ignore)]
        public string Shortcut { get; set; }

        [JsonProperty("executecallback", NullValueHandling = NullValueHandling.Ignore)]
        public string Executecallback { get; set; }

        [JsonProperty("adminpassword", NullValueHandling = NullValueHandling.Ignore)]
        public string Adminpassword { get; set; }

        [JsonProperty("debugpassword", NullValueHandling = NullValueHandling.Ignore)]
        public string Debugpassword { get; set; }

        [JsonProperty("dll", NullValueHandling = NullValueHandling.Ignore)]
        public string Dll { get; set; }

        [JsonProperty("menu", NullValueHandling = NullValueHandling.Ignore)]
        public List<ACTSubMenu> SubMenus { get; set; }

        [JsonProperty("requireenterkey", NullValueHandling = NullValueHandling.Ignore)]
        public bool RequireEnterKey { get; set; }

        [JsonProperty("helptext", NullValueHandling = NullValueHandling.Ignore)]
        public string HelpText { get; set; }

        /// <summary>
        /// Process Cache
        /// </summary>
        /// <param name="MenuItm"></param>
        /// <param name="ParentID"></param>
        public void ProcessCache(ACTSubMenu MenuItm, int ParentID)
        {
            if (ParentID > 0)
            {
                foreach (var itm in MenuItm.SubMenus)
                {
                    __tmpCounter++;
                    _MappedSubMenuCache.Add(itm.Id, itm);
                    _MappedSubMenuToParentMenu.Add(ParentID, itm);
                    ProcessCache(itm, itm.Id);
                }
            }
            else
            {
                __tmpCounter = 0;
                foreach (var itm in SubMenus)
                {
                    __tmpCounter++;
                    _MappedSubMenuCache.Add(itm.Id, itm);
                    _MappedSubMenuToParentMenu.Add(itm.Id, null);
                    ProcessCache(itm, itm.Id);
                }
            }
        }

        public static ACTMenuData FromJson(string jsonFileFullPath)
        {
            try
            {
                if (jsonFileFullPath.FileExists()) { throw new System.IO.FileNotFoundException(jsonFileFullPath); }
                string _jsonFileLocation = jsonFileFullPath.GetDirectoryFromFileLocation().EnsureDirectoryFormat();

                string json = jsonFileFullPath.ReadAllText();

                var _MenuDataObject = JsonConvert.DeserializeObject<ACTMenuData>(json, JsonSerializationSettings.Settings);
                _MenuDataObject.ProcessCache(null, 0);

                try
                {
                    if (_MenuDataObject.HelpText.StartsWith("file:"))
                    {
                        string _helpFileLocation = _MenuDataObject.HelpText.Replace("file:", _jsonFileLocation);
                        if (_helpFileLocation.FileExists()) { _MenuDataObject.HelpText = _helpFileLocation.ReadAllText(); }
                    }
                }
                catch
                {
                    _MenuDataObject.HelpText = "Unavailable Check Log File";
                }

                // Recursive Check For Menu Items Validity and Update Them.
                foreach (var mnuItem in _MenuDataObject.SubMenus) { _MenuDataObject.LoopOverAllMenuItems(mnuItem, LoopAction.CheckValidity); }

                // Print Valididy If Debugging
                if (_MenuDataObject.VerboseDebugging) { foreach (var mnuItem in _MenuDataObject.SubMenus) { _MenuDataObject.LoopOverAllMenuItems(mnuItem, LoopAction.PrintValidity); } }

                return _MenuDataObject;
            }
            catch (Exception ex)
            {
                // TODO Log Error
                return null;
            }
        }
       
        private void LoopOverAllMenuItems(ACTSubMenu mnuItem, LoopAction action)
        {
            if (action.HasFlag(LoopAction.CheckValidity))
            {
                if (CheckMenuRequirements(mnuItem)) { mnuItem.Valid = true; } else { mnuItem.Valid = false; }
            }

            if (action.HasFlag(LoopAction.PrintValidity)) 
            { 
                if (mnuItem.Valid == false) { ACT.Core.Windows.Console.Helper.WriteLine("Menu Item [" + mnuItem.Id.ToString() + "] Is Invalid", ConsoleColor.Red); } 
            }

            foreach (var _itm in mnuItem.SubMenus) { LoopOverAllMenuItems(_itm, action); }
        }

        public string ToJson() => JsonConvert.SerializeObject(this, JsonSerializationSettings.Settings);

        public ACTSubMenu GetMenuByID(int MenuID)
        {
            if (_MappedSubMenuCache.ContainsKey(MenuID))
            {
                return _MappedSubMenuCache[MenuID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// AddMenuItem To The ParentID
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="newMenuItem"></param>
        /// <returns></returns>
        public (bool Success, string ErrorMsg) AddMenuItem(int ParentID, ACTSubMenu newMenuItem)
        {
            if (ParentID != 0)
            {
                ACTSubMenu _ParentSubMenu = null;
                _ParentSubMenu = _MappedSubMenuToParentMenu[ParentID];
                int _MaxID = _ParentSubMenu.SubMenus.OrderByDescending(x => x.Id).First().Id;
                if (_MaxID > 8) { return (false, "Too Many Menu Items Only 10 Allowed Per Screen... For Now"); }
                newMenuItem.Id = _MaxID++;
                _ParentSubMenu.SubMenus.Add(newMenuItem);
                _MappedSubMenuToParentMenu.Add(newMenuItem.Id, null);
            }
            else
            {
                int _MaxID = this.SubMenus.OrderByDescending(x => x.Id).First().Id;
                if (_MaxID > 8) { return (false, "Too Many Menu Items Only 10 Allowed Per Screen... For Now"); }
                newMenuItem.Id = _MaxID++;
                this.SubMenus.Add(newMenuItem);
                _MappedSubMenuToParentMenu.Add(newMenuItem.Id, _MappedSubMenuCache[ParentID]);
            }

            _MappedSubMenuCache.Add(newMenuItem.Id, newMenuItem);
            

            return (true, "");
        }

        /// <summary>
        /// AddMenuItem To The ParentID
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="mnuToRemove"></param>
        /// <returns></returns>
        public (bool Success, string ErrorMsg) DeleteMenuItem(int ParentID, ACTSubMenu mnuToRemove)
        {
            try
            {
                if (ParentID != 0)
                {
                    ACTSubMenu _ParentSubMenu = null;
                    _ParentSubMenu = _MappedSubMenuToParentMenu[ParentID];
                    _ParentSubMenu.SubMenus.Remove(_ParentSubMenu.SubMenus.First(xii => xii.Id == mnuToRemove.Id));
                }
                else { this.SubMenus.Remove(this.SubMenus.First(xii => xii.Id == mnuToRemove.Id)); }
            }
            catch(Exception ex) 
            {
                if (VerboseDebugging) { ACT.Core.Windows.Console.Helper.WriteLine("Error Deleting Menu Item: " + ex.Message, ConsoleColor.Red); }
                return (false, "");
            }

            return (true, "");
        }

        /// <summary>
        /// Check Menu Requirements
        /// </summary>
        /// <param name="mnuItem"></param>
        /// <returns></returns>
        private bool CheckMenuRequirements(ACTSubMenu mnuItem)
        {
            if (mnuItem.Text.NullOrEmpty()) { return false; }
            if (mnuItem.Executecallback.NullOrEmpty()) { return false; }
            if (mnuItem.Dll.NullOrEmpty() == false)
            {
                if (mnuItem.Dll.ToLower().EndsWith(".dll") == false) { return false; }
                if (mnuItem.FullClassName.NullOrEmpty()) { return false; }
            }
            return true;
        }
    }
    public class ACTSubMenu
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsonSerializationSettings.ParseStringConverter))]
        public int Id { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("shortcut", NullValueHandling = NullValueHandling.Ignore)]
        public string Shortcut { get; set; }

        [JsonProperty("dll", NullValueHandling = NullValueHandling.Ignore)]
        public string Dll { get; set; }

        [JsonProperty("fullclassname", NullValueHandling = NullValueHandling.Ignore)]
        public string FullClassName { get; set; }

        [JsonProperty("executecallback", NullValueHandling = NullValueHandling.Ignore)]
        public string Executecallback { get; set; }

        [JsonProperty("isadminfeature", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Isadminfeature { get; set; }

        [JsonProperty("isdebugfeature", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Isdebugfeature { get; set; }

        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hidden { get; set; }

        [JsonProperty("menu", NullValueHandling = NullValueHandling.Ignore)]
        public List<ACTSubMenu> SubMenus { get; set; }

        [JsonProperty("valid", NullValueHandling = NullValueHandling.Ignore)]
        public bool Valid { get; set; }
    }

    public class LoggerPluginInfo
    {
        [JsonProperty("dll", NullValueHandling = NullValueHandling.Ignore)]
        public string Dll { get; set; }

        [JsonProperty("fullclassname", NullValueHandling = NullValueHandling.Ignore)]
        public string FullClassName { get; set; }
    }
}