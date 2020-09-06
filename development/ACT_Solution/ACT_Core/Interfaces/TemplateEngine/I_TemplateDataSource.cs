///-------------------------------------------------------------------------------------------------
// file:	Interfaces\TemplateEngine\I_TemplateDataSource.cs
//
// summary:	Declares the I_TemplateDataSource interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.TemplateEngine
{
    public interface I_TemplateDataSource
    {
        string RawTemplateIn { get; set; }
        string ProcessedTemplate { get; set; }

        void Process(string Template);

        void Configure(ACT.Core.TemplateEngine.TemplatePackage PackageRef, Dictionary<string, object> DataSourceData);


    }
}
