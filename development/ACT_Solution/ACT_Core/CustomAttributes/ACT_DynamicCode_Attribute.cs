using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.CustomAttributes
{
    /// <summary>
    /// This indicates the Target(s) expensive and to what level the cost is relative to Infinite Perfection and Frozen
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Class)]
    public class ACT_Dynamic_Code_Attribute : System.Attribute
    {        
        public ClassPurpose Purpose { get; set; }
        public string FullClassName { get; set; }

        public string CustomIdentifier { get; set; }

        public ACT_Dynamic_Code_Attribute(string FullyQualifiedClassName, ClassPurpose IntendedPurpose, string CustomIdentifiers = "")
        {
            FullClassName = FullyQualifiedClassName;
            Purpose = IntendedPurpose;
            CustomIdentifier = CustomIdentifier;
        }

        public enum ClassPurpose
        {
            DataStorage,
            ConfigurationSettings,
            DataManipulation,
            StructureAndWorkflow
        }
    }
}
