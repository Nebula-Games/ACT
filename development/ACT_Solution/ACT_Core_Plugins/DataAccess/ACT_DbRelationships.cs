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
using System.Data;

namespace ACT.Plugins.DataAccess
{

    /// <summary>
    /// This represents a IDbRelationship
    /// </summary>
    public class ACT_IDbRelationships : ACT.Plugins.ACT_Core, I_DbRelationship
    {

        public ACT_IDbRelationships()
        {
            ColumnNames = new List<string>();
            ExternalColumnNames = new List<string>();
        }
        public bool MultiFieldRelationship { get; set; }
       
        public void AddColumn(string ColName,string ExternalColName)
        {
            ColumnNames.Add(ColName);
            ExternalColumnNames.Add(ExternalColName);                        
        }
        public List<string> ColumnNames { get; set; }
        public List<string> ExternalColumnNames { get; set; }

		#region Fields (8) 
                
        private string _ColumnName = "";
        private DbType _ColumnType;

        public DbType ColumnType
        {
            get { return _ColumnType; }
            set { _ColumnType = value; }
        }
        private string _External_ColumnName = "";
        private DbType _External_ColumnType;

        public DbType External_ColumnType
        {
            get { return _External_ColumnType; }
            set { _External_ColumnType = value; }
        }
        private string _External_TableName = "";
        private bool _IsForeignKey = false;
        private string _RelationshipName = "";
        private string _ShortExternal_TableName = "";
        private string _ShortTableName = "";
        private string _TableName = "";

		#endregion Fields 

		#region Methods (1) 


		// Public Methods (1) 

        /// <summary>
        /// Override the ExportXML so it functions
        /// </summary>
        /// <returns>XML String</returns>
        public override string ExportXMLData()
        {
            StringBuilder _TmpReturn = new StringBuilder();
            _TmpReturn.Append("<Relationship>");
            _TmpReturn.Append("\t<RelationshipName>" + _RelationshipName + "</RelationshipName>\n");
            _TmpReturn.Append("\t<External_ColumnName>" + _External_ColumnName + "</External_ColumnName>\n");
            _TmpReturn.Append("\t<External_TableName>" + _External_TableName + "</External_TableName>\n");

            _TmpReturn.Append("\t<ColumnName>" + _ColumnName + "</ColumnName>\n");
            _TmpReturn.Append("\t<TableName>" + _TableName + "</TableName>\n");
            _TmpReturn.Append("\t<IsForeignKey>" + _IsForeignKey + "</IsForeignKey>\n");
            _TmpReturn.Append("</Relationship>");

            return _TmpReturn.ToString();
        }


		#endregion Methods 
        
        #region IDbRelationship Members

        /// <summary>
        /// Short Name of Table Where Relationship is Found
        /// </summary>
        public string ShortTableName
        {
            get { return _ShortTableName; }
            set { _ShortTableName = value; }
        }

        /// <summary>
        /// Short Name of Table Where Relationship is Matched
        /// </summary>
        public string ShortExternal_TableName
        {
            get { return _ShortExternal_TableName; }
            set { _ShortExternal_TableName = value; }
        }

        /// <summary>
        /// Relationship Name..  Changed depending on Origin (FK_NAME,PK_NAME)
        /// </summary>
        public string RelationshipName
        {
            get
            {
                return _RelationshipName;
            }
            set
            {
                _RelationshipName = value; HasChanged = true;
            }
        }

        /// <summary>
        /// Table Name Where Relationship Matched (Fully Qualified)
        /// </summary>
        public string External_TableName
        {
            get
            {
                return _External_TableName;
            }
            set
            {
                _External_TableName = value; HasChanged = true;
            }
        }

        /// <summary>
        /// Column Name Where Relationship Is Matched
        /// </summary>
        public string External_ColumnName
        {
            get
            {
                return _External_ColumnName;
            }
            set
            {
                _External_ColumnName = value; HasChanged = true;
            }
        }

        /// <summary>
        /// Specifies if this relationship is a ForeignKey or not Depending on Origin
        /// </summary>
        public bool IsForeignKey
        {
            get
            {
                return _IsForeignKey;
            }
            set
            {
                _IsForeignKey = value; HasChanged = true;
            }
        }

        /// <summary>
        /// Column Name Where Relationship Is Found
        /// </summary>
        public string ColumnName
        {
            get
            {
                return _ColumnName;
            }
            set
            {
                _ColumnName = value; HasChanged = true;
            }
        }

        /// <summary>
        /// Table Name Where Relationship Is Found (Fully Qualified)
        /// </summary>
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value; HasChanged = true;
            }
        }

        #endregion
    }
}
