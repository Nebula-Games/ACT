///-------------------------------------------------------------------------------------------------
// file:	Interfaces\DataAccess\I_Db_TypeConverter.cs
//
// summary:	Declares the I_Db_TypeConverter interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// DB Type Converter - System Agnostic Definition
    /// </summary>
    public interface I_Db_TypeConverter
    {
        /// <summary>
        /// Source Type
        /// </summary>
        string SourceType { get; set; }
        /// <summary>
        /// Source Length A
        /// </summary>
        int SourceLengthA { get; set; }
        /// <summary>
        /// Source Length B
        /// </summary>
        int SourceLengthB { get; set; }
        /// <summary>
        /// Source Value
        /// </summary>
        object SourceValue { get; set; }
        /// <summary>
        /// Dest Type
        /// </summary>
        string DestType { get; set; }
        /// <summary>
        /// Dest Length A
        /// </summary>
        int DestLengthA { get; set; }
        /// <summary>
        /// Dest Length B
        /// </summary>
        int DestLengthB { get; set; }
        /// <summary>
        /// Dest Value 
        /// </summary>
        object DestValue { get; set; }
        /// <summary>
        /// Convert Method
        /// </summary>
        /// <returns></returns>
        object Convert();

        /// <summary>
        /// Types With Length
        /// </summary>
        Dictionary<string, bool> TypesWithLengthHasTwo
        {
            get;
        }

        /// <summary>
        /// Valid Types
        /// </summary>
        string[] ValidTypes
        {
            get;
        }
    }
}
