using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Types.Database
{
    /// <summary>
    /// MSSQL Large Script
    /// </summary>
    public class MSSQL_LargeScript
    {
        public string SQLFileLocation { get; set;  }
        public string OutputFileLocation { get; set; }
        public bool UseUnicodeOutputFormat { get; set; }
        public bool UseTrustedConnection { get; set; }
        public bool EncryptOutputFile { get; set; }
        public string EncryptionKey { get; set; }
    }
}

