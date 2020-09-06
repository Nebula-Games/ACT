using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;

namespace ACT.Plugins.Web.IIS
{
    /// <summary>
    /// Implements the IISHIT Interface
    /// </summary>
    public class ACT_IISHit : ACT.Core.Interfaces.Web.IIS.I_IISHit
    {        
        /// <summary>
        /// Date Of the IIS HIt
        /// </summary>
        public DateTime HitDate { get; set; } 	

        /// <summary>
        /// SITE Name
        /// </summary>
        public string SiteName { get; set; } 	

        /// <summary>
        /// Server Name
        /// </summary>
        public string ServerName { get; set; } 	

        /// <summary>
        /// Server IP Address
        /// </summary>
        public string ServerIP { get; set; } 	

        /// <summary>
        /// Method
        /// </summary>
        public string Method { get; set; } 		

        /// <summary>
        /// URI Root
        /// </summary>
        public string UriRoot { get; set; } 	

        /// <summary>
        /// Resource
        /// </summary>
        public string Resource { get; set; }    

        /// <summary>
        /// URI Query
        /// </summary>
        public string UriQuery { get; set; } 	

        /// <summary>
        /// Port Number
        /// </summary>
        public string PortNumber { get; set; } 	
        
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }    

        /// <summary>
        /// Client IP Address
        /// </summary>
        public string ClientIPAddress { get; set; } 
        
        /// <summary>
        ///  CS Version
        /// </summary>
        public string CSVersion { get; set; }   

        /// <summary>
        /// User Agent
        /// </summary>
        public string UserAgent { get; set; }   

        /// <summary>
        /// Cookie
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// Referrer
        /// </summary>
        public string Referrer { get; set; }    

        /// <summary>
        /// Host Header Name
        /// </summary>
        public string HostHeaderName { get; set; } 	

        /// <summary>
        /// ScStatus
        /// </summary>
        public int ScStatus { get; set; } 		

        /// <summary>
        /// ScSubStatus
        /// </summary>
        public int ScSubstatus { get; set; }

        /// <summary>
        /// ScWin32Status
        /// </summary>
        public int ScWin32Status { get; set; }	

        /// <summary>
        /// Bytes Sent
        /// </summary>
        public int BytesSent { get; set; } 		

        /// <summary>
        /// Bytes received
        /// </summary>
        public int BytesReceived { get; set; } 	
        
        /// <summary>
        ///  Time Taken
        /// </summary>
        public int TimeTaken { get; set; } 		

        /// <summary>
        /// Parse Error If Any
        /// </summary>
        public bool ParseError { get; set; }

        /// <summary>
        /// Raw Hit Record
        /// </summary>
        public string RawLine { get; set; }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Static Class
        /// </summary>
        /// <param name="lineData"></param>
        /// <returns></returns>
        public static ACT_IISHit FromString(string lineData)
        {            
            return new ACT_IISHit(lineData);
        }

        /// <summary>
        /// Parse the lineData and Populate the Class
        /// </summary>
        /// <param name="lineData"></param>
        public void ParseLine(string lineData)
        {
            var items = lineData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int item = 0;
            try
            {
                ParseError = false;
                HitDate = DateTime.Parse(items[item++] + " " + items[item++]);
                SiteName = items[item++]; //Site(items[2]); lookup the domain name from a map of w3x to domain name...
                ServerName = items[item++];
                ServerIP = items[item++];
                Method = items[item++];
                UriRoot = items[item++];
                UriQuery = items[item++];
                PortNumber = items[item++];
                UserName = items[item++];
                ClientIPAddress = items[item++];
                CSVersion = items[item++];
                UserAgent = items[item++];
                Cookie = items[item++];
                Referrer = items[item++];
                HostHeaderName = items[item++];
                ScStatus = items[item++].ToInt(0);
                ScSubstatus = items[item++].ToInt(0);
                ScWin32Status = items[item++].ToInt(0);
                BytesSent = items[item++].ToInt(0);
                BytesReceived = items[item++].ToInt(0);
                TimeTaken = items[item++].ToInt(0);
                
            }
            catch (Exception ex)
            {
                ParseError = true;
                RawLine = lineData;
                Console.WriteLine("{0}", item);
                ErrorMessage = string.Format("Error parsing item({0}) -> '{1}' item:{2}", item, ex.Message, items[item]);
            }
        }

        /// <summary>
        /// Parse the Line Data
        /// </summary>
        /// <param name="rawline"></param>
        public ACT_IISHit(string rawline)
        {
            ParseLine(rawline);
        }
    }

}
