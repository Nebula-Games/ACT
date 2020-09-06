// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-04-2019
// ***********************************************************************
// <copyright file="ASPNET_Helper.cs" company="Nebula Entertainment LLC">
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
namespace ACT.Core.Web.HelperMethods
{
#if DOTNETFRAMEWORK
    /// <summary>
    /// Class ASPNET_Helper.
    /// </summary>
    public static class ASPNET_Helper
    {
        /// <summary>
        /// Returns all of the Loaded Modules
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetAllLoadedModules(HttpContext context)
        {
            List<string> _tmpReturn = new List<string>();

            //Get Application Instance from Current Content
            //Get List of modules in module collections

            HttpApplication httpApps = HttpContext.Current.ApplicationInstance;
            HttpModuleCollection httpModuleCollections = httpApps.Modules;
            
            foreach (string activeModule in httpModuleCollections.AllKeys) { _tmpReturn.Add(activeModule); }

            return _tmpReturn;
        }

        /// <summary>
        /// Gets the browser image.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <returns>System.Drawing.Image.</returns>
        public static System.Drawing.Image GetBrowserImage(string URL)
        {
            /*System.Windows.Forms.WebBrowser _Browser = new System.Windows.Forms.WebBrowser();
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load("Yor Path(local,web)");
            var result = doc.DocumentNode.Descendants("img");//return HtmlCollectionNode*/
            return null;
        }
    }
#endif
}
