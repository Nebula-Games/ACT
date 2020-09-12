using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces;
using ACT.Core;
using ACT.Core.Enums;


namespace ACT.Plugins.DataAccess
{

    /// <summary>
    /// This class Implements the IDB Interface
    /// </summary>
    public class ACT_IDb : ACT.Plugins.ACT_Core, I_Db
    {
        #region Fields (3)

        private I_UserInfo _Current_UserInfo;
        private string _Name = "";
        private BindingList<I_DbTable> _Tables = new BindingList<I_DbTable>();
        private List<I_DbView> _Views = new List<I_DbView>();
        private List<I_DbDataType> _Types = new List<I_DbDataType>();        
        private List<I_DbStoredProcedure> _Procedures = new List<I_DbStoredProcedure>();

        /// <summary>
        /// Get / Set the Tables
        /// </summary>
        public BindingList<I_DbTable> Tables
        {
            get
            {
                return _Tables;
            }
            set
            {
                _Tables = value;
            }
        }

        /// <summary>
        /// Get / Set the DB Views
        /// </summary>
        public List<I_DbView> Views
        {
            get
            {
                return _Views;
            }
            set
            {
                _Views = value;
            }
        }

        /// <summary>
        /// Get / Set the DB Types
        /// </summary>
        public List<I_DbDataType> Types
        {
            get { return _Types; }
            set { _Types = value; }
        }

        /// <summary>
        /// Get / Set the DB Stored Procedures
        /// </summary>
        public List<I_DbStoredProcedure> StoredProcedures
        {
            get { return _Procedures; }
            set { _Procedures = value; }
        }

        /// <summary>
        /// Returns the Table Names
        /// </summary>
        public List<string> TableNames
        {
            get { return _Tables.Select(x => x.ShortName).ToList(); }
        }

        /// <summary>
        /// Returns the Stored Procedures
        /// </summary>
        public List<string> StoredProcedureNames
        {
            get
            {
                return _Procedures.Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Returns the Type Names
        /// </summary>
        public List<string> TypeNames
        {
            get
            {
                return Types.Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Returns the View Names
        /// </summary>
        public List<string> ViewNames
        {
            get
            {
                return _Views.Select(x => x.Name).ToList();
            }
        }

        /// <summary>
        /// Return the Number of Tables
        /// </summary>
        public int TableCount
        {
            get { return _Tables.Count; }
        }

        /// <summary>
        /// Return the Number of Views
        /// </summary>
        public int ViewCount
        {
            get { return _Views.Count; }
        }

        /// <summary>
        /// Gets the Data Access Class being Used
        /// </summary>
        public I_DataAccess DataAccessClass { get; set; }

        /// <summary>
        /// Get / Set the Database Name
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
            }
        }


        #endregion Fields

        #region Constructors

        /// <summary>
        /// Empty Contructor
        /// </summary>
        public ACT_IDb()
        {
            // Watch For Table Changes
            _Tables.ListChanged += new ListChangedEventHandler(_Tables_ListChanged);
        }

        #endregion Constructors



        #region Methods (3)

        

        /// <summary>
        /// Doesnt Follow Normal Logic.  May need to rewrite
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (this == (ACT_IDb)obj)
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generate a Hash Code Based On Object Values
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            // TODO Write Real Code Here Blame Joe
            int TmpReturn = 0;

            foreach (I_DbTable _TmpTable in _Tables)
            {
                foreach (char c in _TmpTable.Name)
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
        /// Validates the Current Structure of the Database
        /// </summary>
        /// <returns></returns>
        public I_TestResult Validate()
        {
            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();
            _TestResult.Success = true;
            _TestResult.Messages.Add("Always True");
            return _TestResult;
        }

        /// <summary>
        /// Gets a view based on the index of the Views Position
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public I_DbView GetView(int Index)
        {
            return _Views[Index];
        }

        /// <summary>
        /// Internal Get Table Index By Name (Case Insensitive)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>Index or -1</returns>
        internal int GetTableIndex(string Name)
        {
            for (int x = 0; x < _Tables.Count; x++)
            {
                if (_Tables[x].Name.ToLower() == Name.ToLower())
                {
                    return x;
                }
            }
            return -1;
        }

        /// <summary>
        /// Add a table to the Database.  This doesn't add to the database until you Save
        /// </summary>
        /// <param name="Table">Table To Add</param>
        /// <returns>I_TestResult Result of addition</returns>
        public I_TestResult AddTable(I_DbTable Table)
        {
            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();

            if (GetTable(Table.Name, true) != null)
            {
                _TestResult.Success = false;
                _TestResult.Messages.Add("Duplicate Table Found");
            }
            else
            {
                _Tables.Add(Table);
                _TestResult.Success = true;
            }
            return _TestResult;
        }

        /// <summary>
        /// Adds a view to the Database.  This doesn't add to the database until you Save
        /// </summary>
        /// <param name="View">View to Add</param>
        /// <returns>I_TestResult Result of the Addition</returns>
        public I_TestResult AddView(I_DbView View)
        {
            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();

            if (GetView(View.Name, true) != null)
            {
                _TestResult.Success = false;
                _TestResult.Messages.Add("Duplicate View Found");
            }
            else
            {
                _Views.Add(View);
                _TestResult.Success = true;
            }
            return _TestResult;
        }

        /// <summary>
        /// Remove a Table from the current definition
        /// </summary>
        /// <param name="Name">Fully Qualified Name of Table</param>
        /// <param name="IgnoreCase">Ignore Case?</param>
        /// <returns>I_TestResult</returns>
        public I_TestResult RemoveTable(string Name, bool IgnoreCase)
        {
            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();

            I_DbTable _TmpTable = GetTable(Name, true);
            if (_TmpTable != null)
            {
                _TestResult.Success = true;
                _Tables.RemoveAt(GetTableIndex(_TmpTable.Name));
            }
            else
            {
                _TestResult.Messages.Add("Table Not Found Found");
                _TestResult.Success = false;
            }
            return _TestResult;
        }

        /// <summary>
        /// Removes a table at Index. This doesn't add to the database until you Save
        /// </summary>
        /// <param name="Index"></param>
        /// <returns>I_TestResult Result of Removal</returns>
        public I_TestResult RemoveTable(int Index)
        {
            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();

            if ((Index < _Tables.Count && Index > -1))
            {
                _Tables.RemoveAt(Index);
                _TestResult.Success = true;
            }
            else
            {
                _TestResult.Messages.Add("Error in Index Value");
                _TestResult.Success = false;
            }
            return _TestResult;
        }

        /// <summary>
        /// Modify Table 
        /// </summary>
        /// <param name="Original">Original Table To Modify</param>
        /// <param name="New">New Table </param>
        /// <returns>I_TestResult with Modification Success</returns>
        public I_TestResult ModifyTable(I_DbTable Original, I_DbTable New)
        {
            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();

            int x = GetTableIndex(Original.Name);

            if (x == -1)
            {
                _TestResult.Messages.Add("Original Table Not Found");
                _TestResult.Success = false;
            }
            else
            {
                _Tables[x] = New;
                _TestResult.Success = true;
            }
            return _TestResult;
        }

        /// <summary>
        /// Replace Table With New Table
        /// </summary>
        /// <param name="Original">Name Case Insensitive</param>
        /// <param name="New"></param>
        /// <returns></returns>
        public I_TestResult ModifyTable(string Original, I_DbTable New)
        {

            I_TestResult _TestResult = CurrentCore<ACT.Core.Interfaces.Common.I_TestResult>.GetCurrent();
            int x = GetTableIndex(Original);

            if (x == -1)
            {
                _TestResult.Messages.Add("Original Table Not Found");
                _TestResult.Success = false;
            }
            else
            {
                _Tables[x] = New;
                _TestResult.Success = true;
            }
            return _TestResult;
        }

        /// <summary>
        /// Get table by name
        /// </summary>
        /// <param name="Name">Table Name</param>
        /// <param name="IgnoreCase">Ignore Table Name Case Sesitivity</param>
        /// <returns>IDbTable or Null if not found</returns>
        public I_DbTable GetTable(string Name, bool IgnoreCase)
        {
            foreach (I_DbTable _Table in _Tables)
            {
                if (_Table.Name == Name)
                {
                    return _Table;
                }
                else if (IgnoreCase == true && _Table.Name.ToLower() == Name.ToLower())
                {
                    return _Table;
                }
            }
            return null;
        }

        /// <summary>
        /// Get table by Index
        /// </summary>
        /// <param name="Index">Index of List</param>
        /// <returns>IDbTable or null if Index Not Found</returns>
        public I_DbTable GetTable(int Index)
        {
            return _Tables[Index];
        }

        /// <summary>
        /// Get table by name
        /// </summary>
        /// <param name="Name">Table Name</param>
        /// <param name="IgnoreCase">Ignore Table Name Case Sesitivity</param>
        /// <returns>IDbTable or Null if not found</returns>
        public I_DbView GetView(string Name, bool IgnoreCase)
        {
            foreach (I_DbView _View in _Views)
            {
                if (_View.Name == Name)
                {
                    return _View;
                }
                else if (IgnoreCase == true && _View.Name.ToLower() == Name.ToLower())
                {
                    return _View;
                }
            }
            return null;
        }


        #endregion Methods

        #region EVENT HANDLERS

        private void _Tables_ListChanged(object sender, ListChangedEventArgs e)
        {
            HasChanged = true;
        }

        #endregion
                
        #region IDisposable Members

        /// <summary>
        /// Disposes the Class
        /// </summary>
        public override void Dispose()
        {
            _Tables.Clear();
            _Tables = null;
        }

        #endregion

        #region IPlugin Members

        /// <summary>
        /// Sets the Impersonation of the User Making Database Commands Which are not implmented.
        /// </summary>
        /// <param name="UserInfo"></param>
        public override void SetImpersonate(I_UserInfo UserInfo)
        {
            _Current_UserInfo = UserInfo;
        }

        /// <summary>
        /// Returns a List of SystemSettingRequirements
        /// </summary>
        /// <returns></returns>
        public override List<string> ReturnSystemSettingRequirements()
        {               
            List<string> _tmpReturn = new List<string>();
            _tmpReturn.Add("I_DbColumn");
            _tmpReturn.Add("I_DbDataType");
            _tmpReturn.Add("I_DbStoredProcedure");
            _tmpReturn.Add("I_DbStoredProcedureParameter");
            _tmpReturn.Add("I_DbView");
            _tmpReturn.Add("I_DbTable");
            _tmpReturn.Add("I_DbWhereStatement");
            _tmpReturn.Add("I_DbRelationship");
            _tmpReturn.Add("I_DBObject");
                        
            return _tmpReturn;
        }

        /// <summary>
        /// Validate the Plugin
        /// </summary>
        /// <returns></returns>
        public override I_TestResult ValidatePluginRequirements()
        {
            var _TmpReturn = ACT.Core.SystemSettings.MeetsExpectations((ACT.Core.Interfaces.Common.I_Plugin)this);

            return _TmpReturn;
        }
        #endregion

        #region Operator Overloads

        /// <summary>
        /// My Implementation of the == Operator
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(ACT_IDb x, ACT_IDb y)
        {
            foreach (I_DbTable _TmpTable in x.Tables)
            {
                if (_TmpTable.CompareTo(y.GetTable(_TmpTable.Name, false)) != 0)
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
        public static bool operator !=(ACT_IDb x, ACT_IDb y)
        {
            if (x == y) { return false; }

            return true;
        }

        #endregion

        #region ICore Members

        /// <summary>
        /// Override the ExportXML to Provide XML Functionality
        /// </summary>
        /// <returns></returns>
        public override string ExportXMLData()
        {
            StringBuilder _TmpExport = new StringBuilder("<Database name=\"");
            _TmpExport.Append(_Name);
            _TmpExport.Append("\">\n\r");

            _TmpExport.Append("\t<Tables>\n\r");
            foreach (ACT_IDbTable _C in _Tables)
            {
                _TmpExport.Append(_C.ExportXMLData());
            }
            _TmpExport.Append("\t</Tables>\n\r");
            _TmpExport.Append("</Database>\n\r");

            return _TmpExport.ToString();
        }

        public void SetImpersonate(object UserInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
