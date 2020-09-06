using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.CodeGeneration
{ 
    public class ACT_CodeGeneration_UpdateEnums : ACT.Core.Interfaces.CodeGeneration.I_CodeGeneration_UpdateEnums
    {
        public string[] LocateEnums(System.Reflection.Assembly asm)
        {
            List<string> _TmpReturn = new List<string>();
            foreach(var t in asm.GetExportedTypes())
            {
                if (t.CustomAttributes.Any(x=>x.AttributeType == typeof(ACT.Core.CustomAttributes.DatabaseTableBased)))
                {
                    _TmpReturn.Add(t.FullName);
                }
            }

            return _TmpReturn.ToArray();
        }        
    }
}
