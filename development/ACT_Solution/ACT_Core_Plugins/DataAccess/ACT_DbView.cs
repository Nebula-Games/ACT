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
using ACT.Core.Extensions;

namespace ACT.Plugins.DataAccess
{
    /// <summary>
    /// ACT Database View
    /// </summary>
    public class ACT_IDbView : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.DataAccess.I_DbView
    {
        /// <summary>
        /// Parent Database
        /// </summary>
        public I_Db ParentDatabase { get; set; }
        private List<string> _PrimaryKeys = new List<string>();
        private BindingList<I_DbColumn> _Columns = new BindingList<I_DbColumn>();
        private int _AgeInDays = -1;

        public BindingList<I_DbColumn> Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }

        private string _Name = "";
        private string _ShortName = "";
        private string _Description = "";
        private string _Owner = "";
             
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public string ShortName
        {
            get
            {
                return _ShortName;
            }
            set
            {
                _ShortName = value;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public string Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
            }
        }

        public int AgeInDays
        {
            get
            {
                return _AgeInDays;
            }
            set
            {
                _AgeInDays = value;
            }
        }

        #region IComparable Members

        /// <summary>
        /// Compares One Object To Another ONLY SUPPORTS SPECIFIC CLASS IMPLEMENTATIONs
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is ACT_IDbView)
            {
                return (obj as ACT_IDbView).Name.CompareTo(Name);
            }
            else
            {
                throw new ArgumentException("Object is not DIP_IDbView");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a Table the the Internal List
        /// </summary>
        /// <param name="Column"></param>
        /// <returns></returns>
        public bool AddColumn(I_DbColumn Column)
        {
            try
            {
                _Columns.Add((ACT_IDbColumn)Column);
            }
            catch (Exception ex)
            {
                LogError(this.GetType().FullName, "Error Adding Column", ex, "", ErrorLevel.Warning);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Gets a Column based on Name
        /// </summary>
        /// <param name="Name">Name of Column</param>
        /// <param name="IgnoreCase">Ignore Case?</param>
        /// <returns>IDbColumn</returns>
        public I_DbColumn GetColumn(string Name, bool IgnoreCase)
        {
            for (int x = 0; x < _Columns.Count; x++)
            {
                if (IgnoreCase)
                {
                    if (_Columns[x].Name.ToLower() == Name.ToLower())
                    {
                        return _Columns[x];
                    }
                }
                else
                {
                    if (_Columns[x].Name == Name)
                    {
                        return _Columns[x];
                    }
                }
            }
            return null;
        }
        
        /// <summary>
        /// Gets a Column based on Index
        /// </summary>
        /// <param name="Index">Index of Column</param>        
        /// <returns>IDbColumn</returns>
        public I_DbColumn GetColumn(int Index)
        {
            if (Index < _Columns.Count)
            {
                return _Columns[Index];
            }
            return null;
        }

        /// <summary>
        /// Generate a Hash Code Based On Object Values
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int TmpReturn = 0;
            foreach (I_DbColumn _TmpColumn in _Columns)
            {
                foreach (char c in _TmpColumn.Name)
                {
                    TmpReturn += (int)c;
                }
                foreach (char c in Name)
                {
                    TmpReturn += (int)c;
                }
            }
            return TmpReturn;
        }

        /// <summary>
        /// Override the Dispose to Provide Functionality
        /// </summary>
        public override void Dispose()
        {
            _Columns = null;
            _Name = null;
        }

        #endregion

        #region ICore Members
        /// <summary>
        /// Override the ExportXML to Provide XML Functionality
        /// </summary>
        /// <returns></returns>
        public override string ExportXMLData()
        {
            StringBuilder _TmpExport = new StringBuilder("\t<View name=\"");
            _TmpExport.Append(_Name + "\"");
            _TmpExport.Append(" ShortName=\"" + _ShortName + "\" ");
            _TmpExport.Append(" Description=\"" + _Description + "\" ");
            _TmpExport.Append(" Owner=\"" + _Owner + "\" ");
            _TmpExport.Append(">\n\r");

            _TmpExport.Append("\t\t<Columns>\n\r");
            foreach (I_DbColumn _C in _Columns)
            {
                _TmpExport.Append(_C.ExportXMLData());
            }
            _TmpExport.Append("\t\t</Columns>\n\r");
            _TmpExport.Append("\t</View>\n\r");

            return _TmpExport.ToString();
        }
        #endregion
    }
}
