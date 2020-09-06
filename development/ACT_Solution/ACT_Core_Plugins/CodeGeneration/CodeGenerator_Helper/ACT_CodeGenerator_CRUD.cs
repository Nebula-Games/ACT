// ***********************************************************************
// Assembly         : ACTPlugins
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="ACT_CodeGenerator.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Extensions.CodeGenerator;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;
using System;
using System.Text;


namespace ACT.Plugins.CodeGeneration
{
    /// <summary>
    /// Internal Code Generation Class Generates C# Code
    /// Implements the <see cref="ACT.Plugins.ACT_Core" />
    /// Implements the <see cref="ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator" />
    /// </summary>
    /// <seealso cref="ACT.Plugins.ACT_Core" />
    /// <seealso cref="ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator" />
    public partial class ACT_CodeGenerator : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator
    {
        /// <summary>
        /// Generates the Update Method based on the table
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns></returns>
        private string GenerateUpdateMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public virtual I_QueryResult Update()" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){ " + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("" + Environment.NewLine);
            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("string SQLInsertFields = \"\";" + Environment.NewLine);
            _TmpBuilder.Append("string SQLValues = \"\";" + Environment.NewLine);

            foreach (var C in Table.Columns)
            {
                if (C.DataType == System.Data.DbType.Guid && C.IsPrimaryKey == false)
                {

                    _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " == Guid.Empty){" + Environment.NewLine);
                    _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", DBNull.Value));" + Environment.NewLine);
                    _TmpBuilder.Append("SQLInsertFields += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param, \";" + Environment.NewLine);
                    _TmpBuilder.Append("}");
                    _TmpBuilder.Append("else if (this." + C.ShortName.ToCSharpFriendlyName() + " != null) {" + Environment.NewLine);
                    _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));" + Environment.NewLine);
                    _TmpBuilder.Append("SQLInsertFields += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param, \";" + Environment.NewLine);
                    _TmpBuilder.Append("}" + Environment.NewLine);

                }
                else
                {
                    _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " != null){ " + Environment.NewLine);

                    if (C.Name.ToLower() == "datemodified")
                    {
                        _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", DateTime.Now));" + Environment.NewLine);
                    }
                    else
                    {
                        _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));" + Environment.NewLine);
                    }

                    if (C.IsPrimaryKey)
                    {
                        _TmpBuilder.Append("SQLValues += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param AND \";" + Environment.NewLine);
                    }
                    else
                    {
                        _TmpBuilder.Append("SQLInsertFields += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param, \";" + Environment.NewLine);
                    }
                    _TmpBuilder.Append("}" + Environment.NewLine);
                }



            }

            _TmpBuilder.Append("SQLInsertFields = SQLInsertFields.TrimEnd(\", \".ToCharArray());" + Environment.NewLine);
            _TmpBuilder.Append("SQLValues = SQLValues.TrimEnd(\"AND \".ToCharArray());" + Environment.NewLine);

            _TmpBuilder.Append("string _SQL = \"Update [" + Table.ShortName.ToCSharpFriendlyName() + "] set \" + SQLInsertFields + \" Where \" + SQLValues;" + Environment.NewLine);

            _TmpBuilder.Append("var _TmpReturn = DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);" + Environment.NewLine);
            _TmpBuilder.Append("\n\rreturn _TmpReturn;" + Environment.NewLine);
            _TmpBuilder.Append("\t}" + Environment.NewLine + "}" + Environment.NewLine);


            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns></returns>
        private string GenerateDeleteMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public virtual void Delete()" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine);
            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){ " + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("" + Environment.NewLine);
            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            // _TmpBuilder.Append("string SQLInsertFields = \"\";" + Environment.NewLine);
            _TmpBuilder.Append("string SQLValues = \"\";" + Environment.NewLine);

            foreach (var C in Table.Columns)
            {
                _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " != null){ " + Environment.NewLine);


                if (C.IsPrimaryKey)
                {
                    _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));" + Environment.NewLine);

                    _TmpBuilder.Append("SQLValues += \"[" + C.ShortName + "] = @" + C.ShortName.ToCSharpFriendlyName() + "_Param AND \";" + Environment.NewLine);
                }

                _TmpBuilder.Append("}" + Environment.NewLine);
            }

            // _TmpBuilder.Append("SQLInsertFields = SQLInsertFields.TrimEnd(\", \".ToCharArray());" + Environment.NewLine);
            _TmpBuilder.Append("SQLValues = SQLValues.TrimEnd(\"AND \".ToCharArray());" + Environment.NewLine);

            _TmpBuilder.Append("string _SQL = \"Delete From [" + Table.ShortName.ToCSharpFriendlyName() + "] Where \" + SQLValues;" + Environment.NewLine);

            _TmpBuilder.Append("DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);" + Environment.NewLine);
            _TmpBuilder.Append("}}" + Environment.NewLine);


            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// Generates the Create Method for the Table
        /// </summary>
        /// <param name="Table">ACT.Core.Interfaces.DataAccess.IDbTable - Table Information</param>
        /// <param name="CodeSettings">ACT.Core.Interfaces.DataAccess.ICodeGenerationSettings - Code Settings</param>
        /// <returns></returns>
        private string GenerateCreateMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            StringBuilder _TmpBuilder = new StringBuilder();

            _TmpBuilder.Append("public virtual I_QueryResult Create()" + Environment.NewLine);
            _TmpBuilder.Append("\t{" + Environment.NewLine);
            _TmpBuilder.Append("using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent()){ " + Environment.NewLine);
            _TmpBuilder.Append("\t\tif (LocalConnectionString == \"\") {" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(GenericStaticClass.DatabaseConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\telse { " + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("\t\t\tDataAccess.Open(LocalConnectionString);" + Environment.NewLine);
            _TmpBuilder.Append("\t\t}" + Environment.NewLine + Environment.NewLine);
            _TmpBuilder.Append("" + Environment.NewLine);
            _TmpBuilder.Append("List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();" + Environment.NewLine);

            _TmpBuilder.Append("string SQLInsertFields = \"\";" + Environment.NewLine);
            _TmpBuilder.Append("string SQLValues = \"\";" + Environment.NewLine);


            bool _ExpectReturnInteger = false;

            foreach (var C in Table.Columns)
            {
                if (C.IsPrimaryKey)
                {
                    if (C.DataType == System.Data.DbType.Guid)
                    {
                        _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " == null) { this." + C.ShortName.ToCSharpFriendlyName() + " = Guid.NewGuid(); }");
                    }
                    else
                    {
                        _ExpectReturnInteger = true;
                    }
                }

                _TmpBuilder.Append("if (this." + C.ShortName.ToCSharpFriendlyName() + " != null){ " + Environment.NewLine);

                _TmpBuilder.Append("_Params.Add(new System.Data.SqlClient.SqlParameter(\"" + C.ShortName.ToCSharpFriendlyName() + "_Param\", this." + C.ShortName.ToCSharpFriendlyName() + "));" + Environment.NewLine);

                _TmpBuilder.Append("SQLInsertFields += \"[" + C.ShortName + "], \";" + Environment.NewLine);
                _TmpBuilder.Append("SQLValues += \"@" + C.ShortName.ToCSharpFriendlyName() + "_Param, \";" + Environment.NewLine);
                _TmpBuilder.Append("}" + Environment.NewLine);
            }

            _TmpBuilder.Append("SQLInsertFields = SQLInsertFields.TrimEnd(\", \".ToCharArray());" + Environment.NewLine);
            _TmpBuilder.Append("SQLValues = SQLValues.TrimEnd(\", \".ToCharArray());" + Environment.NewLine);

            if (_ExpectReturnInteger)
            {
                _TmpBuilder.Append("string _SQL = \"Insert Into [" + Table.ShortName.ToCSharpFriendlyName() + "] ( \" + SQLInsertFields + \") Values (\" + SQLValues + \"); SELECT SCOPE_IDENTITY() as ID; \";" + Environment.NewLine);
                _TmpBuilder.Append("var _TmpReturn = DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);" + Environment.NewLine);

                _TmpBuilder.Append("try" + Environment.NewLine);
                _TmpBuilder.Append("{" + Environment.NewLine);
                _TmpBuilder.Append("    this.ID = _TmpReturn.FirstDataTable_WithRows().Rows[0][0].ToInt(-1);" + Environment.NewLine);
                _TmpBuilder.Append("    if (this.ID == -1)" + Environment.NewLine);
                _TmpBuilder.Append("    {" + Environment.NewLine);
                _TmpBuilder.Append("        this.ID = null;" + Environment.NewLine);
                _TmpBuilder.Append("        this.LogError(this.GetType().FullName, \"Error Grabbing The ID\", null, \"\", ErrorLevel.Critical);" + Environment.NewLine);
                _TmpBuilder.Append("    }" + Environment.NewLine);
                _TmpBuilder.Append("}" + Environment.NewLine);
                _TmpBuilder.Append("catch (Exception ex)" + Environment.NewLine);
                _TmpBuilder.Append("{" + Environment.NewLine);
                _TmpBuilder.Append("    this.LogError(this.GetType().FullName, \"Error Grabbing The ID\", ex, \"\", ErrorLevel.Critical);" + Environment.NewLine);
                _TmpBuilder.Append("}" + Environment.NewLine);

                _TmpBuilder.Append("\n\rreturn _TmpReturn;" + Environment.NewLine);
                _TmpBuilder.Append("\t}" + Environment.NewLine + "}" + Environment.NewLine);
            }
            else
            {
                _TmpBuilder.Append("string _SQL = \"Insert Into [" + Table.ShortName.ToCSharpFriendlyName() + "] ( \" + SQLInsertFields + \") Values (\" + SQLValues + \")\";" + Environment.NewLine);
                _TmpBuilder.Append("var _TmpReturn = DataAccess.RunCommand(_SQL, _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);" + Environment.NewLine);
                _TmpBuilder.Append("\n\rreturn _TmpReturn;" + Environment.NewLine);
                _TmpBuilder.Append("\t}" + Environment.NewLine + "}" + Environment.NewLine);
            }
           


            return _TmpBuilder.ToString();
        }


        /// <summary>
        /// Generates the blank update method.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateBlankUpdateMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder();
            _TmpBuilder.Append("public virtual void Update()" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine + " // No Primary Keys Found Must Define Manually \n }");
            return _TmpBuilder.ToString();
        }

        /// <summary>
        /// Generates the blank delete method.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateBlankDeleteMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            StringBuilder _TmpBuilder = new StringBuilder();
            _TmpBuilder.Append("public virtual void Delete()" + Environment.NewLine);
            _TmpBuilder.Append("{" + Environment.NewLine + " // No Primary Keys Found Must Define Manually \n }");
            return _TmpBuilder.ToString();
        }

    }
}
