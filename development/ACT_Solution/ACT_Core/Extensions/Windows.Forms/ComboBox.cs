// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ComboBox.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class ComboBoxExtensions.
    /// </summary>
    public static class ComboBoxExtensions
    {
        #if DOTNETFRAMEWORK
        /// <summary>
        /// Gets the index by value.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="Value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetIndexByValue(this System.Windows.Forms.ComboBox c, string Value)
        {
            for (int x = 0; x < c.Items.Count; x++)
            {
                var citem = c.Items[x];

                var field = (System.Reflection.PropertyInfo)citem.GetType().GetMember(c.ValueMember)[0];
                
                if (field.GetValue(citem,null).ToString() == Value.ToString())
                {
                    return x;
                }

            }
            return -1;
        }
#endif
    }

    
}
