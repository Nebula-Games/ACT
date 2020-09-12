///-------------------------------------------------------------------------------------------------
// file:	Windows\Com\ACT_ComInteropHelper.cs
//
// summary:	Implements the act com interop helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace ACT.Core.Windows.COM
{
    public static class ACT_ComInteropHelper
    {
        /// <summary>
        /// Release The Object
        /// </summary>
        /// <param name="obj">COM Object To Release</param>
        public static void releaseObject(ref object obj) // note ref!
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }
            // Since passed "by ref" this assingment will be useful
            // (It was not useful in the original, and neither was the
            //  GC.Collect.)
            obj = null;
            
        }

        /// <summary>
        /// release the object without the REF Tag
        /// </summary>
        /// <param name="obj">COM Object To Release</param>
        public static void releaseObject(object obj) // note ref!
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }           
        }
    }
}
