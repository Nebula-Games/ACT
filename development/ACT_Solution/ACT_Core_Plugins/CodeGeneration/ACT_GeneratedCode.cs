using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces;
using ACT.Core;
using ACT.Core.Enums;

namespace ACT.Plugins.CodeGeneration
{
    public class ACT_GeneratedCode : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.CodeGeneration.I_GeneratedCode
    {
        private string _FileName;
        private string _Path;
        private string _Code;
        private string _UserCode;
        private string _WebServiceCode;
        public string WebServiceSoapCode { get; set; }
        public string WebServiceSoapASMX { get; set; }
        public string WebServiceCode
        {
            get { return _WebServiceCode; }
            set { _WebServiceCode = value; }
        }
        private bool _CompileBaseLayerOnly;
        private string _TableName;

        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }


        public bool CompileBaseLayerOnly
        {
            get { return _CompileBaseLayerOnly; }
            set { _CompileBaseLayerOnly = value; }
        }

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        private string _DatabaseConnectionString;

        public string DatabaseConnectionString
        {
            get { return _DatabaseConnectionString; }
            set { _DatabaseConnectionString = value; }
        }
        private string _DatabaseConnectionName;

        public string DatabaseConnectionName
        {
            get { return _DatabaseConnectionName; }
            set { _DatabaseConnectionName = value; }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
            }
        }

        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
    }
}
