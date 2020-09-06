using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ACT.Core.Interfaces.Security.Encryption;
using Newtonsoft.Json;


namespace ACT.Core.Extensions
{
    /// <summary>   A system types. </summary>
    ///
    /// <remarks>   Mark Alicz, 12/8/2016. </remarks>

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Class SystemTypes. </summary>
    ///
    /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class String_Manipulations
    {


        #region Conditional - Searches - State
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches the string X for any instance of the List of Strings. </summary>
        /// <summary>   Determines whether this instance contains the object. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">                . </param>
        /// <param name="SearchStrings">    . </param>
        /// <param name="IgnoreCase">       . </param>
        ///
        /// <returns>   True if the object is in this collection, false if not. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool Contains(this string x, List<string> SearchStrings, bool IgnoreCase)
        {
            string _tmp = x;
            if (IgnoreCase) { _tmp = _tmp.ToLower(); }
            foreach (string s in SearchStrings)
            {
                string _tmpS = s;
                if (IgnoreCase) { _tmpS = s.ToLower(); }
                if (_tmp.Contains(_tmpS)) { return true; }
            }
            return false;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches the string X for any instance of the String. </summary>
        /// <summary>   Determines whether this instance contains the object. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">            . </param>
        /// <param name="SearchString"> . </param>
        /// <param name="IgnoreCase">   (Optional) </param>
        ///-------------------------------------------------------------------------------------------------
        public static bool Contains(this string x, string SearchString, bool IgnoreCase = true)
        {
            string _tmpS = x;
            if (IgnoreCase) { _tmpS = _tmpS.ToLower(); }
            string tmpA = SearchString;

            if (IgnoreCase) { tmpA = SearchString.ToLower(); }
            if (_tmpS.Contains(tmpA)) { return true; }

            return false;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches the string X for any instance of the List of Strings. </summary>
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">            . </param>
        /// <param name="SearchString"> . </param>
        /// <param name="IgnoreCase">   (Optional) </param>
        ///
        /// <returns>   True if the object is in this collection, false if not. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool Contains(this List<string> x, string SearchString, bool IgnoreCase = true)
        {
            foreach (string _tmpS in x)
            {
                string s = _tmpS;
                if (IgnoreCase) { s = _tmpS.ToLower(); }
                string tmpA = SearchString;

                if (IgnoreCase) { tmpA = SearchString.ToLower(); }
                if (s.Contains(tmpA)) { return true; }
            }

            return false;
        }






        #endregion





        ///-------------------------------------------------------------------------------------------------
        /// <summary>   RPLs the specified find. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">        The x. </param>
        /// <param name="Find">     The find. </param>
        /// <param name="Replace">  The replace. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string RPL(this string x, string Find, string Replace)
        {
            return x.Replace(Find, Replace);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Tilde , Number sign, Percent, Ampersand,Asterisk, Braces, Backslash,Colon,Angle brackets,
        /// Question mark, Slash,Plus sign,Pipe,Quotation mark, Doesnt Start With a _ Cant Containt
        /// multiple .  i.e (mark...alicz.txt)
        /// cant end with a period (mark.txt.)
        /// Escapes the name of the file.
        /// </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    The x. </param>
        /// <param name="R">    (Optional) The r. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string EscapeFileName(this string x, string R = "_")
        {
            x = x.Trim("_");
            x = x.Trim(".");
            x = x.Trim("..");
            return x.RPL("~", R).RPL("#", R).RPL("%", R).RPL("&", R).RPL("*", R).RPL("{", R).RPL("}", R).RPL("\\", R).RPL(":", R).RPL("<", R).RPL(">", R).RPL("?", R).RPL("/", R).RPL("+", R).RPL("|", R).RPL("\"", R);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   XML Escape Attributes. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    . </param>
        /// <param name="R">    (Optional) </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string XML_EscapeAttribute(this string x, string R = "_")
        {
            x = x.Replace("&", R);
            x = x.Replace("<", R);
            x = x.Replace(">", R);
            x = x.Replace("'", R);
            x = x.Replace("\"", R);
            return x;
        }
        

        /// <summary>   Escapes a string according to the URI data string rules given in RFC 3986. </summary>
        ///
        /// <remarks>
        /// The <see cref="Uri.EscapeDataString"/> method is <i>supposed</i> to take on RFC 3986 behavior
        /// if certain elements are present in a .config file.  Even if this actually worked (which in my
        /// experiments it <i>doesn't</i>), we can't rely on every host actually having this
        /// configuration element present.
        /// </remarks>
        ///
        /// <param name="value">    The value to escape. </param>
        ///
        /// <returns>   The escaped value. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Escapes the URI data string RFC3986. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------

        internal static string EscapeUriDataStringRfc3986(this string value)
        {
            // Start with RFC 2396 escaping by calling the .NET method to do the work.
            // This MAY sometimes exhibit RFC 3986 behavior (according to the documentation).
            // If it does, the escaping we do that follows it will be a no-op since the
            // characters we search for to replace can't possibly exist in the string.
            StringBuilder escaped = new StringBuilder(Uri.EscapeDataString(value));

            // Upgrade the escaping to RFC 3986, if necessary.
            for (int i = 0; i < UriRfc3986CharsToEscape.Length; i++)
            {
                escaped.Replace(UriRfc3986CharsToEscape[i], Uri.HexEscape(UriRfc3986CharsToEscape[i][0]));
            }

            // Return the fully-RFC3986-escaped string.
            return escaped.ToString();
        }

        /// <summary>   To String Overload Convert Null To Empty String. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">                        string To Convert. </param>
        /// <param name="ConvertNullToEmptyString"> True/False Convert Null To Empty String. </param>
        ///
        /// <returns>   A string that represents this object. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns a <see cref="System.String" /> that represents this instance. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">                        The x. </param>
        /// <param name="ConvertNullToEmptyString"> if set to <c>true</c> [convert null to empty string]. </param>
        ///
        /// <returns>   A <see cref="System.String" /> that represents this instance. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ToString(this string x, bool ConvertNullToEmptyString)
        {
            if (ConvertNullToEmptyString)
            {
                if (x == null) { return ""; }
            }
            return x;
        }

        /// <summary>   Converts The String To UTF 8 Format. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    String To Convert. </param>
        ///
        /// <returns>   UTF 8 String. </returns>
        public static string ToUTF8(this string x)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(x);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>   URL Encode The String. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">                String To URL Encode. </param>
        /// <param name="UseDataMethod">    (Optional) Use Data Method (EscapeDataString) VS
        ///                                 (EscapeUriString) </param>
        ///
        /// <returns>   URL Encoded String. </returns>
        public static string URLEncode(this string x, bool UseDataMethod = true)
        {
            if (UseDataMethod)
            {
                return System.Uri.EscapeDataString(x);
            }
            else
            {
                return System.Uri.EscapeUriString(x);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Decodes a URL Encoded String. </summary>
        /// <summary>   URLs the decode. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------
        public static string URLDecode(this string x)
        {
            return System.Web.HttpUtility.UrlDecode(x);
        }

        #region String Manipulation

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that ensures that starts with. </summary>
        /// <summary>   Ensures the starts with. </summary>
        /// <remarks>   Mark Alicz, 9/8/2019. </remarks>
        ///
        /// <param name="x">        string To Convert. </param>
        /// <param name="StartsWith">    The end. </param>
        ///
        /// <returns>   A string. </returns>        
        ///-------------------------------------------------------------------------------------------------
        public static string EnsureStartsWith(this string x, string StartsWith)
        {
            if (x.StartsWith(StartsWith) == false) { return StartsWith + x; }
            else { return x; }
        }
        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Ensures the ends with. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    The x. </param>
        /// <param name="EndsWith">  The end. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string EnsureEndsWith(this string x, string EndsWith)
        {
            if (x.EndsWith(EndsWith) == false)
            {
                return x + EndsWith;
            }
            else
            {
                return x;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Checks the string to make sure the string ends with one of the specified options. </summary>
        /// <summary>   Endses the with. </summary>
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">                . </param>
        /// <param name="Options">          . </param>
        /// <param name="CaseSensitive">    (Optional) if set to <c>true</c> [case sensitive]. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool EndsWith(this string x, List<string> Options, bool CaseSensitive = false)
        {
            string _tmp = x;
            if (CaseSensitive == false) { x = x.ToLower(); }

            foreach (var _option in Options)
            {
                if (CaseSensitive == false)
                {
                    if (x.EndsWith(_option.ToLower())) { return true; }
                }
                else
                {
                    if (x.EndsWith(_option)) { return true; }
                }
            }

            return false;
        }

        #endregion

        /// <summary>
        /// Checks to see if the string is a valid base64 string
        /// </summary>
        /// <param name="x"></param>
        /// <returns>true/false</returns>
        public static bool IsValidBase64String(this string x)
        {
            try
            {                
                byte[] data = Convert.FromBase64String(x);
                return (x.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {             
                return false;
            }
        }

        /// <summary>   Tests The Value Returns True/False Depending on Match. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">        . </param>
        /// <param name="Default">  (Optional) </param>
        /// <param name="LiberalTest">  Tests the String For Anything That Is Not a 0</param>
        ///
        /// <returns>   The given data converted to a bool. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool ToBool(this string x, bool Default = false, bool LiberalTest = false)
        {
            if (x == null) { return Default; }
            x = x.Trim();
            try
            {
                if (x.ToLower() == "yes" || x.ToLower() == "true" || x.ToLower() == "1" || (LiberalTest && x.ToInt(-1) != 0))
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>   Tests The Value Returns True/False Depending on Match. </summary>
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">                . </param>
        /// <param name="TestCaseYes">      . </param>
        /// <param name="CaseSensitive">    (Optional) </param>
        ///
        /// <returns>   true if the test passes, false if the test fails. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool TestToBool(this string x, string TestCaseYes, bool CaseSensitive = false)
        {
            if (CaseSensitive)
            {
                if (x.ToString() == TestCaseYes)
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if (x.ToString().ToLower() == TestCaseYes.ToLower())
                {
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>   Converts A String To A DateTime.  If Error Returns Null. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   DateTime or Null. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static DateTime? TryToDateTime(this string x)
        {
            try
            {
                return Convert.ToDateTime(x);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Normalize A String Value For SQL Server.  Used Specifically For Non Parameterized SQL
        /// Queries.  i.e. Values of Insert Statements Also Attempts To Remove Any SQL Injections
        /// Attempts.
        /// </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    String To Normalize. </param>
        ///
        /// <returns>   Normalized String. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static string NormalizeForSQLServer(this string x)
        {
            return x.Replace("'", "''").Replace("' OR", "").Replace("' AND", "");
        }

        /// <summary>   Converts A String To A Int.  If Error Returns Null. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   Int or Null. </returns>
        public static int? TryToInt(this string x)
        {
            try
            {
                return Convert.ToInt32(x);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>   Converts a string to a Guid. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   Guid or Null. </returns>
        public static Guid? TryToGuid(this string x)
        {
            try
            {
                return new Guid(x);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>   Converts A String To A Decimal.  If Error Returns Null. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   Decimal or Null. </returns>    
        public static decimal? TryToDecimal(this string x)
        {
            try
            {
                return Convert.ToDecimal(x);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>   Converts A String To A Decimal.  If Error Returns Null. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   Decimal or Null. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Tries to double. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    The x. </param>
        ///
        /// <returns>   System.Nullable&lt;System.Double&gt;. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static double? TryToDouble(this string x)
        {
            try
            {
                return Convert.ToDouble(x);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>   A string extension method that format to money. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   The formatted to money. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Formats to money. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    The x. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Format_To_Money(this string x)
        {
            try
            {
                double _TmpDouble = Convert.ToDouble(x);
                string _Tmp = String.Format("{0:C}", _TmpDouble);
                _Tmp = _Tmp.Substring(0, _Tmp.IndexOf("."));

                return _Tmp;
            }
            catch
            {
                return "$0";
            }
        }

        /// <summary>   A byte[] extension method that gets a string. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="buffer">   The buffer to act on. </param>
        ///
        /// <returns>   The string. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the string. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="buffer">   The buffer. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string GetString(this byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
            {
                return "";
            }

            // Ansi as default
            System.Text.Encoding encoding = System.Text.Encoding.Default;

            /*
                EF BB BF	UTF-8 
                FF FE UTF-16	little endian 
                FE FF UTF-16	big endian 
                FF FE 00 00	UTF-32, little endian 
                00 00 FE FF	UTF-32, big-endian 
             */

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            {
                encoding = System.Text.Encoding.UTF8;
            }
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                encoding = System.Text.Encoding.Unicode;
            }
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                encoding = System.Text.Encoding.BigEndianUnicode; // utf-16be
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                encoding = System.Text.Encoding.UTF32;
            }
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            {
                encoding = System.Text.Encoding.UTF7;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>   A string extension method that returns get web request. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="URL">              The URL to act on. </param>
        /// <param name="OptionalProxy">    (Optional) the optional proxy. </param>
        ///
        /// <returns>   The get web request. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns the get web request. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="URL">              The URL. </param>
        /// <param name="OptionalProxy">    (Optional) The optional proxy. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Return_GetWebRequest(this string URL, WebProxy OptionalProxy = null)
        {
            WebRequest wrGETURL = WebRequest.Create(URL);


            if (OptionalProxy != null)
            {
                wrGETURL.Proxy = OptionalProxy;
            }

            using (Stream objStream = wrGETURL.GetResponse().GetResponseStream())
            {

                using (StreamReader objReader = new StreamReader(objStream))
                {

                    string sLine = objReader.ReadToEnd();
                    wrGETURL = null;
                    return sLine;
                }
            }



        }

        /// <summary>
        /// A string extension method that creates nice spaced text from single text capitals.
        /// </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="val">  The val to act on. </param>
        ///
        /// <returns>   The new nice spaced text from single text capitals. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates the nice spaced text from single text capitals. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="val">  The value. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string CreateNiceSpacedTextFromSingleTextCapitals(this string val)
        {
            val = string.Concat(val.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
            return val;
        }

        /// <summary>   A string extension method that string between. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">        string To Convert. </param>
        /// <param name="Start">    The start. </param>
        /// <param name="End">      The end. </param>
        ///
        /// <returns>   A string. </returns>

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Strings the between. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">        The x. </param>
        /// <param name="Start">    The start. </param>
        /// <param name="End">      The end. </param>
        ///
        /// <returns>   System.String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string StringBetween(this string x, string Start, string End)
        {
            int _First = x.IndexOf(Start) + Start.Length;
            if (_First == -1) { return ""; }
            int _Second = x.IndexOf(End, _First);
            if (_Second == -1) { return ""; }

            return x.Substring(_First, _Second - _First);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts a byte array to a string. </summary>
        /// <summary>   From the base64 string. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="base64String"> The base64 string. </param>
        ///
        /// <returns>   The string. </returns>
        /// <returns>   System.Byte[]. </returns>
        ///
        /// ### <param name="bytes">    the byte array. </param>
        ///-------------------------------------------------------------------------------------------------

        public static byte[] FromBase64String(this string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Replace Extension (Child) Accepts Comma Seperated Strings as Parameters for the
        ///             Lazy People. </summary>
        /// <summary>   Replaces the specified replace comma seperated. </summary>
        ///
        /// <remarks>   Mark Alicz, 11/24/2016. </remarks>
        ///
        /// <param name="str">                          The str to act on. </param>
        /// <param name="replaceCommaSeperated">        The replace comma seperated. </param>
        /// <param name="replacewithCommaSeperated">    The replacewith comma seperated. </param>
        /// <param name="comparison">                   The comparison method. </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="str">  The string. </param>
        ///
        /// ### <param name="replaceCommaSeperated">        The replace comma seperated. </param>
        /// ### <param name="replacewithCommaSeperated">    The replacewith comma seperated. </param>
        /// ### <param name="comparison">                   The comparison. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string Replace(this string str, string replaceCommaSeperated, string replacewithCommaSeperated, StringComparison comparison)
        {
            return Replace(str, replaceCommaSeperated.SplitString(",", StringSplitOptions.RemoveEmptyEntries), replacewithCommaSeperated.SplitString(",", StringSplitOptions.None), comparison);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Read Till. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">                . </param>
        /// <param name="FindCharacters">   . </param>
        ///
        /// <returns>   String Of Data. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ReadTill(this string x, string FindCharacters)
        {
            int _foundIndex = -1;

            _foundIndex = x.IndexOf(FindCharacters);
            if (_foundIndex != -1) { return ""; }
            return x.Substring(0, _foundIndex);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Replace with Inheriant Case Insensitivity (English Works :) </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="str">          Base String. </param>
        /// <param name="Find">         String To Find. </param>
        /// <param name="ReplaceWith">  Replace With String. </param>
        ///
        /// <returns>   Replacement String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ReplaceCaseInsensitive(this string str, string Find, string ReplaceWith)
        {
            bool _tmpInReplacement = false;
            int _PositionTmp = 0;
            int _StartPos = 0;
            int _DeffOffset = ReplaceWith.Length - Find.Length;
            List<int> FoundPlaces = new List<int>();

            for (int x = 0; x < str.Length; x++)
            {
                if (_tmpInReplacement)
                {
                    if (Char.ToLower(str[x]) != Char.ToLower(Find[_PositionTmp]))
                    {
                        _PositionTmp = 0; _tmpInReplacement = false;
                    }
                    else
                    {
                        _PositionTmp++;
                        if (Find.Length == _PositionTmp)
                        {
                            _StartPos = _StartPos + (_DeffOffset * (FoundPlaces.Count()));

                            FoundPlaces.Add(_StartPos); _PositionTmp = 0; _tmpInReplacement = false; _StartPos = 0;
                        }
                    }
                }
                else
                {
                    if (Char.ToLower(str[x]) == Char.ToLower(Find[0]))
                    {
                        _StartPos = x;
                        _PositionTmp++;
                        _tmpInReplacement = true;
                    }
                }
            }

            foreach (int start in FoundPlaces)
            {
                str = str.Substring(0, start) + ReplaceWith + str.Substring(start + Find.Length);
            }

            return str;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that replaces strings. </summary>
        /// <summary>   Replaces the specified replace. </summary>
        ///
        /// <remarks>   Mark Alicz, 11/24/2016. </remarks>
        ///
        /// <param name="str">          The str to act on. </param>
        /// <param name="replace">      The Old Value strings string[]. </param>
        /// <param name="replacewith">  The New Value strings string[]. </param>
        /// <param name="comparison">   The comparison method. </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="str">  The string. </param>
        ///
        /// ### <param name="replace">      The replace. </param>
        /// ### <param name="replacewith">  The replacewith. </param>
        /// ### <param name="comparison">   The comparison. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string Replace(this string str, string[] replace, string[] replacewith, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder();
            string _Changed = str;

            for (int xr = 0; xr < replace.Length; xr++)
            {
                string oldValue = replace[xr];
                string newValue = replacewith[xr];

                int previousIndex = 0;
                int index = _Changed.IndexOf(oldValue, comparison);
                while (index != -1)
                {
                    sb.Append(_Changed.Substring(previousIndex, index - previousIndex));
                    sb.Append(newValue);
                    index += oldValue.Length;

                    previousIndex = index;
                    index = _Changed.IndexOf(oldValue, index, comparison);
                }
                sb.Append(_Changed.Substring(previousIndex));
                _Changed = sb.ToString();
                sb.Clear();
            }

            return _Changed;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that removes the script tags described by x. </summary>
        /// <summary>   Removes the script tags. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string RemoveScriptTags(this string x)
        {
            string _tmp = x.Replace("<script>", "");
            _tmp = _tmp.Replace("</script>", "");
            _tmp = _tmp.Replace("</Script>", "");
            _tmp = _tmp.Replace("</SCRIPT>", "");
            _tmp = _tmp.Replace("</SCript>", "");
            _tmp = _tmp.Replace("<Script>", "");
            _tmp = _tmp.Replace("<SCRIPT>", "");
            _tmp = _tmp.Replace("<SCript>", "");

            return _tmp;
        }

        #region JSON Extensions

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a JSON. </summary>
        /// <summary>   Converts to json. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a dynamic. </returns>
        /// <returns>   dynamic. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///
        /// ### <exception cref="Exception">    Error Loading JSON. </exception>
        ///-------------------------------------------------------------------------------------------------

        public static dynamic ToJSON(this string x)
        {
            try
            {
                dynamic t = JsonConvert.DeserializeObject<dynamic>(x);
                return t;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Loading JSON", ex);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that JSON to data table. </summary>
        /// <summary>   Jsons to data table. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   A DataTable. </returns>
        /// <returns>   DataTable. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static DataTable JSONToDataTable(this string x)
        {
            dynamic t = JsonConvert.DeserializeObject<dynamic>(x);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(t[0]);

            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, typeof(String));
            }
            object[] values = new object[props.Count];
            foreach (var item in t)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                    values[i] = values[i].ToString();
                }
                table.Rows.Add(values);
            }
            return table;

        }

        #endregion JSON Extensions

        #region Convert Methods (ToXXX, FromXXX)

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts to type. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <typeparam name="T">    . </typeparam>
        /// <param name="x">            The x. </param>
        /// <param name="defaultValue"> The default value. </param>
        ///
        /// <returns>   T. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static T ToType<T>(this string x, string defaultValue)
        {
            if (typeof(T) == typeof(uint) || typeof(T) == typeof(uint))
            {
                return (T)Convert.ChangeType(x.ToInt(defaultValue.ToInt(0)), typeof(T));
            }
            else if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
            {
                return (T)Convert.ChangeType(x.ToLong(defaultValue.ToLong(0)), typeof(T));
            }
            else if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(x.ToBool(defaultValue.ToBool(false)), typeof(T));
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (T)Convert.ChangeType(x.ToDateTime(defaultValue.ToDateTime(DateTime.MinValue)), typeof(T));
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(x, typeof(string));
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (T)Convert.ChangeType(x.ToDecimal(defaultValue.ToDecimal(0)), typeof(decimal));
            }
            else if (typeof(T) == typeof(decimal))
            {
                return (T)Convert.ChangeType(x.ToFloat(defaultValue.ToFloat(0)), typeof(float));
            }
            else
            {
                return default(T);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a base 64. </summary>
        /// <summary>   Converts to base64. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToBase64(this string x)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(x);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a base 16. </summary>
        /// <summary>   Converts to base16. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToBase16(this string x)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(x);

            string returnValue = ACT.Core.Encoding.Base16Encoder.encode(toEncodeAsBytes);

            return returnValue;
        }

        #region ACTString Methods

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a ACT 16. </summary>
        /// <summary>   Converts to baseactstring. </summary>
        ///
        /// <remarks>   Mark Alicz, 5/8/2018. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToBaseACTString(this string x)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(x);

            return ToBaseACTString(x);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a ACT 16. </summary>
        /// <summary>   Converts to baseactstring. </summary>
        ///
        /// <remarks>   Mark Alicz, 5/8/2018. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToBaseACTString(this byte[] x)
        {
            string returnValue = ACT.Core.Encoding.ACTEncoder.encode(x);

            return returnValue;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts the string to a byte[] from a ACT Encoded String. </summary>
        /// <summary>   Froms the base act string. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   A byte[]. </returns>
        /// <returns>   System.Byte[]. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static byte[] FromBaseACTString(this string x)
        {
            return ACT.Core.Encoding.ACTEncoder.decode(x);
        }




        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that initializes this object from the given from base
        ///             64. </summary>
        /// <summary>   Froms the base64. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string FromBase64(this string x)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(x);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that initializes this object from the given from base
        ///             16. </summary>
        /// <summary>   Froms the base16. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string FromBase16(this string x)
        {
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(ACT.Core.Encoding.Base16Encoder.decode(x));

            return returnValue;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a sha 256. </summary>
        /// <summary>   Converts to sha256. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------
        public static string ToSHA256(this string x) { return ACT.Core.CurrentCore<I_Encryption>.GetCurrent().SHA256(x); }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a sha 512. </summary>
        /// <summary>   Converts to sha512. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------
        public static string ToSHA512(this string x) { return ACT.Core.CurrentCore<I_Encryption>.GetCurrent().SHA512(x); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a md 5. </summary>
        /// <summary>   Converts to md5. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToMD5(this string x) { return ACT.Core.CurrentCore<I_Encryption>.GetCurrent().MD5(x); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a md 5 a lt. </summary>
        /// <summary>   Converts to md5alt. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToMD5ALT(this string x) { return ACT.Core.CurrentCore<I_Encryption>.GetCurrent().MD5ALT(x); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to an invariant string. </summary>
        /// <summary>   Converts to invariantstring. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as a string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string ToInvariantString(this string x)
        {
            return x.ToString(CultureInfo.InvariantCulture);

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to an int fast. </summary>
        /// <summary>   Converts to intfast. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   x as an int. </returns>
        /// <returns>   System.Int32. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static int ToIntFast(this string x)
        {
            int y = 0;
            for (int i = 0; i < x.Length; i++)
            {
                y = y * 10 + (x[i] - '0');
            }

            return y;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Is Integer. </summary>
        /// <summary>   Determines whether the specified x is integer. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   True if integer, false if not. </returns>
        /// <returns>   <c>true</c> if the specified x is integer; otherwise, <c>false</c>. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static bool IsInteger(this string x)
        {
            for (int p = 0; p < x.Length; p++)
            {
                if (p == 0) { if (x[0] == '-') { continue; } }

                if (!ACT.Core.ACTConstants.AllNumericArray.Contains(x[p])) { return false; }
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to an int. </summary>
        /// <summary>   Converts the string representation of a number to an integer. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   The given data converted to an int. </returns>
        /// <returns>   System.Int32. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static int ToInt(this string x) { return Convert.ToInt32(x); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to a long. </summary>
        /// <summary>   Converts to long. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   The given data converted to a long. </returns>
        /// <returns>   System.Int64. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static long ToLong(this string x) { return Convert.ToInt64(x); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to an int. </summary>
        /// <summary>   Converts the string representation of a number to an integer. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        ///
        /// <returns>   The given data converted to an int. </returns>
        /// <returns>   System.Int32. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///
        /// ### <param name="ErrorValue">   The error value. </param>
        ///-------------------------------------------------------------------------------------------------

        public static int ToInt(this string x, int ErrorValue) { if (x.IsInteger()) { return ToIntFast(x); } else { return ErrorValue; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to a long. </summary>
        /// <summary>   Converts to long. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        ///
        /// <returns>   The given data converted to a long. </returns>
        /// <returns>   System.Int64. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///
        /// ### <param name="ErrorValue">   The error value. </param>
        ///-------------------------------------------------------------------------------------------------

        public static long ToLong(this string x, long ErrorValue) { if (x.IsInteger()) { return Convert.ToInt64(x); } else { return ErrorValue; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to a decimal. </summary>
        /// <summary>   Converts to decimal. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        ///
        /// <returns>   The given data converted to a decimal. </returns>
        /// <returns>   System.Decimal. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///
        /// ### <param name="ErrorValue">   The error value. </param>
        ///-------------------------------------------------------------------------------------------------

        public static decimal ToDecimal(this string x, decimal ErrorValue) { try { return Convert.ToDecimal(x); } catch { return ErrorValue; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to a date time. </summary>
        /// <summary>   Converts to datetime. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   The given data converted to a DateTime. </returns>
        /// <returns>   DateTime. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static DateTime ToDateTime(this string x) { return Convert.ToDateTime(x); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts this object to a date time. </summary>
        /// <summary>   Converts to datetime. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="ErrorValue">   The error value Date/Time. </param>
        ///
        /// <returns>   The given data converted to a DateTime. </returns>
        /// <returns>   DateTime. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///
        /// ### <param name="ErrorValue">   The error value. </param>
        ///-------------------------------------------------------------------------------------------------

        public static DateTime ToDateTime(this string x, DateTime ErrorValue) { try { return Convert.ToDateTime(x); } catch { return ErrorValue; } }
        #endregion

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that escape specifier characters for js. </summary>
        /// <summary>   Escapes the spec chars for js. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    string To Convert. </param>
        ///
        /// <returns>   A string. </returns>
        /// <returns>   System.String. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static string EscapeSpecCharsForJS(this string x)
        {
            string returnValue = x;

            returnValue = returnValue.Replace("\\", "\\\\");
            returnValue = returnValue.Replace("'", "\\'");
            returnValue = returnValue.Replace("\r\n", "<br />");
            return returnValue;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that null or empty. </summary>
        /// <summary>   Nulls the or empty. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        /// <returns>   <c>true</c> if XXXX, <c>false</c> otherwise. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///-------------------------------------------------------------------------------------------------

        public static bool NullOrEmpty(this string x)
        {
            return String.IsNullOrEmpty(x);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that ensures that int. </summary>
        /// <summary>   Ensures the int. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">        . </param>
        /// <param name="SetNull">  true to set null. </param>
        ///
        /// <returns>   An int? </returns>
        /// <returns>   System.Nullable&lt;System.Int32&gt;. </returns>
        ///
        /// ### <param name="x">    The x. </param>
        ///
        /// ### <param name="SetNull">  if set to <c>true</c> [set null]. </param>
        ///-------------------------------------------------------------------------------------------------

        public static int? EnsureInt(this string x, bool SetNull)
        {
            int? _TmpReturn;

            try
            {
                _TmpReturn = Convert.ToInt32(x);
            }
            catch
            {
                if (SetNull) { return null; }
                else
                {
                    _TmpReturn = 0;
                }
            }

            return _TmpReturn;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that splits a string. </summary>
        /// <summary>   Splits the string. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="Delimeter">    The delimeter. </param>
        /// <param name="o">            The StringSplitOptions to process. </param>
        ///
        /// <returns>   A string[]. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string[] SplitString(this string x, string Delimeter, StringSplitOptions o)
        {
            return Regex.Split(x, Regex.Escape(Delimeter));
            //return x.Split(Delimeter.ToCharArray(), o);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that splits a string. </summary>
        /// <summary>   Splits the string. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">                string To Convert. </param>
        /// <param name="Delimeter">        (Optional) The delimeter. </param>
        /// <param name="QuotedIdentifier"> (Optional) Identifier for the quoted. </param>
        /// <param name="o">                (Optional) The StringSplitOptions to process. </param>
        ///
        /// <returns>   A string[]. </returns>
        /// <returns>   System.String[]. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string[] SplitString(this string x, string Delimeter = " ", string QuotedIdentifier = "\"", StringSplitOptions o = StringSplitOptions.RemoveEmptyEntries)
        {
            return x.Split(Delimeter.ToCharArray()).Select((element, index) => index % 2 == 0 ? element.Split(QuotedIdentifier.ToCharArray(), o) : new string[] { element }).SelectMany(element => element).ToArray();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that trim start. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="Characters">   The characters. </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string TrimStart(this string x, string Characters)
        {
            return x.TrimStart(Characters.ToCharArray());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that trim end. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="Characters">   The characters. </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string TrimEnd(this string x, string Characters)
        {
            return x.TrimEnd(Characters.ToCharArray());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that trims. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">            string To Convert. </param>
        /// <param name="Characters">   The characters. </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Trim(this string x, string Characters)
        {
            return x.Trim(Characters.ToCharArray());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that lefts. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">        string To Convert. </param>
        /// <param name="Length">   The length. </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Left(this string x, int Length)
        {
            return x.Substring(0, Length);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that rights. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">        string To Convert. </param>
        /// <param name="Length">   The length. </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string Right(this string x, int Length)
        {
            return x.Substring(x.Length - Length);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts a DBType to a database type. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="DBType">   The DBType to act on. </param>
        ///
        /// <returns>   DBType as a System.Data.DbType. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static System.Data.DbType ToDbType(this string DBType)
        {
            switch (DBType.ToLower())
            {
                case "varchar":
                    return System.Data.DbType.AnsiStringFixedLength;
                case "nvarchar":
                    return System.Data.DbType.StringFixedLength;
                case "int":
                    return System.Data.DbType.Int32;
                case "bigint":
                    return System.Data.DbType.Int64;
                case "binary":
                    return System.Data.DbType.Binary;
                case "bit":
                    return System.Data.DbType.Boolean;
                case "char":
                    return System.Data.DbType.AnsiStringFixedLength;
                case "datetime":
                    return System.Data.DbType.DateTime;
                case "decimal":
                    return System.Data.DbType.Decimal;
                case "float":
                    return System.Data.DbType.Double;
                case "image":
                    return System.Data.DbType.Object;
                case "money":
                    return System.Data.DbType.Currency;
                case "nchar":
                    return System.Data.DbType.StringFixedLength;
                case "ntext":
                    return System.Data.DbType.String;
                case "text":
                    return System.Data.DbType.AnsiString;
                case "smallint":
                    return System.Data.DbType.Int16;
                case "uniqueidentifier":
                    return System.Data.DbType.Guid;
                case "tinyint":
                    return System.Data.DbType.Byte;
                case "varbinary":
                    return System.Data.DbType.Binary;
                case "guid":
                    return System.Data.DbType.Guid;
                case "string":
                    return System.Data.DbType.String;
                case "sysname":
                    return System.Data.DbType.String;
                case "real":
                    return System.Data.DbType.Decimal;
                case "date":
                    return DbType.Date;
                case "numeric":
                    return DbType.Decimal;
                case "xml":
                    return DbType.String;
                case "smalldatetime":
                    return DbType.DateTime;
                case "datatable":
                    return DbType.Object;
                default:
                    return DbType.Object;

            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Turns An XML String Into A Dictionary. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    . </param>
        ///
        /// <returns>   x as a Dictionary&lt;string,string&gt; </returns>
        ///-------------------------------------------------------------------------------------------------

        public static Dictionary<string, string> ToDictionaryFromFormattedXML(this string x)
        {
            Dictionary<string, string> _TmpReturn = new Dictionary<string, string>();

            System.Xml.XPath.XPathDocument _Doc = new System.Xml.XPath.XPathDocument(new System.IO.StringReader(x));

            var _Nav = _Doc.CreateNavigator();

            _Nav.MoveToChild("root", "");
            _Nav.MoveToChild("items", "");

            var _Iter = _Nav.SelectDescendants(System.Xml.XPath.XPathNodeType.Element, false);

            while (_Iter.MoveNext())
            {
                _TmpReturn.Add(_Iter.Current.GetAttribute("key", ""), _Iter.Current.GetAttribute("value", ""));
            }

            return _TmpReturn;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that parse web page name. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="SCRIPTNAME">   The SCRIPTNAME to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ParseWeb_Page_Name(this string SCRIPTNAME)
        {
            var Parts = SCRIPTNAME.SplitString("/", StringSplitOptions.RemoveEmptyEntries);

            string _TmpReturn = "";
            bool _Start = false;
            foreach (var o in Parts)
            {
                if (_Start)
                {
                    _TmpReturn += o + "/";
                }

                if (o.ToLower().Contains(".com") || o.ToLower().Contains(".org") || o.ToLower().Contains(".net")
                || o.ToLower().Contains(".info") || o.ToLower().Contains(".us") || o.ToLower().Contains(".gov")
                || o.ToLower().Contains(".edu") || o.ToLower().Contains(".mil") || o.ToLower().Contains(".int")
                || o.ToLower().Contains(".info") || o.ToLower().Contains(".us") || o.ToLower().Contains(".gov")
                )
                {
                    _TmpReturn = "/";
                    _Start = true;
                }

            }

            if (_Start == false)
            {
                return SCRIPTNAME;
            }

            return _TmpReturn.TrimEnd("/");
        }
    }
}

namespace ACT.Core.Extensions.CodeGenerator
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    /// This is Needed In Order To Allow The Code Generation to Create Valid C# Formatted Characters.
    /// </summary>
    ///
    /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class CodeGeneration
    {
        /// <summary>   The reserved keywords. </summary>
        public static string[] __ReservedKeywords = new string[] { "abstract", "as", "base", "bool", "break", "by", "case", "catch", "char", "checked", "class", "con", "continue", "decimal", "default", "delegate", "do", "doub", "else", "enum", "event", "explicit", "extern", "fal", "finally", "fixed", "float", "for", "foreach", "go", "if", "implicit", "in", "int", "interface", "intern", "is", "lock", "long", "namespace", "new", "nu", "object", "operator", "out", "override", "params", "priva", "protected", "public", "readonly", "ref", "return", "sby", "sealed", "short", "sizeof", "stackalloc", "static", "stri", "struct", "switch", "this", "throw", "true", "t", "typeof", "uint", "ulong", "unchecked", "unsafe", "usho", "using", "virtual", "void", "volatile", "whi", "System", "ACT", "Microsoft", "Core" };

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that converts an x to a C# friendly name. </summary>
        ///
        /// <remarks>   Mark Alicz, 12/8/2016. </remarks>
        ///
        /// <param name="x">    The x to act on. </param>
        ///
        /// <returns>   x as a string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ToCSharpFriendlyName(this string x)
        {

            var m = Regex.Match(x, "^[A-Za-z]");
            if (m.Success == false)
            {
                x = "_" + x;
            }

            x = x.Replace("$", "dollar");
            x = x.Replace("-", "dash");
            x = x.Replace(" ", "_");
            x = x.Replace("!", "exclamation");
            x = x.Replace("@", "at");
            x = x.Replace("#", "pound");
            x = x.Replace("%", "percent");
            x = x.Replace("^", "carrot");
            x = x.Replace("&", "and");
            x = x.Replace("*", "times");
            x = x.Replace("(", "leftparent");
            x = x.Replace(")", "rightparent");
            x = x.Replace("?", "questionmark");
            x = x.Replace("/", "slash");
            x = x.Replace("\\", "backslash");


            foreach (var s in __ReservedKeywords)
            {
                if (s == x)
                {
                    x = x + "_R";
                }
            }
            return x;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   To MSSQL Friendly Name. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/7/2019. </remarks>
        ///
        /// <param name="x">    String To Validate. </param>
        ///
        /// <returns>   Validated String. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ToMSSQLFriendlyName(this string x)
        {
            string[] _AllowedCharacters = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,_,@,#,$".SplitString(",", StringSplitOptions.RemoveEmptyEntries);

            string _replacement = "";
            foreach (char c in x.ToArray())
            {
                if (_AllowedCharacters.Contains(c.ToString()) == true) { _replacement = _replacement + c; }
                else { _replacement = _replacement + "_"; }
            }
            return _replacement;
        }
    }
}
