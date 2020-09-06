using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Security.Hashing;
using System.Reflection;
using System.Diagnostics;

namespace ACT.Plugins
{
    /// <summary>
    /// 
    /// </summary>
    public class ACT_About : ACT.Core.Interfaces.Plugins.I_About
    {
        FileVersionInfo _FileInfoVersion;

        /// <summary>
        /// Constructor
        /// </summary>
        public ACT_About()
        {
            _FileInfoVersion = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
        }

        /// <summary>
        /// Description
        /// </summary>
        public virtual string Description
        {
            get { return _FileInfoVersion.CompanyName; }
        }

        /// <summary>
        /// Author
        /// </summary>
        public virtual string Author
        {
            get { return "Mark Alicz"; }
        }

        /// <summary>
        /// Author ID
        /// </summary>
        public virtual string AuthorID { get { return "53B65C12-DC3A-4C74-A888-B9FDC0A7EEF5"; } }

        /// <summary>
        /// Company Name
        /// </summary>
        public virtual string CompanyName
        {
            get { return _FileInfoVersion.CompanyName; }
        }

        /// <summary>
        /// Company ID
        /// </summary>
        public virtual string CompanyID
        {
            get { return "AFB39E11-6C33-4B87-83D4-E5149B35B42D"; }
        }

        /// <summary>
        /// Current Assembly Hash Value
        /// </summary>
        public virtual byte[] AssemblyHashvalue
        {
            get
            {
                System.Security.Cryptography.SHA256CryptoServiceProvider md5 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
                System.IO.FileStream stream = new System.IO.FileStream(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                var _tmpReturn = md5.ComputeHash(stream);

                stream.Close();

                return _tmpReturn;
            }
        }
    }
}
