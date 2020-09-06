// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Pattern Recognition String Extension Methods
    /// </summary>
    public static class StringPatterns
    {
        /// <summary>
        /// Patternses the get common start.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="MustEndWith">The must end with.</param>
        /// <param name="FailOnNotEnd">if set to <c>true</c> [fail on not end].</param>
        /// <returns>System.String.</returns>
        public static string Patterns_GetCommonStart(this List<string> Data, string MustEndWith = "\\", bool FailOnNotEnd = false)
        {
            string _TmpReturn = "";
            string _BaseString = Data[0];

            int _CharacterPosition = 0;

            for (_CharacterPosition = 0; _CharacterPosition < _BaseString.Length; _CharacterPosition++)
            {
                bool _Found = true;
                char _CurrentCharacter = _BaseString[_CharacterPosition];
                for (int x = 1; x < Data.Count(); x++)
                {
                    if ((Data[x][_CharacterPosition] == _CurrentCharacter) == false) { _Found = false; break; }
                }
                if (_Found == false) { break; }
                else
                {
                    _TmpReturn += _CurrentCharacter;
                }
            }
            if (_TmpReturn.EndsWith(MustEndWith) == false && FailOnNotEnd == true) { return ""; }

            return _TmpReturn;
        }

        /// <summary>
        /// Get Unique Items based on the Data and Common Element
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="CommonElement">The common element.</param>
        /// <param name="IsPath">if set to <c>true</c> [is path].</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> Patterns_GetUniqueMinusCommon(this List<string> Data, string CommonElement, bool IsPath)
        {
            List<string> _TmpReturn = new List<string>();
            if (IsPath) { CommonElement = CommonElement.ToLower(); }

            foreach (var d in Data)
            {
                string _tmpData = d;
                if (IsPath)
                {
                    _tmpData = _tmpData.ToLower();
                    if (_tmpData.StartsWith(CommonElement) == false) { continue; }
                    _tmpData = _tmpData.Replace(CommonElement, "");
                    if (_TmpReturn.Contains(_tmpData) == false) { _TmpReturn.Add(_tmpData); }
                }
                else
                {
                    _tmpData = _tmpData.Replace(CommonElement, "");
                    if (_TmpReturn.Contains(_tmpData) == false) { _TmpReturn.Add(_tmpData); }
                }                
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Validates a Regular Expression Pattern
        /// </summary>
        /// <param name="RegX">REGEX Pattern to Validate</param>
        /// <returns>True/False</returns>
        public static bool IsValidRegularExpression(this string RegX)
        {
            if (RegX.NullOrEmpty()) return false;

            try { System.Text.RegularExpressions.Regex.Match("", RegX); }
            catch (ArgumentException) { return false; }

            return true;
        }
    }
}
