// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="HttpResponse.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
#if DOTNETFRAMEWORK
using System.Web.UI;
#endif

namespace ACT.Core.Extensions
{
#if DOTNETFRAMEWORK
    /// <summary>
    /// Class ResponseHelper.
    /// </summary>
    public static class ResponseHelper
    {
        /// <summary>
        /// Redirects the specified URL.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="url">The URL.</param>
        /// <param name="target">The target.</param>
        /// <param name="windowFeatures">The window features.</param>
        /// <exception cref="InvalidOperationException">Cannot redirect to new window outside Page context.</exception>
        public static void Redirect(this HttpResponse response, string url, string target, string windowFeatures)
        {

            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowFeatures))
            {
                response.Redirect(url);
            }
            else
            {
                Page page = (Page)HttpContext.Current.Handler;

                if (page == null)
                {
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);

                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }
                script = String.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }
    }
#endif
}
