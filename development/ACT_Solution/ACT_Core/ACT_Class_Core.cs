using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core
{
    public class ACT_Class_Core
    {
        public void LogError(object sender, Exception ex, Enums.ErrorLevel errLvl, params string[] Data)
        {
            ACT_Class_Core.ConditionalLogError(sender, ex, errLvl, Data);
        }

        public static void ConditionalLogError(object sender, Exception ex, Enums.ErrorLevel errLvl, params string[] Data)
        {
            string _tmpResult = "";
#if NETCORE
            foreach (string data in Data) { _tmpResult = _tmpResult + data; }
#else
            foreach (string data in Data) { _tmpResult = _tmpResult.FastAppend(data); }
#endif


            if (sender is string) { Helper.ErrorLogger.VLogError(sender, _tmpResult, ex, errLvl); }
            else { Helper.ErrorLogger.VLogError(sender.GetType().FullName, _tmpResult, ex, errLvl); }
        }
    }
}
