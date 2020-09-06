///-------------------------------------------------------------------------------------------------
// file:	Web\IIS\IIS_8_5.cs
//
// summary:	Implements the iis 8 5 class
///-------------------------------------------------------------------------------------------------

#if DOTNETFRAMEWORK
// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="IIS_8_5.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Web.Administration;
using ACT.Core.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace ACT.Core.Web.IIS
{
    /// <summary>
    /// IIS 8.5 Manager
    /// </summary>
    public class IIS8_5_Manager
    {
        /// <summary>
        /// The server name
        /// </summary>
        string _ServerName;

        /// <summary>
        /// Enum CreateSiteResult
        /// </summary>
        public enum CreateSiteResult
        {
            /// <summary>
            /// The site created
            /// </summary>
            SiteCreated = 1,
            /// <summary>
            /// The site already exists
            /// </summary>
            SiteAlreadyExists = 2,
            /// <summary>
            /// The required fields missing
            /// </summary>
            RequiredFieldsMissing = 3,
            /// <summary>
            /// The HTTPS requires a certificate
            /// </summary>
            HttpsRequiresACertificate = 4,
            /// <summary>
            /// The HTTPS certificate not found
            /// </summary>
            HttpsCertificateNotFound = 5
        }
        /// <summary>
        /// Enum StopStartSiteResult
        /// </summary>
        public enum StopStartSiteResult
        {
            /// <summary>
            /// The site stopped
            /// </summary>
            SiteStopped = 1,
            /// <summary>
            /// The site not found
            /// </summary>
            SiteNotFound = 2,
            /// <summary>
            /// The site started
            /// </summary>
            SiteStarted = 3,
            /// <summary>
            /// The site still stopping
            /// </summary>
            SiteStillStopping = 4,
            /// <summary>
            /// The site still starting
            /// </summary>
            SiteStillStarting = 5,
            /// <summary>
            /// The site start state unknown
            /// </summary>
            SiteStartStateUnknown = 6
        }
        /// <summary>
        /// Enum DotNetVersions
        /// </summary>
        public enum DotNetVersions
        {
            /// <summary>
            /// The version1 1
            /// </summary>
            Version1_1 = 1,
            /// <summary>
            /// The version2 0
            /// </summary>
            Version2_0 = 2,
            /// <summary>
            /// The version4 0
            /// </summary>
            Version4_0 = 3

        }
        /// <summary>
        /// Enum IdentityType
        /// </summary>
        public enum IdentityType
        {
            /// <summary>
            /// The user
            /// </summary>
            User = 1,
            /// <summary>
            /// The system
            /// </summary>
            System = 2,
            /// <summary>
            /// The service
            /// </summary>
            Service = 3,
            /// <summary>
            /// The net service
            /// </summary>
            NetService = 4,
            /// <summary>
            /// The application pool
            /// </summary>
            AppPool = 5

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IIS8_5_Manager"/> class.
        /// </summary>
        /// <param name="ServerName">Name of the server.</param>
        public IIS8_5_Manager(string ServerName)
        {
            _ServerName = ServerName;
        }

        /// <summary>
        /// Sites the exists.
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SiteExists(string SiteName)
        {
            using (ServerManager s = new ServerManager())
            {
                if (s.Sites.Where(x => x.Name == SiteName).Count() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets all sites.
        /// </summary>
        /// <param name="ServerName">Name of the server.</param>
        /// <returns>List&lt;IISSiteInfo&gt;.</returns>
        public List<IISSiteInfo> GetAllSites(string ServerName)
        {
            List<IISSiteInfo> _Sites = new List<IISSiteInfo>();

            ServerManager iisManager = new ServerManager();
            SiteCollection sites = iisManager.Sites;
            foreach (Site iisSite in sites)
            {
                foreach (Application iisApp in iisSite.Applications)
                {
                    IISSiteInfo _New = new IISSiteInfo();
                    _New.AppName = iisApp.ToString();
                    _New.AppPoolName = iisApp.ApplicationPoolName;
                    _New.SiteID = iisSite.Id.ToString();
                    _New.SiteName = iisSite.Name;
                    _New.Directories = String.Join(";", iisApp.VirtualDirectories.Select(a => a.PhysicalPath).ToArray());
                    _Sites.Add(_New);
                }
            }

            return _Sites;
        }
        /// <summary>
        /// Gets the site home directory.
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="WaitTime">The wait time.</param>
        /// <returns>System.String[].</returns>
        public string[] GetSiteHomeDirectory(string SiteName, int WaitTime)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var _SiteSearchResults = serverManager.Sites.Where(x => x.Name == SiteName);

                if (_SiteSearchResults.Count() == 1)
                {
                    return _SiteSearchResults.First().GetWebConfiguration().GetLocationPaths();
                }
            }
            return null;
        }

        /// <summary>
        /// Remove the host name for the SiteName
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="IP">The ip.</param>
        /// <param name="HostName">Name of the host.</param>
        /// <param name="protocal">The protocal.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool RemoveHostName(string SiteName, string IP, string HostName, string protocal)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var _BindingFind = serverManager.Sites.Where(x => x.Name == SiteName).First().Bindings.Where(x => x.Host == HostName && x.Protocol == protocal);

                if (_BindingFind.Count() != 1) { return false; }

                try
                {
                    var _SiteFind = serverManager.Sites.Where(x => x.Name == SiteName).First();
                    if (_SiteFind == null) { return false; }

                    _SiteFind.Bindings.Remove(_BindingFind.First());

                    return true;
                }
                catch { }

                return false;
            }
        }


        /// <summary>
        /// Adds the name of the host.
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="IP">The ip.</param>
        /// <param name="HostName">Name of the host.</param>
        /// <param name="protocal">The protocal.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddHostName(string SiteName, string IP, string HostName, string protocal)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var _BindingFind = serverManager.Sites.Where(x => x.Name == SiteName).First().Bindings.Where(x => x.Host == HostName && x.Protocol == protocal);

                if (_BindingFind.Count() == 1) { return false; }

                try
                {
                    var _SiteFind = serverManager.Sites.Where(x => x.Name == SiteName).First();

                    if (_SiteFind == null) { return false; }

                    var _Binding = _SiteFind.Bindings.Add(IP + ":" + HostName, protocal);

                    if (_Binding != null) { return true; }
                }
                catch { }

                return false;
            }
        }

        /// <summary>
        /// Determines whether [has host name] [the specified site name].
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="IP">The ip.</param>
        /// <param name="HostName">Name of the host.</param>
        /// <param name="protocal">The protocal.</param>
        /// <returns><c>true</c> if [has host name] [the specified site name]; otherwise, <c>false</c>.</returns>
        public bool HasHostName(string SiteName, string IP, string HostName, string protocal)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var _BindingFind = serverManager.Sites.Where(x => x.Name == SiteName).First().Bindings.Where(x => x.Host == HostName && x.Protocol == protocal);

                if (_BindingFind.Count() == 1) { return true; }
                else { return false; }
            }
        }

        /// <summary>
        /// Starts the site.
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="WaitTime">The wait time.</param>
        /// <returns>StopStartSiteResult.</returns>
        public StopStartSiteResult StartSite(string SiteName, int WaitTime)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var _SiteSearchResults = serverManager.Sites.Where(x => x.Name == SiteName);

                if (_SiteSearchResults.Count() == 1)
                {
                    if (_SiteSearchResults.First().State == ObjectState.Started) { return StopStartSiteResult.SiteStarted; }
                    if (_SiteSearchResults.First().State == ObjectState.Starting) { return StopStartSiteResult.SiteStillStopping; }

                    ObjectState _Result = _SiteSearchResults.First().Start();

                    TimeSpan _WaitTime = new TimeSpan(0, 0, WaitTime);
                    DateTime _StartTime = DateTime.Now;

                    if (_Result == ObjectState.Starting)
                    {
                        while (_SiteSearchResults.First().State == ObjectState.Starting)
                        {
                            if (DateTime.Now - _StartTime > _WaitTime) { break; }
                        }
                    }

                    if (_Result == ObjectState.Started) { return StopStartSiteResult.SiteStarted; }
                    else if (_Result == ObjectState.Starting) { return StopStartSiteResult.SiteStillStarting; }
                    else if (_Result == ObjectState.Unknown) { return StopStartSiteResult.SiteStartStateUnknown; }
                    else { return StopStartSiteResult.SiteStopped; }
                }
                else
                {
                    return StopStartSiteResult.SiteNotFound;
                }
            }
        }

        /// <summary>
        /// Stops the site.
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="WaitTime">The wait time.</param>
        /// <returns>StopStartSiteResult.</returns>
        public StopStartSiteResult StopSite(string SiteName, int WaitTime)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                var _SiteSearchResults = serverManager.Sites.Where(x => x.Name == SiteName);

                if (_SiteSearchResults.Count() == 1)
                {
                    if (_SiteSearchResults.First().State == ObjectState.Stopped) { return StopStartSiteResult.SiteStopped; }
                    if (_SiteSearchResults.First().State == ObjectState.Stopped) { return StopStartSiteResult.SiteStillStopping; }

                    ObjectState _Result = _SiteSearchResults.First().Stop();

                    TimeSpan _WaitTime = new TimeSpan(0, 0, WaitTime);
                    DateTime _StartTime = DateTime.Now;

                    if (_Result == ObjectState.Stopping)
                    {
                        while (_SiteSearchResults.First().State == ObjectState.Stopping)
                        {
                            if (DateTime.Now - _StartTime > _WaitTime) { break; }
                        }
                    }

                    if (_Result == ObjectState.Stopped) { return StopStartSiteResult.SiteStopped; }
                    else if (_Result == ObjectState.Stopping) { return StopStartSiteResult.SiteStillStopping; }
                    else if (_Result == ObjectState.Unknown) { return StopStartSiteResult.SiteStartStateUnknown; }
                    else { return StopStartSiteResult.SiteStarted; }
                }
                else
                {
                    return StopStartSiteResult.SiteNotFound;
                }
            }
        }

        /// <summary>
        /// Creates the site.
        /// </summary>
        /// <param name="SiteName">Name of the site.</param>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <param name="BindingProtocal">The binding protocal.</param>
        /// <param name="HostName">Name of the host.</param>
        /// <param name="IPAddress">The ip address.</param>
        /// <param name="PortNumber">The port number.</param>
        /// <param name="ServerAutoStart">if set to <c>true</c> [server automatic start].</param>
        /// <param name="ApplicationPoolName">Name of the application pool.</param>
        /// <param name="IntegratedAppPool">if set to <c>true</c> [integrated application pool].</param>
        /// <param name="DotNetVersion">The dot net version.</param>
        /// <param name="AppPoolIdentityType">Type of the application pool identity.</param>
        /// <param name="AppPool_UserName">Name of the application pool user.</param>
        /// <param name="AppPool_Password">The application pool password.</param>
        /// <param name="CertificateThumbprint">The certificate thumbprint.</param>
        /// <returns>CreateSiteResult.</returns>
        public CreateSiteResult CreateSite(string SiteName, string BaseDirectory, string BindingProtocal, string HostName, string IPAddress, string PortNumber, bool ServerAutoStart,
            string ApplicationPoolName, bool IntegratedAppPool, DotNetVersions DotNetVersion, IdentityType AppPoolIdentityType, string AppPool_UserName = "", string AppPool_Password = "",
            string CertificateThumbprint = "")
        {
            using (ServerManager serverManager = new ServerManager())
            {

                if (serverManager.Sites.Where(x => x.Name == SiteName).Count() == 1)
                {
                    return CreateSiteResult.SiteAlreadyExists;
                }
                if (AppPoolIdentityType == IdentityType.User)
                {
                    if (AppPool_UserName == "" || AppPool_Password == "" || BindingProtocal == "" || HostName == "" || IPAddress == "" || PortNumber == "" || ApplicationPoolName == "")
                    {
                        return CreateSiteResult.RequiredFieldsMissing;
                    }
                }

                Site mySite;
                if (BindingProtocal == "https" && CertificateThumbprint == "")
                {

                    if (CertificateThumbprint == "")
                    {
                        return CreateSiteResult.HttpsRequiresACertificate;
                    }
                    else
                    {
                        X509Certificate _Cert;

                        try
                        {
                            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                            store.Open(OpenFlags.OpenExistingOnly);
                            _Cert = store.Certificates.Find(X509FindType.FindByThumbprint, "‎41 a2 ca 06 94 00 91 29 dd d0 c3 db f5 7f fd 40 2a eb d7 c1", true)[0];
                        }
                        catch (Exception ex)
                        {
                            var _ErrorLogger = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent();
                            _ErrorLogger.LogError("Error Finding Certificate", "ACT.Core.Windows.Web.IIS8_5_Manager Method", ex, ex.Message, Enums.ErrorLevel.Severe);
                            _ErrorLogger = null;
                            return CreateSiteResult.HttpsCertificateNotFound;
                        }
                        mySite = serverManager.Sites.Add(SiteName, IPAddress + ":" + PortNumber + ":" + HostName, BaseDirectory, _Cert.GetCertHash());
                    }
                }
                else
                {
                    mySite = serverManager.Sites.Add(SiteName, BindingProtocal, IPAddress + ":" + PortNumber + ":" + HostName, BaseDirectory);
                }

                mySite.ServerAutoStart = ServerAutoStart;
                serverManager.CommitChanges();
            }
            //  serverManager = null;
            using (var serverManager = new ServerManager())
            {
                serverManager.ApplicationPools.Add(ApplicationPoolName);
                ApplicationPool app_pool = serverManager.ApplicationPools[ApplicationPoolName];

                if (IntegratedAppPool == true)
                {
                    app_pool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
                }
                else
                {
                    app_pool.ManagedPipelineMode = ManagedPipelineMode.Classic;
                }
                if (DotNetVersion == DotNetVersions.Version1_1) { app_pool.ManagedRuntimeVersion = "v1.1"; }
                else if (DotNetVersion == DotNetVersions.Version2_0) { app_pool.ManagedRuntimeVersion = "v2.0"; }
                else if (DotNetVersion == DotNetVersions.Version4_0) { app_pool.ManagedRuntimeVersion = "v4.0"; }

                if (AppPoolIdentityType == IdentityType.User)
                {
                    app_pool.ProcessModel.UserName = AppPool_UserName;
                    app_pool.ProcessModel.IdentityType = ProcessModelIdentityType.SpecificUser;
                    app_pool.ProcessModel.Password = AppPool_Password;
                }
                else if (AppPoolIdentityType == IdentityType.System)
                {
                    app_pool.ProcessModel.IdentityType = ProcessModelIdentityType.LocalSystem;
                }
                else if (AppPoolIdentityType == IdentityType.Service)
                {
                    app_pool.ProcessModel.IdentityType = ProcessModelIdentityType.LocalService;
                }
                else if (AppPoolIdentityType == IdentityType.NetService)
                {
                    app_pool.ProcessModel.IdentityType = ProcessModelIdentityType.NetworkService;
                }
                else
                {
                    app_pool.ProcessModel.IdentityType = ProcessModelIdentityType.ApplicationPoolIdentity;
                }

                serverManager.CommitChanges();
            }

            return CreateSiteResult.SiteCreated;
        }
    }
}
#endif