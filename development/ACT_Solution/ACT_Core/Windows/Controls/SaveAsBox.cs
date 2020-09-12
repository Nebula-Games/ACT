///-------------------------------------------------------------------------------------------------
// file:	Windows\Controls\SaveAsBox.cs
//
// summary:	Implements the save as box class
///-------------------------------------------------------------------------------------------------

#if DOTNETFRAMEWORK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACT.Core.Windows.Controls
{
    public static class SaveAsBox
    {
        public class SaveAsBoxResult
        {
            public string FullPath = "";
            public DialogResult TheDialogResult;

        }
        public static SaveAsBoxResult Show(string BasePath, string Extension, bool overWrite = false, bool FileExists = false, bool PromptUser = false )
        {
            System.Windows.Forms.SaveFileDialog _SD = new System.Windows.Forms.SaveFileDialog();
            
            _SD.CheckFileExists = FileExists;
            _SD.OverwritePrompt = PromptUser;
            _SD.CreatePrompt = PromptUser;
            _SD.DefaultExt = Extension;
            _SD.InitialDirectory = BasePath;

            SaveAsBoxResult _TmpReturn = new SaveAsBoxResult();
            _TmpReturn.FullPath = _SD.FileName;
            _TmpReturn.TheDialogResult = _SD.ShowDialog();
            return _TmpReturn;
        }
    }
}
#endif