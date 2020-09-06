///-------------------------------------------------------------------------------------------------
// file:	Web\WebForms\ACT_WEB_CORE_JSWRITER.cs
//
// summary:	Implements the act web core jswriter class
///-------------------------------------------------------------------------------------------------

#if DOTNETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ACT.Core.Extensions;

namespace ACT.Core.Web.WebForms
{
    public static class ACT_WEB_CORE_JSWRITER
    {
        /// <summary>
        /// Generate the Javascript Validation Script based on the Library
        /// </summary>
        /// <param name="ValidationData">List of Validation Data</param>
        /// <param name="Library">Custom Library (Not Used Currently)</param>
        /// <returns>String to Validate Form Data</returns>
        public static string GenerateJSValidationScript(List<ACT_WEB_PAGE.Form_Validator_Data> ValidationData, string Library = "validate.js")
        {
            string _JSTemplate = "";
            string _FileName = "";

            if (Library == "validate.js") { _FileName = "Validation_Template.xml"; }

            _JSTemplate = ACT.Core.Managers.DataManager.GetResource(_FileName, false, "Resources", "Web Form Config");

            XElement XEl = XElement.Parse(_JSTemplate);

            string _TmpReturn = XEl.Descendants("outsidetemplate").First().Value;

            XElement XConstraints = XEl.Descendants("constraints").First();

            string _TmpConstraints = "var constraints = {" + Environment.NewLine;

            foreach (var itm in ValidationData)
            {
                string _tmpItem = "\"" + itm.ControlID + "\": { " + Environment.NewLine;
            
                if (itm.Required == true)
                {
                    string _rtmp = XConstraints.Descendants("required").First().Value;
                    _rtmp = _rtmp.Replace("###ISREQUIRED###", "true") + ",";
                    _tmpItem += _rtmp;
                }
                
                if (itm.IsEmailAddress)
                {
                    string _rtmp = XConstraints.Descendants("range").First().Value;
                    _rtmp = _rtmp.Replace("###ISEMAIL###", "true") + ",";
                    _tmpItem += _rtmp;
                }

                if (itm.Min != -1 && itm.Max != -1)
                {
                    string _rtmp = XConstraints.Descendants("email").First().Value;
                    _rtmp = _rtmp.Replace("###MIN###", itm.Min.ToString());
                    _rtmp = _rtmp.Replace("###MAX###", itm.Max.ToString()) + ",";
                    _tmpItem += _rtmp;
                }

                if (itm.RegEx.NullOrEmpty())
                {
                    string _rtmp = XConstraints.Descendants("regularexpression").First().Value;
                    _rtmp = _rtmp.Replace("###REGULAREXPRESSION###", itm.RegEx.ToString());
                    _rtmp = _rtmp.Replace("###MESSAGE###", itm.ErrorMessage.ToString());                    
                    _rtmp = _rtmp.Replace("###UPPERCASE###", "i") + ",";
                    _tmpItem += _rtmp;
                }

                if (itm.EqualToControlID.NullOrEmpty())
                {
                    string _rtmp = XConstraints.Descendants("equality").First().Value;
                    _rtmp = _rtmp.Replace("###CONTROLID###", itm.RegEx.ToString());
                    _rtmp = _rtmp.Replace("###MESSAGE###", itm.ErrorMessage.ToString()) + ",";
                    _tmpItem += _rtmp;
                }

                if (itm.IncludesValue.Count > 0)
                {
                    string _rtmp = XConstraints.Descendants("inclusion").First().Value;
                    string _tmpInclusion = "";
                    foreach (string tItm in itm.IncludesValue) { _tmpInclusion += "\"" + tItm + "\","; }
                    _tmpInclusion = _tmpInclusion.TrimEnd(",");

                    _rtmp = _rtmp.Replace("###VALUEARRAY###", _tmpInclusion);
                    _rtmp = _rtmp.Replace("###MESSAGE###", itm.ErrorMessage.ToString()) + ",";
                    _tmpItem += _rtmp;
                }

                if (itm.DateEarliestTerm.NullOrEmpty())
                {
                    string _rtmp = XConstraints.Descendants("dateearliest").First().Value;  
                    _rtmp = _rtmp.Replace("###TERM###", itm.DateEarliestTerm);
                    _rtmp = _rtmp.Replace("###DATEPART", itm.DateEarliestPart);
                    _rtmp = _rtmp.Replace("###MESSAGE###", itm.ErrorMessage.ToString()) + ",";
                    _tmpItem += _rtmp;
                }

                if (itm.DateLatestTerm.NullOrEmpty())
                {
                    string _rtmp = XConstraints.Descendants("datelatest").First().Value;
                    _rtmp = _rtmp.Replace("###TERM###", itm.DateLatestTerm);
                    _rtmp = _rtmp.Replace("###DATEPART", itm.DateLatestPart);
                    _rtmp = _rtmp.Replace("###MESSAGE###", itm.ErrorMessage.ToString()) + ",";
                    _tmpItem += _rtmp;
                }

                _tmpItem = _tmpItem.TrimEnd(",");

                _tmpItem = _tmpItem + "},";

                _TmpConstraints = _TmpConstraints + _tmpItem;
            }

            _TmpConstraints = _TmpConstraints.TrimEnd(",") + "};";

            _TmpReturn = _TmpReturn.Replace("###CONSTRAINTS###", _TmpConstraints);

            XEl = null; XConstraints = null;
            _JSTemplate = "";

            return _TmpReturn;
        }
    }
}
#endif