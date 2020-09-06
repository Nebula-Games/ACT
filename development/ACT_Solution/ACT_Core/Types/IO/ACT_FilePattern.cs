///-------------------------------------------------------------------------------------------------
// file:	Types\IO\ACT_FilePattern.cs
//
// summary:	Implements the act file pattern class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;

namespace ACT.Core.Types.IO
{
    /// <summary>
    /// ACT File Pattern
    /// </summary>
    public class ACT_FilePattern
    {

        /// <summary>
        /// Path Pattern To Use To Generate Path
        /// </summary>
        public string PathPattern { get; set; }

        /// <summary>
        /// FileName Pattern To Use To Generate Path
        /// </summary>
        public string FileNamePattern { get; set; }

        /// <summary>
        /// Current Path
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Current FileName 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Time Before a New File Is Generated  
        ///     IF NULL NEVER GENERATE UNLESS FILENAME IS BLANK
        /// </summary>
        public TimeSpan? NewFileTimeSpan { get; set; }

        private DateTime LastFileNameGeneratedOn { get; set; }

        private void GenerateFileName(bool Force = false)
        {
            bool _Generate = false;

            if (Force == false) { if ((DateTime.Now - LastFileNameGeneratedOn) > NewFileTimeSpan) { _Generate = true; } }
            else { _Generate = true; }

            if (NewFileTimeSpan == null && FileName.NullOrEmpty() == false) { _Generate = false; }

            if (_Generate)
            {
                LastFileNameGeneratedOn = DateTime.Now;
                BasePath = PathPattern;
                BasePath = BasePath.Replace("###SHORTDATE###", DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString());
                BasePath = BasePath.Replace("###YEAR###", DateTime.Now.Year.ToString());
                BasePath = BasePath.Replace("###MONTH###", DateTime.Now.Month.ToString());
                BasePath = BasePath.Replace("###DAY###", DateTime.Now.Day.ToString());
                BasePath = BasePath.Replace("###UNIXTIME###", DateTime.Now.ToUnixTime().ToString());
                int _lastIndex = 0;
                while (BasePath.Substring(_lastIndex).Contains("###ACT_"))
                {
                    int _Start = BasePath.IndexOf("###ACT_");
                    _Start = _Start + 7;
                    int _End = BasePath.IndexOf("###", _Start);
                    string ACT_FLAG = BasePath.Substring(_Start, _End - _Start);
                    BasePath = BasePath.Replace("###ACT_" + ACT_FLAG + "###", ACT.Core.SystemSettings.GetSettingByName(ACT_FLAG).Value);
                    _lastIndex = _End + 3;
                }
                BasePath = BasePath.Replace("###GUID###", Guid.NewGuid().ToString());
                BasePath = BasePath.Replace("###RANDOM###", ACT.Core.Helper.RandomHelper.Random_Helper.GetRandomFileNameString(10, "aaa").Replace(".aaa", ""));

                FileName = FileNamePattern;
                FileName = FileName.Replace("###SHORTDATE###", DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString());
                FileName = FileName.Replace("###YEAR###", DateTime.Now.Year.ToString());
                FileName = FileName.Replace("###MONTH###", DateTime.Now.Month.ToString());
                FileName = FileName.Replace("###DAY###", DateTime.Now.Day.ToString());
                FileName = FileName.Replace("###UNIXTIME###", DateTime.Now.ToUnixTime().ToString());
                _lastIndex = 0;
                while (FileName.Substring(_lastIndex).Contains("###ACT_"))
                {
                    int _Start = FileName.IndexOf("###ACT_");
                    _Start = _Start + 7;
                    int _End = FileName.IndexOf("###", _Start);
                    string ACT_FLAG = FileName.Substring(_Start, _End - _Start);
                    FileName = FileName.Replace("###ACT_" + ACT_FLAG + "###", ACT.Core.SystemSettings.GetSettingByName(ACT_FLAG).Value);
                    _lastIndex = _End + 3;
                }
                FileName = FileName.Replace("###GUID###", Guid.NewGuid().ToString());
                FileName = FileName.Replace("###RANDOM###", ACT.Core.Helper.RandomHelper.Random_Helper.GetRandomFileNameString(10, "aaa").Replace(".aaa", ""));
            }

        }


        /// <summary>
        /// Force a New File To be Generated
        /// </summary>
        public void ForceNewFile()
        {
            GenerateFileName(true);
        }
    }
}