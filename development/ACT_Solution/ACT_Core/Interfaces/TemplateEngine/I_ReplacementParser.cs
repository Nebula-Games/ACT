using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.TemplateEngine
{
    public interface I_ReplacementParser
    {
        string ProcessParser(string InboundText, Dictionary<string, string> AdditionalData);

    }
}
