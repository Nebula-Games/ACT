using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;
using ACT.Core.Compression;
using ACT.Core.Extensions;


namespace ACT.Plugins.Compression
{

    /*
    public class ACT_Compression : ACT.Core.Interfaces.Common.I_Compression
    {
        public I_TestResultExpanded AddFileToZip(Compressed_File_Settings ZipSettings)
        {
            System.IO.Compression.ZipArchive _ExistingZipFile = null;

            try
            {
                _ExistingZipFile = System.IO.Compression.ZipFile.Open(ZipSettings.ZipFileLocation.EnsureDirectoryFormat() + ZipSettings.ZipFileName, System.IO.Compression.ZipArchiveMode.Update);
            }
            catch(Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.VLogError(this, "Error Opening ZipFile", ex, Core.Enums.ErrorLevel.Informational);
            }

          //  _ExistingZipFile.Entries[0].
            throw new NotImplementedException();
        }

        public Compressed_Data_Structure CompressData(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }

        public Compressed_Data_Structure CompressFile(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }

        public I_TestResultExpanded CompressFolder(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }

        public Compressed_Data_Structure DeCompressData(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }

        public Compressed_Data_Structure DeCompressFile(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }

        public I_TestResultExpanded UnzipFile(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }

        public I_TestResultExpanded UnzipFiles(Compressed_File_Settings ZipSettings)
        {
            throw new NotImplementedException();
        }
    }*/
}
