///-------------------------------------------------------------------------------------------------
// file:	Types\Database\GenericImportInformation.cs
//
// summary:	Implements the generic import information class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Database
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Information about the generic import. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/21/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class GenericImportInformation
    {

        public List<Interfaces.DataAccess.I_DbColumn> ColumnInformation = new List<Interfaces.DataAccess.I_DbColumn>();

        public Dictionary<string, Interfaces.DataAccess.I_DbWhereStatement> ConditionalImport = new Dictionary<string, Interfaces.DataAccess.I_DbWhereStatement>();

      //  public void ProcessFile(byte[] FileData, Enums.Files.StructuredTypesEnum)


    }
}
