// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_ParentChild_Data.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types
{
    /// <summary>
    /// Represents Basic Parent Child Data
    /// </summary>
    public class ACT_ParentChild_Data
    {
        /// <summary>
        /// ID Of The Object
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Parent ID
        /// </summary>
        /// <value>The parent identifier.</value>
        public string ParentID { get; set; }

        /// <summary>
        /// Display Text of the Object
        /// </summary>
        /// <value>The display text.</value>
        public string DisplayText { get; set; }
        
        /// <summary>
        /// Convert A List Of Classes To DataTables
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>System.Data.DataTable.</returns>
        public static System.Data.DataTable ToDataTable(List<ACT_ParentChild_Data> Data)
        {
            System.Data.DataTable tmpReturn = new System.Data.DataTable("ParentChildData");

            tmpReturn.Columns.Add("ID", typeof(string));
            tmpReturn.Columns.Add("ParentID", typeof(string));
            tmpReturn.Columns.Add("DisplayText", typeof(string));
            
            foreach (var pc in Data)
            {
                var _newRow = tmpReturn.NewRow();
                _newRow["ID"] = pc.ID;
                _newRow["DisplayText"] = pc.DisplayText;
                _newRow["ParentID"] = pc.ParentID;
                tmpReturn.Rows.Add(_newRow);
            }

            return tmpReturn;
        }

        /// <summary>
        /// Navigate URL
        /// </summary>
        public string NavigationURL { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        public string ImageURL { get; set; }

    }
}
