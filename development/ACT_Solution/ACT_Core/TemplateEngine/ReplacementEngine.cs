// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-15-2019
// ***********************************************************************
// <copyright file="ReplacementEngine.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using ACT.Core.Extensions;


namespace ACT.Core.TemplateEngine
{
    /// <summary>
    /// Basic Replacement Engine
    /// </summary>
    public static class ReplacementEngine
    {
        /// <summary>
        /// Performs the generic replacements.
        /// </summary>
        /// <param name="Replacements">The replacements.</param>
        /// <param name="CommonDataToUse">The common data to use.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> PerformGenericReplacements(List<string> Replacements, List<object> CommonDataToUse = null)
        {
            Dictionary<string, string> _tmpReturn = new Dictionary<string, string>();

            foreach (string k in Replacements)
            {
                _tmpReturn.Add(k, k);
                if (k.StartsWith("DATE."))
                {
                    var _DateToUse = DateTime.Now;

                    if (CommonDataToUse.Exists(x => x is DateTime))
                    {
                        try { _DateToUse = CommonDataToUse.First(x => x is DateTime).ToDateTime().Value; }
                        catch { }
                    }

                    if (k.EndsWith("YEAR")) { _tmpReturn[k] = DateTime.Now.Year.ToString(); }
                    if (k.EndsWith("FULLMONTH")) { _tmpReturn[k] = DateTime.Now.FullMonth(); }
                    if (k.EndsWith("FULLDAY")) { _tmpReturn[k] = DateTime.Now.FullDay(); }
                    if (k.EndsWith("MONTH")) { _tmpReturn[k] = DateTime.Now.Month.ToString(); }
                    if (k.EndsWith("DAY")) { _tmpReturn[k] = DateTime.Now.Day.ToString(); }
                    if (k.EndsWith("SHORTDATE")) { _tmpReturn[k] = DateTime.Now.ToShortDateString(); }
                    if (k.EndsWith("SHORTDATEFULL")) { _tmpReturn[k] = DateTime.Now.ToShortDateFull(); }
                }
            }

            return _tmpReturn;

        }

        /// <summary>
        /// Holds a single Replacement Item
        /// </summary>
        public struct ReplacementItem
        {
            /// <summary>
            /// Field Name As Found In Template
            /// </summary>
            public string FieldName;

            /// <summary>
            /// Index Position data
            /// </summary>
            public ACT.Core.Types.TwoValues<int> IndexPositionData;
        }

        /// <summary>
        /// Locate all Replacement Fields
        /// </summary>
        /// <param name="template">string Template</param>
        /// <param name="Pattern">Match Pattern PATTERN+FIELD+PATTERN
        /// i.e. if Pattern="###"  ###FIRSTNAME###</param>
        /// <returns>List of ReplacementItems</returns>
        /// <seealso cref="ReplacementItem" />
        public static List<ReplacementItem> FindAllReplacementFields(string template, string Pattern = "###")
        {
            List<ReplacementItem> _tmpReturnData = new List<ReplacementItem>();
            System.Text.RegularExpressions.Regex _regex = new System.Text.RegularExpressions.Regex(Pattern + "(.*?)" + Pattern);
            var _matches = _regex.Matches(template);
            foreach (System.Text.RegularExpressions.Match match in _matches)
            {
                if (!_tmpReturnData.Exists(x => x.FieldName == match.Groups[0].Value.Replace(Pattern, "")))
                {
                    _tmpReturnData.Add(new ReplacementItem()
                    {
                        FieldName = match.Groups[0].Value.Replace(Pattern, ""),
                        IndexPositionData = new Types.TwoValues<int> { First = match.Index, Second = match.Index + match.Length }
                    });
                }
            }

            return _tmpReturnData;
        }

        /// <summary>
        /// Process the Template Processing
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="ReplacementItems">The replacement items.</param>
        /// <param name="ResultSet">The result set.</param>
        /// <param name="UseFullName">if set to <c>true</c> [use full name].</param>
        /// <param name="Pattern">The pattern.</param>
        /// <returns>Parsed Template as string</returns>
        private static List<string> ProcessQueryResults(this string template, List<ReplacementItem> ReplacementItems, Interfaces.DataAccess.I_QueryResult ResultSet, bool UseFullName = false, string Pattern = "###")
        {
            List<string> _tmpReturn = new List<string>();
            var _WorkingDataTable = ResultSet.FirstDataTable_WithRows();

            foreach (System.Data.DataRow datarow in _WorkingDataTable.Rows)
            {
                string _tmpTemplate = template;

                foreach (var itm in ReplacementItems)
                {
                    if (_WorkingDataTable.Columns.Contains(itm.FieldName))
                    {
                        if (UseFullName == true)
                        {
                            _tmpTemplate = _tmpTemplate.Replace(Pattern + itm.FieldName.ToUpper() + Pattern, datarow[itm.FieldName].ToString());
                        }
                        else
                        {
                            _tmpTemplate = _tmpTemplate.Replace(Pattern + itm.FieldName.ToUpper() + Pattern, datarow[itm.FieldName].ToString());
                        }
                    }
                }

                _tmpReturn.Add(_tmpTemplate);
            }

            return _tmpReturn;
        }

        /// <summary>
        /// Process a Template and Replace the Data
        /// Multi Rows Based On The Query Result Processing Engine
        /// </summary>
        /// <param name="template">Original Template</param>
        /// <param name="queryResults">Query Result To Process (1 DataTable Only)</param>
        /// <param name="otherReplacements">Dictionary Of Other Replacements</param>
        /// <param name="Pattern">Optional (Default is ###)</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <seealso cref="ACT.Core.TemplateEngine.ReplacementEngine.ProcessQueryResults(string, bool, List{ReplacementItem}, Interfaces.DataAccess.I_QueryResult, string)" />
        public static List<string> Process(string template, Interfaces.DataAccess.I_QueryResult queryResults, Dictionary<string, string> otherReplacements, string Pattern = "###")
        {
            var _tmpReturn = new List<string>();
            if (template.NullOrEmpty()) { _tmpReturn.Add(template); return _tmpReturn; }

            // Determine all of the REPLACEMENT FIELDS IN THE TEMPLATE
            var _AllReplacements = FindAllReplacementFields(template, Pattern);
            if (_AllReplacements.Count() == 0) { _tmpReturn.Add(template); return _tmpReturn; }

            bool _HasQueryData = false;

            if (queryResults.HasRows()) { _HasQueryData = true; }

            if (_HasQueryData)
            {
                _tmpReturn = ACT.Core.TemplateEngine.ReplacementEngine.ProcessQueryResults(template, _AllReplacements, queryResults, false, Pattern);
            }

            // For Each record In the Results
            for (int idx = 0; idx < _tmpReturn.Count(); idx++)
            {
                string _tmpTemplate = _tmpReturn[idx];

                #region For Each Record in the Dictionary
                foreach (string key in otherReplacements.Keys)
                {
                    // If the record exists as a replacement then do the replace otherwise ignore;
                    if (_AllReplacements.Exists(x => x.FieldName.ToUpper() == key.ToUpper()))
                    {
                        _tmpTemplate = _tmpTemplate.Replace(Pattern + key.ToUpper() + Pattern, otherReplacements[key]);
                    }

                    #region STATIC REPLACEMENTS i.e ###STATIC_CUSTOMDATETIMEA### - Dic ( Key = ###STATIC_CUSTOMDATETIMEA###, Value = hh:mm:ss.FF )

                    else if (key.ToUpper().StartsWith("STATIC_CUSTOMDATETIME"))
                    {
                        string _tmpType = key.Replace("STATIC_CUSTOMDATETIME", "");
                        if (_AllReplacements.Exists(x => x.FieldName.ToUpper() == "STATIC_CUSTOMDATETIME" + _tmpType))
                        {
                            _tmpTemplate = _tmpTemplate.Replace(Pattern + "STATIC_CUSTOMDATETIME" + _tmpType + Pattern, DateTime.Now.ToString(otherReplacements[key]));
                        }
                    }

                    #endregion
                }
                #endregion

                #region Foreach ACT Setting PATTERN + ACT_FIELDNAME + PATTERN

                foreach (var setting in ACT.Core.SystemSettings.SettingKeys)
                {
                    // Do Replacement only if the Field Exsts in the template
                    if (_AllReplacements.Exists(x => x.FieldName.ToUpper() == "ACT_" + setting.ToUpper()))
                    {
                        _tmpTemplate.Replace(Pattern + "ACT_" + setting.ToUpper() + Pattern, ACT.Core.SystemSettings.GetSettingByName(setting).Value);
                    }
                }

                #endregion

                #region Cleanup The rest of the Replacement Keys
                foreach (var replacementKey in _AllReplacements)
                {
                    _tmpTemplate = _tmpTemplate.Replace(Pattern + replacementKey.FieldName + Pattern, "");
                }
                #endregion

                _tmpReturn[idx] = _tmpTemplate;
            }

            return _tmpReturn;
        }
    }
}
