using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;

namespace ACT.Plugins.Common
{
    [Serializable()]
    public class ACT_TestResult : I_TestResult
    {
        private static I_TestResult _Default_True = null;
        private static I_TestResult _Default_False = null;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the default true. </summary>
        ///
        /// <value> The default true. </value>
        ///-------------------------------------------------------------------------------------------------
        public static I_TestResult Default_True
        {
            get
            {
                if (_Default_True != null) { return _Default_True; }
                else
                {
                    var _tmpReturn = ACT.Core.CurrentCore<I_TestResult>.GetCurrent();
                    _tmpReturn.Success = true;
                    _Default_True = _tmpReturn;
                    return _Default_True;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the default false. </summary>
        ///
        /// <value> The default false. </value>
        ///-------------------------------------------------------------------------------------------------
        public static I_TestResult Default_False
        {
            get
            {
                if (_Default_False != null) { return _Default_False; }
                else
                {
                    var _tmpReturn = ACT.Core.CurrentCore<I_TestResult>.GetCurrent();
                    _tmpReturn.Success = false;
                    _Default_False = _tmpReturn;
                    return _Default_False;
                }
            }
        }

        /// <summary>
        /// Exceptions
        /// </summary>
        [NonSerialized()]
        List<Exception> _Exceptions = new List<Exception>();

        #region Private Fields
        private List<string> _Messages = new List<string>();
        private  List<string> _Warnings = new List<string>();
        private bool _Success;
        private bool _HasWarnings;
        #endregion

        #region I_TestResult Members

        public List<string> Warnings { get { return _Warnings; } set { _Warnings = value; } }

        public bool HasWarnings { get { return _HasWarnings; } set { _HasWarnings = value; } }

        public List<Exception> Exceptions
        {
            get { return _Exceptions; }
            set { _Exceptions = value; }
        }

        public List<string> Messages
        {
            get { return _Messages; }
            set { _Messages = value; }
        }

        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }


        #endregion
    }
}
