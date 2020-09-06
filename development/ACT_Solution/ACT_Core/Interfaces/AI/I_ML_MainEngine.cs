// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ML_MainEngine.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.AI
{
    /// <summary>
    /// ACTAI Main Engine Definition
    /// Implements the <see cref="ACT.Core.Interfaces.IO.I_Exportable{System.String}" />
    /// Implements the <see cref="ACT.Core.Interfaces.IO.I_Saveable" />
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_ExecuteWithParameters" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.IO.I_Exportable{System.String}" />
    /// <seealso cref="ACT.Core.Interfaces.IO.I_Saveable" />
    /// <seealso cref="ACT.Core.Interfaces.Common.I_ExecuteWithParameters" />
    public interface I_ML_MainEngine : Interfaces.IO.I_Exportable<string>, Interfaces.IO.I_Saveable, Interfaces.Common.I_ExecuteWithParameters
    {
        /// <summary>
        /// Active Components Drive the Data Export Interface Requirements
        /// </summary>
        /// <value>The active components.</value>
        List<I_ML_Component> ActiveComponents { get; set; }

        /// <summary>
        /// Passive Components affect internal calculations using short term memory
        /// components can be both active and passive Called ActivlyPassive (Passive Agressive Components :)
        /// </summary>
        /// <value>The passive components.</value>
        List<I_ML_Component> PassiveComponents { get; set; }

        /// <summary>
        /// Load Data Into The System
        /// </summary>
        /// <param name="data">The data.</param>
        void ManuallyLoadData(I_Dataset data);

    }
}
