// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="HelperMethods.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;
using System.Net;

namespace ACT.Core.Web
{
    /// <summary>
    /// Class Helper.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Calls the generic handler.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <returns>System.String.</returns>
        public static string CallGenericHandler(string URL)
        {
            return ACT.Core.Communications.Http.CallGenericHandler(URL);
        }

        /// <summary>
        /// Calls the generic handler.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>Guid.</returns>
        public static Guid CallGenericHandler(string URL, Dictionary<string, string> Parameters)
        {
            return ACT.Core.Communications.Http.CallGenericHandler(URL, Parameters);
        }
       
        /// <summary>
        /// Parses the query string.
        /// </summary>
        /// <param name="FullPath">The full path.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> ParseQueryString(string FullPath)
        {
            return ACT.Core.Communications.Http.ParseQueryString(FullPath);            
        }

        /// <summary>
        /// Posts the data to website.
        /// </summary>
        /// <param name="HeaderData">The header data.</param>
        /// <param name="Data">The data.</param>
        /// <param name="PostURL">The post URL.</param>
        /// <returns>System.String.</returns>
        public static string PostDataToWebsite(Dictionary<string, string> HeaderData, Dictionary<string, string> Data, string PostURL)
        {
            return ACT.Core.Communications.Http.PostDataToWebsite(HeaderData, Data, PostURL);            
        }
    }
}
