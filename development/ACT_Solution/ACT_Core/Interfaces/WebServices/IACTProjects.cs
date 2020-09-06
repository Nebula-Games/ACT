// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="IACTProjects.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

using System.Text;

namespace ACT.Core.Interfaces.WebServices
{
    /// <summary>
    /// Class ACTProjectData.
    /// </summary>
    [DataContract]
    public class ACTProjectData
    {
        /// <summary>
        /// The XML
        /// </summary>
        string _XML = "";
        /// <summary>
        /// Gets or sets the XML.
        /// </summary>
        /// <value>The XML.</value>
        [DataMember]
        public string XML { get { return _XML; } set { _XML = value; } }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        public string ID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }
    }

    /// <summary>
    /// Interface IACTProjects
    /// </summary>
    [ServiceContract]
    public interface IACTProjects
    {
        /// <summary>
        /// Gets my projects.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>List&lt;ACTProjectData&gt;.</returns>
        [OperationContract]
        List<ACTProjectData> GetMyProjects(string SecurityToken, string APIKey);

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Project">The project.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool SaveProject(string SecurityToken, ACTProjectData Project, string APIKey);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="ID">The identifier.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool DeleteProject(string SecurityToken, string ID, string APIKey);

        /// <summary>
        /// Gets the name of the project by.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="Name">The name.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>ACTProjectData.</returns>
        [OperationContract]
        ACTProjectData GetProjectByName(string SecurityToken, string Name, string APIKey);

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>System.String.</returns>
        [OperationContract]
        string GetAllProjects(string SecurityToken, string APIKey);

        /// <summary>
        /// Saves all projects.
        /// </summary>
        /// <param name="SecurityToken">The security token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="OverwriteExisting">if set to <c>true</c> [overwrite existing].</param>
        /// <param name="XML">The XML.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [OperationContract]
        bool SaveAllProjects(string SecurityToken, string APIKey, bool OverwriteExisting, string XML);
    }

}
