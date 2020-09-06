// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_History.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Security.Authentication;

namespace ACT.Core.Interfaces.CodeGeneration.History
{
    /// <summary>
    /// Interface I_History
    /// </summary>
    public interface I_History
    {
        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <param name="Type">The type.</param>
        /// <param name="StoragePath">The storage path.</param>
        void Create(I_UserInfo User, Enums.Serialization.SerializationType Type, string StoragePath);

        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
        bool IsLoaded { get; }
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; }
        /// <summary>
        /// Gets the serialized as.
        /// </summary>
        /// <value>The serialized as.</value>
        Enums.Serialization.SerializationType SerializedAs { get; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        List<I_HistoryItem> Items { get; set; }

        /// <summary>
        /// Loads the items.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="Path">The path.</param>
        void LoadItems(I_UserInfo User, string ProjectName, string Path = "");

        /// <summary>
        /// Gets the by date range.
        /// </summary>
        /// <param name="Start">The start.</param>
        /// <param name="End">The end.</param>
        /// <returns>List&lt;I_HistoryItem&gt;.</returns>
        List<I_HistoryItem> GetByDateRange(DateTime Start, DateTime End);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Date">The date.</param>
        /// <param name="OriginalPath">The original path.</param>
        void AddItem(string ProjectName, string FileName, string Password, string Date, string OriginalPath);
    }
}

