using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Compression
{
    public interface I_CompressedFileActions
    {
        Common.I_TestResultExpanded UnCompressFile(string SourceFile, string DestinationPath);
        Common.I_TestResultExpanded CompressFile(string SourcePath, string DestinationFile);


    }


    public interface I_CompressedFile
    {
        Dictionary<string, string> Contents { get; set; }
        string FileName { get; set; }
        Common.I_TestResultExpanded AddFile(string FilePath);
        Common.I_TestResultExpanded RemoveFile(string FileToRemove);
        Common.I_TestResultExpanded ChangePaths(string SourcePath, string NewPath);
        Common.I_TestResultExpanded ChangeFilePath(string SourceFile, string NewPath);
        Common.I_TestResultExpanded Encrypt(string EncryptionKey);
        Common.I_TestResultExpanded Decrypt(string EncryptionKey);
        Common.I_TestResultExpanded UnCompressFile(string SourceFile, string DestinationPath);
        Common.I_TestResultExpanded CompressFile(string SourcePath, string DestinationFile);
    }

}
