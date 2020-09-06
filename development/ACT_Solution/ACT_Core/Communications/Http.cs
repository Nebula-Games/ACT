// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Http.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACT.Core.Communications
{
    /// <summary>
    /// Class Http.
    /// </summary>
    public class Http
    {
        /// <summary>
        /// Calls the generic handler.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <returns>System.String.</returns>
        public static string CallGenericHandler(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            System.Text.Encoding encoding;
            try
            {
                encoding = System.Text.Encoding.GetEncoding(response.CharacterSet);
            }
            catch
            {
                encoding = System.Text.Encoding.UTF8;
            }

            string _TmpReturn = "";

            using (var responseStream = response.GetResponseStream())
            using (var reader = new System.IO.StreamReader(responseStream, encoding))
            {
                _TmpReturn = reader.ReadToEnd();
            }

            if (_TmpReturn.NullOrEmpty()) { return ""; }
            else { return _TmpReturn; }
        }

        /// <summary>
        /// Call WEB Page and Return String
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>System.String.</returns>
        public static string CallGenericHandler_StringReturn(string URL, Dictionary<string, string> Parameters)
        {
            string _QueryString = "";
            if (Parameters.Count > 0)
            {
                _QueryString += "?";
                foreach (var key in Parameters.Keys)
                {
                    _QueryString += key + "=" + Parameters[key].ToString() + "&";
                }
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL.EnsureEndsWith("/") + _QueryString);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                if (response.ToString().NullOrEmpty()) { return ""; }
                else
                {
                    System.Text.Encoding encoding;

                    if (response.CharacterSet == "") { encoding = System.Text.Encoding.UTF8; }
                    else { encoding = System.Text.Encoding.GetEncoding(response.CharacterSet); }

                    using (var responseStream = response.GetResponseStream())
                    using (var reader = new System.IO.StreamReader(responseStream, encoding))
                        return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Call Generic Handler
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>Guid.</returns>
        public static Guid CallGenericHandler(string URL, Dictionary<string, string> Parameters)
        {
            string _QueryString = "";
            if (Parameters.Count > 0)
            {
                _QueryString += "?";
                foreach (var key in Parameters.Keys)
                {
                    _QueryString += key + "=" + Parameters[key].ToString() + "&";
                }
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL.EnsureEndsWith("/") + _QueryString);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.ToString().NullOrEmpty()) { return Guid.Empty; }
            else
            {
                try
                {
                    Guid? _TmpReturn = response.ToString().ToGuid();
                    if (_TmpReturn == null) { return Guid.Empty; }
                    else
                    {
                        return _TmpReturn.Value;
                    }
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }

        /// <summary>
        /// Parse Query String
        /// </summary>
        /// <param name="FullPath">The full path.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> ParseQueryString(string FullPath)
        {
            FullPath = FullPath.Substring(FullPath.IndexOf("?") + 1);
            return System.Web.HttpUtility.ParseQueryString(FullPath).ToDictionary();
        }

        /// <summary>
        /// Post Data To Website
        /// </summary>
        /// <param name="HeaderData">The header data.</param>
        /// <param name="Data">The data.</param>
        /// <param name="PostURL">The post URL.</param>
        /// <returns>System.String.</returns>
        public static string PostDataToWebsite(Dictionary<string, string> HeaderData, Dictionary<string, string> Data, string PostURL)
        {

            RestSharp.RestClient _NewClient = new RestSharp.RestClient(PostURL);
            RestSharp.RestRequest _NewRequest = new RestSharp.RestRequest(RestSharp.Method.POST);

            // Add All The Parameters
            foreach (string key in Data.Keys) { _NewRequest.AddParameter(key, Data[key]); }

            // Add All The Header Fields
            foreach (string key in HeaderData.Keys) { _NewRequest.AddHeader(key, HeaderData[key]); }

            var _NewResponse = _NewClient.Execute(_NewRequest);

            return _NewResponse.Content;

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Posts a JSON to URL. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/19/2019. </remarks>
        ///
        /// <param name="URL">  The URL. </param>
        /// <param name="Data"> The data. </param>
        ///
        /// <returns>   A Types.Web.ACT_POST_Response. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static Types.Web.ACT_POST_Response PostJSONToURL(string URL, string Data)
        {
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(URL);
            // Set the Method property of the request to POST.  
            request.Method = "POST";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(Data);

            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();

            Types.Web.ACT_POST_Response _tmpReturn = new Types.Web.ACT_POST_Response();

            // Get the response.  
            using (WebResponse response = request.GetResponse())
            {
                // Display the status.  

                _tmpReturn.StatusCode = ((HttpWebResponse)response).StatusCode.ToString();

                // Get the stream containing content returned by the server.  
                // The using block ensures the stream is automatically closed.
                using (dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.  
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.  
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.  
                    _tmpReturn.Content = responseFromServer;
                }

                // Close the response.  
                response.Close();
            }

            return _tmpReturn;
        }

        /// <summary>
        /// Download File From Web URL
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static byte[] DownloadFileFromWebURL(string URL)
        {
            try
            {
                using (var client = new WebClient())
                {
                    byte[] _tmpReturn = client.DownloadData(URL);
                    return _tmpReturn;
                }
            }
            catch { return null; }
        }
    }
}
