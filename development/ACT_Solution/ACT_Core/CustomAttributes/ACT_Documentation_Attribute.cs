using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.CustomAttributes
{
    /// <summary>
    /// This indicates the Target(s) expensive and to what level the cost is relative to Infinite Perfection and Frozen
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Class)]
    public class ACT_Documentation_Attribute : System.Attribute
    {
        public bool _UserFullNameAsIdentifier { get; set; }
        public bool _InternalOnly { get; set; }
        public string _CustomIdentifier { get; set; }

        public ACT_Documentation_Attribute(bool UserFullNameAsIdentifier, bool InternalOnly, string CustomIdentifier = "")
        {
            _UserFullNameAsIdentifier = UserFullNameAsIdentifier;
            _InternalOnly = InternalOnly;
            _CustomIdentifier = CustomIdentifier;
        }
    }
}
