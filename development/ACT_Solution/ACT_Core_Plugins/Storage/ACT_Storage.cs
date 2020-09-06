using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.Storage
{
    public class ACT_Storage : ACT.Core.Interfaces.Storage.I_ACT_Box
    {
        public uint FileCount => throw new NotImplementedException();

        public uint DiskSize => throw new NotImplementedException();

        public uint AddDownloadFile(byte[] fileName, byte[] relativePath, string downloadEndpoint, byte[] userName = null, byte[] passWord = null, byte[] authenticatorCode = null)
        {
            throw new NotImplementedException();
        }

        public uint AddFile(byte[] fileName, byte[] fileData)
        {
            throw new NotImplementedException();
        }

        public byte[] DecryptFile(uint entryID, byte[] userName, byte[] passWord, byte[] authenticatorCode = null)
        {
            throw new NotImplementedException();
        }

        public bool EncryptEntry(uint entryID, byte[] userName, byte[] passWord, byte[] authenticatorCode = null)
        {
            throw new NotImplementedException();
        }

        public uint ExportAll(string exportPath, string downloadFiles)
        {
            throw new NotImplementedException();
        }

        public byte[] ExportToFile()
        {
            throw new NotImplementedException();
        }

        public uint FindEntry(byte[] fileName, byte[] relativePath)
        {
            throw new NotImplementedException();
        }

        public void InitializeStorage()
        {
            throw new NotImplementedException();
        }

        public void ReadFile(byte[] fileData)
        {
            throw new NotImplementedException();
        }

        public uint RemoveEntry(uint entryID)
        {
            throw new NotImplementedException();
        }
    }
}
