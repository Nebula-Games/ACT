// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="GenericExecutionReturn.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Database
{
    /// <summary>
    /// A generic execution return.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <remarks>Mark Alicz, 8/19/2016.</remarks>
    public class GenericExecutionReturn<T>
    {
        /// <summary>
        /// The has errors
        /// </summary>
        public bool HasErrors = false;
        /// <summary>
        /// The error messages
        /// </summary>
        public Dictionary<string, string> ErrorMessages = new Dictionary<string, string>();
        /// <summary>
        /// The inserted i ds
        /// </summary>
        public List<string> InsertedIDs = new List<string>();
        /// <summary>
        /// The updated i ds
        /// </summary>
        public List<string> UpdatedIDs = new List<string>();
        /// <summary>
        /// The return objects
        /// </summary>
        public List<T> ReturnObjects = new List<T>();
        /// <summary>
        /// The return object names
        /// </summary>
        public List<string> ReturnObjectNames = new List<string>();
    }

}
