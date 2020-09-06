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

namespace ACT.Plugins.DataAccess
{
    /// <summary>
    /// Represents a Where Statement
    /// </summary>
    public class ACT_IDbWhereStatemet : ACT.Plugins.ACT_Core, I_DbWhereStatement
    {
        /// <summary>
        /// Blanket Constructor
        /// </summary>
        public ACT_IDbWhereStatemet()
        {

        }
        private string _ColumnName;

        /// <summary>
        /// Overridden by IDBColumn Value if Set
        /// </summary>
        public string ColumnName
        {
            get
            {
                if (Column != null)
                {
                    return Column.Name;
                }
                return _ColumnName;
            }
            set { _ColumnName = value; }
        }

        private System.Data.DbType _ColumnDataType;

        public System.Data.DbType ColumnDataType
        {
            get
            {
                if (Column != null)
                {
                    return Column.DataType;
                }
                return _ColumnDataType;
            }
            set { _ColumnDataType = value; }
        }

        #region IDbWhereStatement Members
        bool _UseAND = false;              
        List<I_DbWhereStatement> _Children = new List<I_DbWhereStatement>();                       
        Operators _StatementOperators;
        I_DbColumn _Column;
        object _Value;

        public void Fill(string FQColumnName, string ShortName, object value, System.Data.DbType DataType, Operators Operator)
        {
            _Column = new ACT_IDbColumn();
            _Column.ShortName = ShortName;
            _Column.Name = FQColumnName;
            _Column.DataType = DataType;
            this.StatementOperators = Operator;
            this.Value = value;
        }
        public void AddChild(string FQColumnName, object Value)
        {

        }

        public I_DbWhereStatement AddChild(string ColumnName, System.Data.DbType DataType, object Value, bool UseAndWithParent)
        {
            var _TmpNewWhere = new ACT_IDbWhereStatemet();
            _TmpNewWhere._ColumnName = ColumnName;
            _TmpNewWhere.ColumnDataType = DataType;
            _TmpNewWhere.Value = Value;
            _TmpNewWhere.UseAND = UseAndWithParent;
            this.Children.Add(_TmpNewWhere);
            return _TmpNewWhere;
        }
       
        public I_DbWhereStatement GenerateFrom(Dictionary<I_DbColumn, object> FieldsAndValues)
        {
            I_DbWhereStatement _TmpReturn = new ACT_IDbWhereStatemet();

            bool _First = true;

            foreach (var Col in FieldsAndValues.Keys)
            {

                if (_First)
                {
                    _TmpReturn.Column = Col;
                    _TmpReturn.UseAND = true;
                    _TmpReturn.Value = FieldsAndValues[Col];
                    _TmpReturn.StatementOperators = Operators.Equals;
                    _First = false;
                }
                else
                {
                    I_DbWhereStatement _ChildW = new ACT_IDbWhereStatemet();
                    _ChildW.Column = Col;
                    _ChildW.UseAND = true;
                    _ChildW.Value = FieldsAndValues[Col];
                    _ChildW.StatementOperators = Operators.Equals;
                    _TmpReturn.Children.Add(_ChildW);
                }

            }


            return _TmpReturn;
        }

        public bool HasChildren
        {
            get { if (_Children.Count > 0) { return true; } else { return false; } }
        }

        public List<I_DbWhereStatement> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        public bool UseAND
        {
            get { return _UseAND; }
            set { _UseAND = value; }
        }

        public Operators StatementOperators
        {
            get
            {
                return _StatementOperators;
            }
            set
            {
                _StatementOperators = value;
            }
        }

        public I_DbColumn Column
        {
            get
            {
                return _Column;
            }
            set
            {
                _Column = value;
            }
        }

        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        #endregion

      

        #region IDisposable Members

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

       

        /// <summary>
        /// This class is always healthy as there are no dependancies required.
        /// </summary>
        /// <returns>Healthy Test Result</returns>
        public override  I_TestResult HealthCheck()
        {
            return ACT.Core.CurrentCore<I_TestResult>.GetCurrent();
        }
    }
}
