///-------------------------------------------------------------------------------------------------
// file:	Interfaces\WebServices\Media\I_MediaServer.cs
//
// summary:	Declares the I_MediaServer interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.WebServices.Media
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for media server. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/22/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface I_MediaServer
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Configure media server. </summary>
        ///
        /// <param name="ApplicationID">        Identifier for the application. </param>
        /// <param name="Versioning">           True to versioning. </param>
        /// <param name="AllPublic">            True to all public. </param>
        /// <param name="SecurityPlugin">       The security plugin. </param>
        /// <param name="ForceSSL">             True to force ssl. </param>
        /// <param name="MIMETypesAllowed">     The mime types allowed. </param>
        /// <param name="StreamEnabled">        True to enable, false to disable the stream. </param>
        /// <param name="StreamAllowApple">     True to stream allow apple. </param>
        /// <param name="StreamSegmentLength">  Length of the stream segment. </param>
        /// <param name="StreamMaxBitRate">     The stream maximum bit rate. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        bool ConfigureMediaServer(Guid ApplicationID, bool Versioning, bool AllPublic, string SecurityPlugin, bool ForceSSL, string[] MIMETypesAllowed,
                bool StreamEnabled, bool StreamAllowApple, int StreamSegmentLength, int StreamMaxBitRate);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Queries if a given media file exists. </summary>
        ///
        /// <param name="ApplicationID">    Identifier for the application. </param>
        /// <param name="FileName">         Filename of the file. </param>
        /// <param name="UserData">         Information describing the user. </param>
        /// <param name="FileSize">         (Optional) Size of the file. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        bool MediaFileExists(Guid ApplicationID, string FileName, Types.ValueTypes.JSONString UserData, int FileSize = 0);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds a media file. </summary>
        ///
        /// <param name="ApplicationID">    Identifier for the application. </param>
        /// <param name="FileName">         Filename of the file. </param>
        /// <param name="FileData">         Information describing the file. </param>
        /// <param name="UserData">         Information describing the user. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        bool AddMediaFile(Guid ApplicationID, string FileName, byte[] FileData, Types.ValueTypes.JSONString UserData);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets media file. </summary>
        ///
        /// <param name="ApplicationID">    Identifier for the application. </param>
        /// <param name="FileName">         Filename of the file. </param>
        /// <param name="UserData">         Information describing the user. </param>
        /// <param name="Version">          (Optional) The version. </param>
        ///
        /// <returns>   An array of byte. </returns>
        ///-------------------------------------------------------------------------------------------------
        byte[] GetMediaFile(Guid ApplicationID, string FileName, Types.ValueTypes.JSONString UserData, int Version = 0);
    }
}
