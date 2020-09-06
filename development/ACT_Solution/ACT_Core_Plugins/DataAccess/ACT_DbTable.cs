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
    /// This class Implements the IDbTable Interface
    /// </summary>
    public class ACT_IDbTable : ACT.Plugins.ACT_Core, I_DbTable
    {

        #region Fields (8)
        public I_Db ParentDatabase { get; set; }
        private List<string> _PrimaryKeys = new List<string>();
        private BindingList<I_DbColumn> _Columns = new BindingList<I_DbColumn>();

        public BindingList<I_DbColumn> Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }

        private BindingList<I_DbRelationship> _Relationships = new BindingList<I_DbRelationship>();
        private bool _IsPackageTable = false;
        private bool _IsSystem = false;
        private bool _IsUserTable = false;
        private string _Name = "";
        private string _PrimaryKeyName = "";
        private string _ShortName = "";
        private string _Description = "";
        private string _Owner = "";
        private int _AgeInDays = -1;

    #endregion Fields

    #region Constructors (1)

    /// <summary>
    /// Create Events to Listen to any modifiactions to the Table
    /// </summary>
    public ACT_IDbTable()
        {
            _Columns.ListChanged += new ListChangedEventHandler(ListChanged);
            _Relationships.ListChanged += new ListChangedEventHandler(ListChanged);
        }

        #endregion Constructors

        #region Properties (8)

        /// <summary>
        /// Returns the Total Column Count
        /// </summary>
        public int ColumnCount
        {
            get { return _Columns.Count; }
        }

        /// <summary>
        /// Returns the Number of ForeignKeys
        /// </summary>
        public int RelationshipCount
        {
            get { return _Relationships.Count; }
        }

        /// <summary>
        /// Specifies if the Table is a Package Table (Used For Modules Only)
        /// </summary>
        public bool IsPackageTable
        {
            get { return _IsPackageTable; }
        }

        /// <summary>
        /// Specified If the Table is a System Table
        /// </summary>
        public bool IsSystem
        {
            get { return _IsSystem; }
        }

        /// <summary>
        /// Specifies if the Table is a User Table
        /// </summary>
        public bool IsUserTable
        {
            get { return _IsUserTable; }
        }

        /// <summary>
        /// Specifies the Name of the Table
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

        /// <summary>
        /// Returns the Primary Key Column Name
        /// </summary>
        public string PrimaryKeyColumnName
        {
            get { return _PrimaryKeyName; }
        }

        /// <summary>
        /// Returns the Short Name Only. Not the FQN
        /// </summary>
        public string ShortName
        {
            get
            {
                return _ShortName;
            }
            set
            {
                _ShortName = value;
                HasChanged = true;
            }
        }
        /// <summary>
        /// Returns the Description
        /// </summary>
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
        /// Returns the Short Name Only. Not the FQN
        /// </summary>
        public string Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
                HasChanged = true;
            }
        }

        /// <summary>
        /// Age In Days Since The Table Was Last Modified
        /// </summary>
        public int AgeInDays { get { return _AgeInDays; } set { _AgeInDays = value; } }

        #endregion Properties

        #region Methods (19)

        public List<string> GetPrimaryColumnNames

        {
            get
            {
                if (_PrimaryKeys.Count == 0)
                {
                    foreach (var c in _Columns)
                    {
                        if (c.IsPrimaryKey)
                        {
                            _PrimaryKeys.Add(c.ShortName);
                        }
                    }
                }

                return _PrimaryKeys;
            }
            set { _PrimaryKeys = value; }
        }
        // Public Methods (18) 

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
                LogError(this.GetType().FullName, "Error Adding Column", ex, "",ErrorLevel.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds a ForeignKey to the Table
        /// </summary>
        /// <param name="Key">IDbForeignKey</param>
        /// <returns>True On Success</returns>
        public bool AddRelationship(I_DbRelationship Key)
        {
            try
            {
                _Relationships.Add(Key);
            }
            catch (Exception ex)
            {
                LogError(this.GetType().FullName, "Error Adding Foreign Key", ex, "", ErrorLevel.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Doesnt Follow Normal Logic.  May need to rewrite
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (this == (ACT_IDbTable)obj)
                {
                    return true;
                }
                else { return false; }
            }
            catch { return false; }
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
        /// Generates a Delete Data SQL Statememt
        /// </summary>
        /// <param name="Where">List of IDbWhereStatement</param>
        /// <returns>SQL String</returns>
        public string GetDeleteDataSQL(I_DbWhereStatement Where)
        {
            string _SPTextFinal = "";
            _SPTextFinal = _SPTextFinal + "Delete From " + this.Name + " Where ";
            _SPTextFinal +=  ParentDatabase.DataAccessClass.GenerateWhereStatement( Where,"");
            return _SPTextFinal;
        }

        /// <summary>
        /// Gets a ForeignKey by Name
        /// </summary>
        /// <param name="Column_Name">Name of ForeignKey</param>
        /// <param name="IgnoreCase">Ignore Case?</param>
        /// <returns>IDBForeignKey</returns>
        public I_DbRelationship GetRelationship(string Column_Name, bool IgnoreCase)
        {
            for (int x = 0; x < _Relationships.Count; x++)
            {
                if (IgnoreCase)
                {
                    if (_Relationships[x].RelationshipName.ToLower() == Column_Name.ToLower())
                    {
                        return _Relationships[x];
                    }
                }
                else
                {
                    if (_Relationships[x].RelationshipName == Column_Name)
                    {
                        return _Relationships[x];
                    }
                }
            }
            return null;
        }
        public BindingList<I_DbRelationship> AllRelationships { get { return _Relationships; } }
        /// <summary>
        /// Gets a ForeignKey by
        /// </summary>
        /// <param name="Index">Index of Key</param>
        /// <returns>IDbForeignKey</returns>
        public I_DbRelationship GetRelationship(int Index)
        {
            if (Index < _Relationships.Count)
            {
                return _Relationships[Index];
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
        /// Generates Insert Data SQL Based on the List of Fields Passed
        /// </summary>
        /// <param name="Fields"></param>
        /// <returns></returns>
        public string GetInsertDataSQL(List<string> Fields)
        {
            string _SPTextFinal = "";
            _SPTextFinal = _SPTextFinal + "Insert Into " + this.Name + " (";
            foreach (string _Field in Fields)
            {
                _SPTextFinal = _SPTextFinal + "[" + _Field + "],";
            }
            _SPTextFinal = _SPTextFinal.TrimEnd(",".ToCharArray());
            _SPTextFinal = _SPTextFinal + ") VALUES (";
            foreach (string _Field in Fields)
            {
                _SPTextFinal = _SPTextFinal + "@" + _Field + ",";
            }
            _SPTextFinal = _SPTextFinal.TrimEnd(",".ToCharArray());
            _SPTextFinal = _SPTextFinal + ")";
            return _SPTextFinal;
        }

        public string GenerateDataExportStructure()
        {
            StringBuilder _TmpExport = new StringBuilder("");

            _TmpExport.Append("SET IDENTITY_INSERT " + _ShortName + " ON\n\r");

            foreach (I_DbColumn _Col in _Columns)
            {
                _TmpExport.Append("Declare @" + _Col.Name + " as " + _Col.DataType.ToDBStringCustom());

                if (_Col.DataType == System.Data.DbType.String || _Col.DataType == System.Data.DbType.AnsiString ||
                    _Col.DataType == System.Data.DbType.AnsiStringFixedLength || _Col.DataType == System.Data.DbType.StringFixedLength)
                {
                    _TmpExport.Append("(" + _Col.Size + ")\n\r\n\r");
                }
                else
                {
                    _TmpExport.Append("\n\r\n\r");
                }
            }

            _TmpExport.Append("\n\r\n\r#SETVARS#\n\r");
            foreach (I_DbColumn _Col in _Columns)
            {
                if (_Col.DataType == System.Data.DbType.String || _Col.DataType == System.Data.DbType.AnsiString ||
                   _Col.DataType == System.Data.DbType.AnsiStringFixedLength || _Col.DataType == System.Data.DbType.StringFixedLength)
                {
                    _TmpExport.Append("Set @" + _Col.Name + " = '#VALUE#'\n\r");
                }
                else 
                {
                    _TmpExport.Append("Set @" + _Col.Name + " = #VALUE#\n\r");
                }
            }
            _TmpExport.Append("#/SETVARS#\n\r\n\r");

            _TmpExport.Append("\n\r\n\r#INSERTSTATEMENT#\n\r");
            _TmpExport.Append("Insert Into " + _ShortName + " (");
            foreach (I_DbColumn _Col in _Columns)
            {
                _TmpExport.Append("[" + _Col.Name + "],");              
            }
            _TmpExport.Remove(_TmpExport.Length - 1, 1);
            _TmpExport.Append(") VALUES (");
            foreach (I_DbColumn _Col in _Columns)
            {
                _TmpExport.Append("@" + _Col.Name + ",");
            }
            _TmpExport.Remove(_TmpExport.Length - 1, 1);
            _TmpExport.Append(")");
            _TmpExport.Append("#/INSERTSTATEMENT#\n\r\n\r");
            return _TmpExport.ToString();
        }
        /// <summary>
        /// Generates an Update Statement With the List of Defined Where Statements
        /// </summary>
        /// <param name="Fields">List Of Fields to Include</param>
        /// <param name="Where">Where Statement</param>
        /// <returns>SQL string</returns>
        public string GetUpdateDataSQL(List<string> Fields, I_DbWhereStatement Where)
        {
            string _SPTextFinal = "";
            _SPTextFinal = _SPTextFinal + "Update " + this.Name + " Set ";
            foreach (string _Field in Fields)
            {
                _SPTextFinal = _SPTextFinal + "[" + _Field + "] = @" + _Field + ",";
            }
            _SPTextFinal.TrimEnd(",".ToCharArray());
            _SPTextFinal += " Where " + ParentDatabase.DataAccessClass.GenerateWhereStatement(Where, "");
            return _SPTextFinal;
        }

        /// <summary>
        /// Generates an Update Statement
        /// </summary>
        /// <param name="Fields">List Of Fields to Include</param>        
        /// <returns>SQL string</returns>
        public string GetUpdateDataSQL(List<string> Fields)
        {
            bool _PreTest = false;
            string _PrimaryKey = "";
            // TEST FOR VALID UPDATE
            foreach (string _Field in Fields)
            {
                if (GetColumn(_Field, true).IsPrimaryKey)
                {
                    _PreTest = true;
                    _PrimaryKey = _Field;
                    break;
                }
            }
            if (_PreTest == false)
            {
                return "";
            }
            string _SPTextFinal = "";
            _SPTextFinal = _SPTextFinal + "Update " + this.Name + " Set ";
            foreach (string _Field in Fields)
            {
                if (!GetColumn(_Field, true).IsPrimaryKey)
                {
                    _SPTextFinal = _SPTextFinal + "[" + _Field + "] = @" + _Field + ",";
                }
            }
            _SPTextFinal = _SPTextFinal.TrimEnd(",".ToCharArray());
            _SPTextFinal = _SPTextFinal + " Where [" + _PrimaryKey + "] = @" + _PrimaryKey;
            return _SPTextFinal;
        }

        /// <summary>
        /// Trys to move a Column Down (CaseSensitive)
        /// </summary>
        /// <param name="ColName">Column Name</param>
        public void MoveColumnDown(string ColName)
        {
            BindingList<I_DbColumn> _NewList = new BindingList<I_DbColumn>();
            int CurrentPosition = 0;
            foreach (I_DbColumn _D in _Columns)
            {
                if (_D.Name == ColName)
                {
                    break;
                }
                CurrentPosition = CurrentPosition + 1;
            }
            if (CurrentPosition >= _Columns.Count) { return; }
            for (int x = 0; x < CurrentPosition; x++)
            {
                _NewList.Add(_Columns[x]);
            }
            _NewList.Add(_Columns[CurrentPosition + 1]);
            _NewList.Add(_Columns[CurrentPosition]);
            for (int x = CurrentPosition + 2; x < _Columns.Count; x++)
            {
                _NewList.Add(_Columns[x]);
            }
            _Columns = _NewList;
        }

        /// <summary>
        /// Tries to move a Column Up (Case Sensitive)
        /// </summary>
        /// <param name="ColName">Column Name to Move</param>
        public void MoveColumnUp(string ColName)
        {
            BindingList<I_DbColumn> _NewList = new BindingList<I_DbColumn>();
            int CurrentPosition = 0;
            foreach (I_DbColumn _D in _Columns)
            {
                if (_D.Name == ColName)
                {
                    break;
                }
                CurrentPosition = CurrentPosition + 1;
            }
            if (CurrentPosition == 0) { return; }
            for (int x = 0; x < CurrentPosition - 1; x++)
            {
                _NewList.Add(_Columns[x]);
            }
            _NewList.Add(_Columns[CurrentPosition]);
            _NewList.Add(_Columns[CurrentPosition - 1]);
            for (int x = CurrentPosition + 1; x < _Columns.Count; x++)
            {
                _NewList.Add(_Columns[x]);
            }
            _Columns = _NewList;
        }

        /// <summary>
        /// Removes a Column at the specified Index
        /// </summary>
        /// <param name="Index">Index to remove at</param>
        public void RemoveColumn(int Index)
        {
            if (_Columns.Count > Index)
            {
                _Columns.RemoveAt(Index);
            }
        }

        /// <summary>
        /// Removes a Column by Name
        /// </summary>
        /// <param name="Name">Name of Column to Remove</param>
        /// <param name="IgnoreCase">Ignore the Case</param>
        public void RemoveColumn(string Name, bool IgnoreCase)
        {
            for (int x = 0; x < _Columns.Count; x++)
            {
                if (IgnoreCase)
                {
                    if (_Columns[x].Name.ToLower() == Name.ToLower())
                    {
                        _Columns.RemoveAt(x);
                    }
                }
                else
                {
                    if (_Columns[x].Name == Name)
                    {
                        _Columns.RemoveAt(x);
                    }
                }
            }
        }

        /// <summary>
        /// Trys to remove a Foreign Key by Index
        /// </summary>
        /// <param name="Index"></param>
        public void Remove_Relationship(int Index)
        {
            if (_Relationships.Count > Index)
            {
                _Relationships.RemoveAt(Index);
            }
        }

        /// <summary>
        /// Trys to remove a foreignKey by Name
        /// </summary>
        /// <param name="Name">Name of ForeignKey</param>
        /// <param name="IgnoreCase">Ignore Case?</param>
        public void Remove_Relationship(string Name, bool IgnoreCase)
        {
            for (int x = 0; x < _Relationships.Count; x++)
            {
                if (IgnoreCase)
                {
                    if (_Relationships[x].RelationshipName.ToLower() == Name.ToLower())
                    {
                        _Relationships.RemoveAt(x);
                    }
                }
                else
                {
                    if (_Relationships[x].RelationshipName == Name)
                    {
                        _Relationships.RemoveAt(x);
                    }
                }
            }
        }



        // Private Methods (1) 

        /// <summary>
        /// This Method is used to Trigger Change Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ListChanged(object sender, ListChangedEventArgs e)
        {
            HasChanged = true;
        }


        #endregion Methods

        #region ICore Members

        /// <summary>
        /// Override the ExportXML to Provide XML Functionality
        /// </summary>
        /// <returns></returns>
        public override string ExportXMLData()
        {
            StringBuilder _TmpExport = new StringBuilder("\t<Table name=\"");
            _TmpExport.Append(_Name + "\"");
            _TmpExport.Append(" PrimaryKeyColumn=\"" + _PrimaryKeyName + "\" ");
            _TmpExport.Append(" ShortName=\"" + _ShortName + "\" ");
            _TmpExport.Append(" Owner=\"" + _Owner + "\" ");
            _TmpExport.Append(">\n\r");

            _TmpExport.Append("\t\t<Columns>\n\r");
            foreach (I_DbColumn _C in _Columns)
            {
                _TmpExport.Append(_C.ExportXMLData());
            }
            _TmpExport.Append("\t\t</Columns>\n\r");
            _TmpExport.Append("\t</Table>\n\r");

            return _TmpExport.ToString();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Override the Dispose to Provide Functionality
        /// </summary>
        public override void Dispose()
        {
            _Columns = null;
            _Relationships = null;
            _Name = null;
            _PrimaryKeyName = null;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// Compares One Object To Another ONLY SUPPORTS SPECIFIC CLASS IMPLEMENTATIONs
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is ACT_IDbTable)
            {
                return (obj as ACT_IDbTable).Name.CompareTo(Name);
            }
            else
            {
                throw new ArgumentException("Object is not DIP_IDbTable");
            }
        }

        #endregion

        #region Operator Overloads

        /// <summary>
        /// My Implementation of the == Operator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(ACT_IDbTable x, ACT_IDbTable y)
        {
            foreach (I_DbColumn _TmpColumn in x._Columns)
            {
                if (_TmpColumn.CompareTo(y.GetColumn(_TmpColumn.Name, false)) != 0)
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// My Implementation of the != Operator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(ACT_IDbTable x, ACT_IDbTable y)
        {
            if (x == y) { return false; }

            return true;
        }

        #endregion
    }
}
