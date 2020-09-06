///-------------------------------------------------------------------------------------------------
// file:	Types\Web\DNS\DNSZoneParts.cs
//
// summary:	Implements the DNS zone parts class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;
using System.ComponentModel;

namespace ACT.Core.Types.Web.DNS
{
    public class SOA
    {
        [Description("Primary name server for the domain: In this example, ns.rackspace.com")]
        [DefaultValue("ns")]
        public string Primary_Name_Server { get; set; }
        public int Email { get; set; }
        public int Revision_Number { get; set; }
        public int Refresh_Time { get; set; }

        public int Retry_Time { get; set; }
        public int Expiration_Time { get; set; }

        public int Minimum_TTL { get; set; }

    }
}
