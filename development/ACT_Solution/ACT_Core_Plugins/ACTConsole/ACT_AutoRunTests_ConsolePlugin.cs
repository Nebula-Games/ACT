using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Common;

namespace ACT.Plugins.ACTConsole
{
    /// <summary>
    /// All Auto Run Test Methods
    /// </summary>
    public class ACT_AutoRunTests_ConsolePlugin : ACT.Core.Interfaces.ACTConsole.I_ConsoleItemClicked
    {
        /// <summary>
        /// Execute Menu Command
        /// </summary>
        /// <param name="MenuCommand">Menu Command</param>
        /// <param name="OtherData">Other Data Entered In Command Line</param>
        /// <returns></returns>
        public I_TestResultExpanded ExecuteMenuCommand(string MenuCommand, string[] OtherData)
        {
            var _tmpReturn = new ACT.Plugins.Common.ACT_ExtendedTestResults();

            switch (MenuCommand)
            {
                case "ACTEREP":
                    string _template = "as asdasd asd asdas da s ###FIRSTNAME### ###LASTNAME### asdasdas ###SOE### asdasdas ###RECORDCOUNT###";
                    Dictionary<string, string> _tmpReplacements = new Dictionary<string, string>();
                    _tmpReplacements.Add("RecordCount", "12");
                    ACT.Plugins.DataAccess.ACT_QueryResult _tmoResult = new DataAccess.ACT_QueryResult();
                    System.Data.DataTable _newTable = new System.Data.DataTable();
                    _newTable.TableName = "Test Table";
                    _newTable.Columns.Add("FirstName", typeof(string));
                    _newTable.Columns.Add("LastName", typeof(string));
                    _newTable.Columns.Add("Email", typeof(string));
                    var _NewRow = _newTable.NewRow();
                    _NewRow["FirstName"] = "Mark";
                    _NewRow["LastName"] = "Alicz";
                    _NewRow["Email"] = "MarkAlicz@gmail.com";
                    _newTable.Rows.Add(_NewRow);
                    _tmoResult.Tables.Add(_newTable);
                    

                    var _Results = ACT.Core.TemplateEngine.ReplacementEngine.Process(_template, _tmoResult, _tmpReplacements);

                    if (_Results[0] == "as asdasd asd asdas da s Mark Alicz asdasdas  asdasdas 12")
                    {
                        Console.WriteLine("Test Passed !!");
                    }
                    else
                    {
                        Console.WriteLine("Test Failed !!");
                        Console.WriteLine(_Results[0]);
                    }

                    Console.ReadKey();
                    break;
                default:
                    break;
            }

            return _tmpReturn;
        }
    }
}
