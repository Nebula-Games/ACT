// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_API.cs" company="Nebula Entertainment LLC">
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

namespace ACT.Core.Web.API
{
#if DOTNETFRAMEWORK
    /// <summary>
    /// Class ACT_API.
    /// Implements the <see cref="System.Web.IHttpHandler" />
    /// </summary>
    /// <seealso cref="System.Web.IHttpHandler" />
    public class ACT_API : System.Web.IHttpHandler
    {
        /// <summary>
        /// Is Reusable
        /// </summary>
        /// <value><c>true</c> if this instance is reusable; otherwise, <c>false</c>.</value>
        public bool IsReusable => false;

        /// <summary>
        /// Process Request
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, <see langword="Request" />, <see langword="Response" />, <see langword="Session" />, and <see langword="Server" />) used to service HTTP requests.</param>
        public virtual void ProcessRequest(HttpContext context)
        {
            
        }

        /// <summary>
        /// Definition File Loading
        /// </summary>
        /// <param name="LocationFile">The location file.</param>
        /// <returns>ACT.Core.Web.API.API_Definition.</returns>
        public ACT.Core.Web.API.API_Definition LoadDefinitionFile(string LocationFile)
        {
            if (System.IO.File.Exists(LocationFile) == false) { return null; }

            try
            {
                string _jsonData = System.IO.File.ReadAllText(LocationFile);
                ACT.Core.Web.API.API_Definition _Definition = ACT.Core.Web.API.API_Definition.FromJson(_jsonData);
                return _Definition;
            }
            catch
            {
                return null;
            }
        }        
    }
#endif
}
