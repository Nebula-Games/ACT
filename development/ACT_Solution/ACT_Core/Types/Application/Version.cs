///-------------------------------------------------------------------------------------------------
// file:	Types\Application\Version.cs
//
// summary:	Implements the version class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Application
{
    /// <summary>
    /// Advanced Versioning Holder.  
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Traditional Major Mesaure
        /// </summary>
        public int Major { get; set; }

        /// <summary>
        /// Traditional Minor Measure
        /// </summary>
        public int Minor { get; set; }

        /// <summary>
        /// Traditional Build Measure
        /// </summary>
        public int Build { get; set; }

        /// <summary>
        /// Traditional Revision Measure
        /// </summary>
        public int Revision { get; set; }

        /// <summary>
        /// Contains the Display Name of The Last Person To Commit Him
        /// </summary>
        public string SubmittedBy { get; set; }

        /// <summary>
        /// Changes If Specificed
        /// </summary>
        public Dictionary<string,string> ChangeSet { get; set; }

        /// <summary>
        /// Release Notes
        /// </summary>
        public string Notes { get; set; }

    }
}
