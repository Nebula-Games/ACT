// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DB_Table_Archive.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Interfaces.Common;
using System.Collections.Generic;


namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface I_DB_Table_Archive
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_DB_Table_Archive : I_Plugin
    {
        /// <summary>
        /// Generates the archive SQL.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <returns>System.String.</returns>
        string GenerateArchiveSQL(string TableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        List<string> GetDependantTables(string TableName);

        /// <summary>
        /// Archives all data. Returns a Base64 Encoded Package Of Data
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="EncryptionKey">The encryption key.</param>
        /// <param name="PluginConfigInfo">The plugin configuration information.</param>
        /// <returns>System.String.</returns>
        string ArchiveAllData(string TableName, string EncryptionKey = "", ACT.Core.Types.ACTConfig.Plugin PluginConfigInfo = null);
        
        /// <summary>
        /// Archive To Package
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="EncryptionKey"></param>
        /// <param name="PluginConfigInfo"></param>
        /// <returns></returns>
        ACT.Core.Encoding.ACTPackaging.ACT_Package ArchiveToPackage(string TableName, string EncryptionKey = "", ACT.Core.Types.ACTConfig.Plugin PluginConfigInfo = null);
    }
}
