using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class String_Advanced.
    /// </summary>
    public static class String_Advanced
    {
        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        private static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        /// <summary>
        /// Compress the String and Return the Compressed Data as a Base64 Array (NOT URL ENCODED)
        /// </summary>
        /// <param name="str">String To Compress</param>
        /// <returns>Compressed String</returns>
        public static string Compress(this string str)
        {
            var _tmp = str.Zip();            
            return _tmp.ToBase64String();
        }

        /// <summary>
        /// Compress the String and Return the Compressed Data as a Base64 Array
        /// </summary>
        /// <param name="str">A BASE 64 ENCODED AND COMPRESSED STRING (NO URL ENCODING)</param>
        /// <returns>Uncompressed String</returns>
        public static string DeCompress(this string str)
        {
            byte[] _data = str.FromBase64String();

            return _data.Unzip();            
        }

        /// <summary>
        /// ZIP UP the STRING
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] Zip(this string str)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        /// <summary>
        /// UNZIP The STRING
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string Unzip(this byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return System.Text.Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        /// <summary>
        /// Append Data Fast Using Unsafe Pointers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="dataToAppend"></param>
        /// <returns></returns>
        public static unsafe string FastAppend(this string x, string dataToAppend)
        {

            // fill string by using pointer operations
            var result = new char[x.Length + dataToAppend.Length];

            fixed (char* fixedPointer = result)
            {
                var pointer = fixedPointer;
                for (int z = 0; z < x.Length; z++)
                {
                    *(pointer++) = x[z];
                }

                for (int i = 0; i < dataToAppend.Length; i++)
                {
                    *(pointer++) = dataToAppend[i];
                }
            }
            return result.ToString();
        }
    }
}
