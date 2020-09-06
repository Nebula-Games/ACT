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
    /// Represents a IDbColumn 
    /// </summary>
    public class ACT_IDbColumn :  ACT.Plugins.ACT_Core, I_DbColumn
    {

		#region Fields (13) 

        private I_DbTable _ParentTable;

        public I_DbTable ParentTable
        {
            get { return _ParentTable; }
            set { _ParentTable = value; }
        }

        private bool _AutoIncrement = false;
        private System.Data.DbType _DataType;
        private string _Default;
        private bool _Identity = false;
        private int _IdentityIncrement = 0;
        private int _IdentitySeed = 0;
        private string _Name = "";
        private bool _Nullable = false;
        private int _Precision;
        private bool _PrimaryKey = false;
        private int _Scale;
        private string _ShortName = "";
        private int _Size = 0;
        private string _Description = "";

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Basic Constructor
        /// </summary>
        public ACT_IDbColumn() { }

		#endregion Constructors 
        
        #region IComparable Members

        /// <summary>
        /// Compares this to obj
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is ACT_IDbColumn)
            {
                return (obj as ACT_IDbColumn).Name.CompareTo(Name);
            }
            else
            {
                throw new ArgumentException("Object is not a DIP_IDbColumn");
            }
        }

        #endregion


        #region Operator Overloads

        public override bool Equals(object obj)
        {
            if (obj is ACT_IDbColumn)
            {
                if ((ACT_IDbColumn)obj == this)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// My Implementation of the == Operator
        /// </summary>
        /// <param name="x">DIP_ID</param>
        /// <param name="y"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(ACT_IDbColumn x, ACT_IDbColumn y)
        {
            if (x.Name == y.Name)
            {
                if (x.DataType == y.DataType)
                {
                    if (x.Default == y.Default)
                    {
                        if (x.Identity == y.Identity)
                        {
                            if (x.IdentityIncrement == y.IdentityIncrement)
                            {
                                if (x.IsPrimaryKey == y.IsPrimaryKey)
                                {
                                    if (x.Nullable == y.Nullable)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            return false;
        }

        /// <summary>
        /// My Implementation of the != Operator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(ACT_IDbColumn x, ACT_IDbColumn y)
        {
            if (x == y) { return false; }

            return true;
        }

        #endregion
        #region ICore Members

        public override I_TestResult ImportXMLData(string XML)
        {
            //try
            //{
            //    System.IO.StringReader _SR = new System.IO.StringReader("<ColumnData>" + XML + "</ColumnData>");

            //    System.Xml.XPath.XPathDocument XPD = new System.Xml.XPath.XPathDocument(_SR);
            //    System.Xml.XPath.XPathNavigator XPN = XPD.CreateNavigator();

            //    System.Xml.XPath.XPathExpression XPE;

            //    XPE = XPN.Compile("/ColumnData");

            //    System.Xml.XPath.XPathNodeIterator XPNI = XPN.Select(XPE);

            //    while (XPNI.MoveNext())
            //    {
            //        System.Xml.XPath.XPathNodeIterator XPCNI = XPNI.Current.SelectChildren(System.Xml.XPath.XPathNodeType.Element);
            //        while (XPCNI.MoveNext())
            //        {
            //            switch (XPCNI.Current.Name)
            //            {
            //                case "DataType":
            //                    //DataType = XPCNI.Current.Value;
            //                    break;
            //                case "Nullable":
            //                    // Nullable = XPCNI.Current.Value;
            //                    break;
            //                case "ColumnDefault":
            //                    ColumnDefault = XPCNI.Current.Value;
            //                    break;
            //                case "MaxCharLength":
            //                    // MaxCharLength = XPCNI.Current.Value;
            //                    break;
            //                case "MaxOctLength":
            //                    //  MaxOctLength = XPCNI.Current.Value;
            //                    break;
            //                default:
            //                    throw new Exception("Error Processing Column Data");
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    return false;
            //}

            return null;
        }

        /// <summary>
        /// Exports the Column to XML
        /// </summary>
        /// <returns></returns>
        public override string ExportXMLData()
        {
            StringBuilder _TmpExport = new StringBuilder("\t\t\t<Column name=\"");
            _TmpExport.Append(_Name);
            _TmpExport.Append("\">\n\r");
            

            _TmpExport.Append("\t\t\t\t<DataType>");
            _TmpExport.Append(DataType.ToDBStringCustom());
            _TmpExport.Append("</DataType>\n\r");

            _TmpExport.Append("\t\t\t\t<Nullable>");
            _TmpExport.Append(_Nullable.ToString());
            _TmpExport.Append("</Nullable>\n\r");

            _TmpExport.Append("\t\t\t\t<ColumnDefault>");
            _TmpExport.Append(_Default);
            _TmpExport.Append("</ColumnDefault>\n\r");

            _TmpExport.Append("\t\t\t\t<Size>");
            _TmpExport.Append(_Size.ToString());
            _TmpExport.Append("</Size>\n\r");

            _TmpExport.Append("\t\t\t\t<Identity>");
            _TmpExport.Append(_Identity.ToString());
            _TmpExport.Append("</Identity>\n\r");

            _TmpExport.Append("\t\t\t\t<PrimaryKey>");
            _TmpExport.Append(_PrimaryKey.ToString());
            _TmpExport.Append("</PrimaryKey>\n\r");
            
            _TmpExport.Append("\t\t\t\t<Scale>");
            _TmpExport.Append(Scale.ToString());
            _TmpExport.Append("</Scale>\n\r");

            _TmpExport.Append("\t\t\t\t<Precision>");
            _TmpExport.Append(_Precision.ToString());
            _TmpExport.Append("</Precision>\n\r");

            _TmpExport.Append("\t\t\t\t<IdentitySeed>");
            _TmpExport.Append(_IdentitySeed.ToString());
            _TmpExport.Append("</IdentitySeed>\n\r");

            _TmpExport.Append("\t\t\t\t<IdentityIncrement>");
            _TmpExport.Append(_IdentityIncrement.ToString());
            _TmpExport.Append("</IdentityIncrement>\n\r");

            _TmpExport.Append("\t\t\t\t<AutoIncrement>");
            _TmpExport.Append(_AutoIncrement.ToString());
            _TmpExport.Append("</AutoIncrement>\n\r");

            _TmpExport.Append("\t\t\t</Column>\n\r");

            return _TmpExport.ToString();

        }

        #endregion

        #region IDbColumn Members

        /// <summary>
        /// Fully Qualified Name
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                HasChanged = true;
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
                HasChanged = true;
            }
        }

        /// <summary>
        /// Column Short Name
        /// </summary>
        public string ShortName
        {
            get
            {
                return _Name;
            }
            set
            {
                _ShortName = value;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Column Data Type
        /// </summary>
        public System.Data.DbType DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                _DataType = value;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Default Value i.e GetDate() or 1 or '1' etc..
        /// </summary>
        public string Default
        {
            get
            {
                return _Default;
            }
            set
            {
                HasChanged = true;
                _Default = value;
            }
        }

        /// <summary>
        /// Allows Nulls
        /// </summary>
        public bool Nullable
        {
            get
            {
                return _Nullable;
            }
            set
            {
                HasChanged = true;
                _Nullable = value;
            }
        }

        /// <summary>
        /// Size of Data Column
        /// </summary>
        public int Size
        {
            get
            {
                return _Size;
            }
            set
            {
                HasChanged = true;
                _Size = value;
            }
        }

        /// <summary>
        /// The Precision
        /// </summary>
        public int Precision
        {
            get { return _Precision; }
            set { _Precision = value; }
        }

        /// <summary>
        /// The Scale
        /// </summary>
        public int Scale
        {
            get
            {
                return _Scale;
            }
            set
            {
                _Scale = value;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Is this an Identity Column
        /// </summary>
        public bool Identity
        {
            get
            {
                return _Identity;
            }
            set
            {
                HasChanged = true;
                _Identity = value;
            }
        }

        /// <summary>
        /// Identity Increment Of The Auto Generated Field
        /// </summary>
        public int IdentityIncrement
        {
            get { return _IdentityIncrement; }
            set { _IdentityIncrement = value; HasChanged = true; }
        }

        /// <summary>
        /// Identity Seed of Auto Incremented Field
        /// </summary>
        public int IdentitySeed
        {
            get { return _IdentitySeed; }
            set
            {
                _IdentitySeed = value;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Auto Increment
        /// </summary>
        public bool AutoIncrement
        {
            get
            {
                return _AutoIncrement;
            }
            set
            {
                HasChanged = true;
                _AutoIncrement = value;
            }
        }

        /// <summary>
        /// Is this Column a Primary Key
        /// </summary>
        public bool IsPrimaryKey
        {
            get
            {
                return _PrimaryKey;
            }
            set
            {
                HasChanged = true;
                _PrimaryKey = value;
            }
        }

        #endregion
    }
}
