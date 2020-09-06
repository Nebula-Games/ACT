///-------------------------------------------------------------------------------------------------
// file:	Web\ACT_URLRewriter.cs
//
// summary:	Implements the act URL rewriter class
///-------------------------------------------------------------------------------------------------

#if DOTNETFRAMEWORK
// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_URLRewriter.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ACT.Core.Extensions;

namespace ACT.Core.Web
{
    /// <summary>
    /// ACT URL Rewriter
    /// Required System Settings
    /// httpredirection
    /// Example Value
    /// Implements the <see cref="System.Web.IHttpModule" />
    /// </summary>
    /// <seealso cref="System.Web.IHttpModule" />
    public class ACTURLRewriter : IHttpModule
    {
        /// <summary>
        /// The redirection data
        /// </summary>
        private Dictionary<string, string> RedirectionData = new Dictionary<string, string>();
        /// <summary>
        /// The loaded
        /// </summary>
        private bool Loaded = false;

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
           // throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        /// <exception cref="Exception">Error Loading HTTP Redirection Data</exception>
        public void Init(HttpApplication context)
        {
            if (LoadData() == false) { throw new Exception("Error Loading HTTP Redirection Data"); }
            context.BeginRequest += context_BeginRequest;
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool LoadData()
        {
            string _Data = ACT.Core.SystemSettings.GetSettingByName("httpredirection").Value;

            var _TmpRedirectionData = _Data.ToDictionaryFromFormattedXML();

            // Adjust Data Into REGEX

            string _Base = "^";
            string _Word = ".\\b(#WORD#)\\b";
            string _Space = ".+";

            foreach (var K in _TmpRedirectionData.Keys)
            {
                string[] _splitter = K.SplitString("/", StringSplitOptions.RemoveEmptyEntries);

                string _TmpNewKey = _Base;

                foreach (var sp in _splitter)
                {
                    if (sp == "") { continue; }

                    if (sp == "?")
                    {
                        _TmpNewKey += _Space;
                    }
                    else
                    {
                        _TmpNewKey += _Word.Replace("#WORD#", sp.ToLower());
                    }
                }

                RedirectionData.Add(_TmpNewKey, _TmpRedirectionData[K]);
            }

            if (RedirectionData.Count() > 0) { Loaded = true; }
            return Loaded;
        }

        /// <summary>
        /// TODO Increase Speed
        /// Look for Memory Leaks
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void context_BeginRequest(object sender, EventArgs e)
        {
            if (Loaded == false)
            {
                LoadData();
            }

            HttpRequest request = ((HttpApplication)(sender)).Request;
            HttpContext context = ((HttpApplication)(sender)).Context;
            HttpResponse response = ((HttpApplication)(sender)).Response;

            string applicationPath = request.ApplicationPath;

            if ((applicationPath == "/"))
            {
                applicationPath = String.Empty;
            }
           
            string requestPath = request.Url.AbsolutePath.Substring(applicationPath.Length);

            if (requestPath.ToLower().EndsWith(".aspx"))
            {
                requestPath = requestPath.Substring(0, requestPath.LastIndexOf("/"));
            }

            if (requestPath.EndsWith("/") == false)
            {
                requestPath += "/";
            }
            requestPath =  HttpContext.Current.Server.UrlDecode(requestPath).ToLower();

            requestPath = requestPath.TrimEnd("/");
            //Redirect conditions
            Dictionary<string, int> _Results = new Dictionary<string, int>();

            foreach (var Path in RedirectionData.Keys)
            {
                System.Text.RegularExpressions.Regex _Reg = new System.Text.RegularExpressions.Regex(Path);

                var _test = _Reg.Match(requestPath);

                if (_test.Success)
                {
                    _Results.Add(Path, _test.Length);
                }
            }

            if (_Results.Count() > 0)
            {
                var _MainMatch = _Results.OrderBy(x => x.Value.ToString().Length).Reverse().First();

                context.RewritePath(RedirectionData[_MainMatch.Key] + "?Data=" + requestPath.ToBase64());
            }
        }
    }
}
#endif