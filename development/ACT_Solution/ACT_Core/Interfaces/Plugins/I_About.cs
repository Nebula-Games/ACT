using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Plugins
{
    public interface I_About
    {
        /// <summary>
        /// Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Author
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Author ID
        /// </summary>
        string AuthorID { get; }

        /// <summary>
        /// Company Name
        /// </summary>
        string CompanyName { get; }

        /// <summary>
        /// Company ID
        /// </summary>
        string CompanyID { get; }

        /// <summary>
        /// Current Assembly Hash Value
        /// </summary>
        byte[] AssemblyHashvalue
        {
            get;
        }

    }
}
