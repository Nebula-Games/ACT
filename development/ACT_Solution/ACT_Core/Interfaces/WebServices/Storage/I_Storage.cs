// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Storage.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
#if DOTNETFRAMEWORK
using System.ServiceModel.Web;
#endif
using System.Text;
using ACT.Core.Interfaces.WebServices.Storage;

namespace ACT.Core.Interfaces.WebServices.Storage
{
    /// <summary>
    /// Interface I_Storage
    /// </summary>
    [ServiceContract]
    public interface I_Storage
    {
        /// <summary>
        /// Gets the raw location.
        /// </summary>
        /// <param name="ShortcutName">Name of the shortcut.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string GetRawLocation(string ShortcutName, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <param name="LocationName">Name of the location.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_Location.</returns>
        [OperationContract]
        I_Location GetLocation(string LocationName, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the location raw.
        /// </summary>
        /// <param name="RawLocation">The raw location.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_Location.</returns>
        [OperationContract]
        I_Location GetLocationRaw(string RawLocation, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the file by shortcut.
        /// </summary>
        /// <param name="ShortcutName">Name of the shortcut.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_File.</returns>
        [OperationContract]
        I_File GetFileByShortcut(string ShortcutName, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="LocationName">Name of the location.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_File.</returns>
        [OperationContract]
        I_File GetFile(string FileName, string LocationName, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the file raw.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="RawLocation">The raw location.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_File.</returns>
        [OperationContract]
        I_File GetFileRaw(string FileName, string RawLocation, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the file full raw.
        /// </summary>
        /// <param name="FullRawLocation">The full raw location.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_File.</returns>
        [OperationContract]
        I_File GetFileFullRaw(string FullRawLocation, string SecurityToken, string APIKey);

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="ModifiedFile">The modified file.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string SaveFile(I_File ModifiedFile, string SecurityToken, string APIKey);

        /// <summary>
        /// Gets all file shortcuts.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String[].</returns>
        [OperationContract]
        string[] GetAllFileShortcuts(string SecurityToken, string APIKey);

        /// <summary>
        /// Gets all location shortcuts.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String[].</returns>
        [OperationContract]
        string[] GetAllLocationShortcuts(string SecurityToken, string APIKey);

        /// <summary>
        /// Deletes the file by full location.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="FullRawLocation">The full raw location.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string DeleteFileByFullLocation(string SecurityToken, string FullRawLocation, string APIKey);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Name">The name.</param>
        /// <param name="RawLocation">The raw location.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string DeleteFile(string SecurityToken, string Name, string RawLocation, string APIKey);

        /// <summary>
        /// Deletes the location.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="RawLocation">The raw location.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="RecursiveDelete">if set to <c>true</c> [recursive delete].</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string DeleteLocation(string SecurityToken, string RawLocation, string APIKey, bool RecursiveDelete = false);

        /// <summary>
        /// Evaluates the permission.
        /// </summary>
        /// <param name="Read">if set to <c>true</c> [read].</param>
        /// <param name="Write">if set to <c>true</c> [write].</param>
        /// <param name="Modify">if set to <c>true</c> [modify].</param>
        /// <param name="RawLocation">The raw location.</param>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool EvaluatePermission(bool Read, bool Write, bool Modify, string RawLocation, string SecurityToken, string APIKey);

#region String Operations

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Name">The name.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="Group">The group.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string GetStringValue(string SecurityToken, string Name, string APIKey, string Group = "");

        /// <summary>
        /// Gets the string groups.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String[].</returns>
        [OperationContract]
        string[] GetStringGroups(string SecurityToken, string APIKey);

        /// <summary>
        /// Gets the string group names.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Group">The group.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String[].</returns>
        [OperationContract]
        string[] GetStringGroupNames(string SecurityToken, string Group, string APIKey);

        /// <summary>
        /// Sets the string.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Value">The value.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="Group">The group.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string SetString(string SecurityToken, string Name, string Value, string APIKey, string Group = "");

        /// <summary>
        /// Deletes the string.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Name">The name.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="Group">The group.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string DeleteString(string SecurityToken, string Name, string APIKey, string Group = "");

        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="ParentRawLocation">The parent raw location.</param>
        /// <param name="Name">The name.</param>
        /// <param name="VersionControl">if set to <c>true</c> [version control].</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string AddLocation(string SecurityToken, string ParentRawLocation, string Name, bool VersionControl, string APIKey);


#endregion

    }
}
