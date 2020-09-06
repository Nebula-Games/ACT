///-------------------------------------------------------------------------------------------------
// file:	Types\Web\DNS\DNSZoneProject.cs
//
// summary:	Implements the DNS zone project class
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
    /*
     [Browsable(bool)] – to show property or not
[ReadOnly(bool)] – possibility to edit property
[Category(string)] – groups of property
[Description(string)] – property description. It is something like a hint.
[DisplayName(string)] – display property
     */
    public class DNSZoneProject
    {
        [DisplayName("Project Name")]
        [Description("Sets the name of the Project")]
        public string ProjectName { get; set; }

        [DisplayName("Compile On Save")]
        [Description("Compiles the Project when Saving. Otherwise saves all files seperatly.")]
        public bool CompileProject { get; set; }
    }
}
