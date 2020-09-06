#region Using

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ACT.Core.Helper;
using ACT.Core.Interfaces.Security.Encryption;
using ACT.Core.Types;

#endregion

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The invalid
        /// </summary>
        internal static bool invalid;

        #region Private Methods


        /// <summary>
        /// Domains the mapper.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>System.String.</returns>
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        #endregion



        #region Public Methods

        /// <summary>
        /// Parse HTTP URL
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        /// <summary>
        /// Parses the HTTP URL.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> ParseHTTP_URL(this string URL)
        {
            List<string> _Data = new List<string>();

            if (URL.Contains("http://"))
            {
                _Data.Add("http");
            }
            else if (URL.Contains("https://"))
            {
                _Data.Add("https");
            }
            else
            {
                _Data.Add("unknown");
            }
            string _StrippedURL = URL.Replace("http://", "");
            _StrippedURL = _StrippedURL.Replace("https://", "");

            string[] _sData = _StrippedURL.SplitString("/", StringSplitOptions.RemoveEmptyEntries);

            string _DomainInfo = _sData[0];
            string _Extension = _DomainInfo.Substring(_DomainInfo.LastIndexOf("."));
            _DomainInfo = _DomainInfo.Substring(0, _DomainInfo.LastIndexOf("."));
            _Extension = _Extension.Replace(".", "");
            string _DomainName = "";
            string _SubDomain = "";
            if (_DomainInfo.Contains("."))
            {
                _DomainName = _DomainInfo.Substring(_DomainInfo.LastIndexOf(".")).Replace(".", "");
            }
            else { _DomainName = _DomainInfo; }

            if (_DomainInfo.Length > 0)
            {
                _DomainInfo = _DomainInfo.Substring(0, _DomainInfo.LastIndexOf("."));
                _SubDomain = _DomainInfo;
            }

            _Data.Add(_SubDomain); _Data.Add(_DomainName); _Data.Add(_Extension);

            for (int x = 1; x < _sData.Count(); x++)
            {
                _Data.Add(_sData[x]);
            }

            return _Data;

        }

        /// <summary>
        /// Adds the spaces to sentence.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="preserveAcronyms">if set to <c>true</c> [preserve acronyms].</param>
        /// <returns>System.String.</returns>
        public static string AddSpacesToSentence(this string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (var i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if (text[i - 1] != ' ' && !char.IsUpper(text[i - 1]) ||
                        preserveAcronyms && char.IsUpper(text[i - 1]) &&
                        i < text.Length - 1 && !char.IsUpper(text[i + 1]))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        /// <summary>
        ///     Compute the distance between two strings.
        /// </summary>
        /// <summary>
        /// Computes the string difference.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <returns>System.Int32.</returns>
        public static int ComputeStringDifference(this string s, string t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
                return m;

            if (m == 0)
                return n;

            // Step 2
            for (var i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (var i = 1; i <= n; i++)
                //Step 4
                for (var j = 1; j <= m; j++)
                {
                    // Step 5
                    var cost = t[j - 1] == s[i - 1] ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            // Step 7
            return d[n, m];
        }


        /// <summary>
        /// Converts to securestring.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>SecureString.</returns>
        public static SecureString ToSecureString(this string x)
        {
            var tmpReturn = new SecureString();
            foreach (var c in x)
                tmpReturn.AppendChar(c);
            return tmpReturn;
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        public static string EncryptString(this string x)
        {
            return CurrentCore<I_Encryption>.GetCurrent().Encrypt(x);
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="EncryptionKey">Encryption Key</param>
        /// <returns>System.String.</returns>
        public static bool EncryptFile(this string x, string EncryptionKey = "")
        {
            if (x.FileExists() == false) { throw new System.IO.FileNotFoundException(x); }
            if (EncryptionKey.NullOrEmpty()) { EncryptionKey = ACT.Core.SystemSettings.GetSettingByName("EncryptionKey").Value; }

            var _ENC = CurrentCore<I_Encryption>.GetCurrent();
            string _Exten = x.GetExtensionFromFileName();
            string _TmpFile = x.Replace(_Exten, "enct");
            _ENC.Encrypt(x, _TmpFile, EncryptionKey);

            if (_TmpFile.FileExists() == false) { throw new Exception("Unknown Error"); }

            try
            {
                x.DeleteFile(50, true);
                _TmpFile.CopyFileTo(x, true);
                _TmpFile.DeleteFile(50, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown Error: ", ex);
            }

            if (x.FileExists()) { return true; }
            else { return false; }

        }


        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        public static string DecryptString(this string x)
        {
            return CurrentCore<I_Encryption>.GetCurrent().Decrypt(x);
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.String.</returns>
        public static string EncryptString(this string x, string Password)
        {
            return CurrentCore<I_Encryption>.GetCurrent().Encrypt(x, Password);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="Password">The password.</param>
        /// <returns>System.String.</returns>
        public static string DecryptString(this string x, string Password)
        {
            return CurrentCore<I_Encryption>.GetCurrent().Decrypt(x, Password);
        }

        /// <summary>
        ///     Checks the string for a valid Phone Number Format
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns>bool:true or false</returns>
        /// <summary>
        /// Determines whether [is valid phone number] [the specified phone].
        /// </summary>
        /// <param name="Phone">The phone.</param>
        /// <returns><c>true</c> if [is valid phone number] [the specified phone]; otherwise, <c>false</c>.</returns>
        public static bool IsValidPhoneNumber(this string Phone)
        {
            var _phonecheck = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

            if (_phonecheck.Matches(Phone).Count > 0)
                return true;

            return false;
        }

        private static bool _hasXMLError = false;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that query if 'XMLToTest' is valid XML. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/20/2019. </remarks>
        ///
        /// <param name="XMLToTest">    The XMLToTest to act on. </param>
        ///
        /// <returns>   True if valid xml, false if not. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool IsValidXML(this string XMLToTest)
        {            
            var _settings = new System.Xml.XmlReaderSettings();
            _settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.AllowXmlAttributes;
            _settings.ValidationEventHandler += _settings_ValidationEventHandler;

            using (var reader = System.Xml.XmlReader.Create(XMLToTest, _settings, new System.Xml.XmlParserContext(null, new System.Xml.XmlNamespaceManager(new System.Xml.NameTable()), null, System.Xml.XmlSpace.None)))
            {
                while (reader.Read()) {; }
            }
            // return FALSE XML Error
            return !_hasXMLError;
        }

        private static void _settings_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == System.Xml.Schema.XmlSeverityType.Error)
            {
                _hasXMLError = true;
            }            
        }

        /// <summary>
        /// Checks for a Valid Email
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        /// <summary>
        /// Determines whether [is valid email] [the specified string in].
        /// </summary>
        /// <param name="strIn">The string in.</param>
        /// <returns><c>true</c> if [is valid email] [the specified string in]; otherwise, <c>false</c>.</returns>
        public static bool IsValidEmail(this string strIn)
        {
            invalid = false;
            if (string.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Checks for Password Complexity
        /// </summary>
        /// <param name="pwd">Self Ref Password To check</param>
        /// <param name="minLength">Default 8</param>
        /// <param name="numUpper">Upper Case Count - Default 1</param>
        /// <param name="numLower">Lower Case Count - Default 1</param>
        /// <param name="numNumbers">Number Count - Default 0</param>
        /// <param name="numSpecial">Special Character Count - Default 1</param>
        /// <returns>bool:true or false</returns>
        /// <summary>
        /// Determines whether the specified minimum length is complex.
        /// </summary>
        /// <param name="pwd">The password.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="numUpper">The number upper.</param>
        /// <param name="numLower">The number lower.</param>
        /// <param name="numNumbers">The number numbers.</param>
        /// <param name="numSpecial">The number special.</param>
        /// <returns><c>true</c> if the specified minimum length is complex; otherwise, <c>false</c>.</returns>
        public static bool IsComplex(this string pwd, int minLength = 8, int numUpper = 1, int numLower = 1,
            int numNumbers = 0, int numSpecial = 1)
        {
            var upper = new Regex("[A-Z]");
            var lower = new Regex("[a-z]");
            var number = new Regex("[0-9]");
            var special = new Regex("[^a-zA-Z0-9]");

            // ' Check the length. 
            if (pwd.Length < minLength) return false;
            // ' Check for minimum number of occurrences. 
            if (upper.Matches(pwd).Count < numUpper) return false;
            if (lower.Matches(pwd).Count < numLower) return false;
            if (number.Matches(pwd).Count < numNumbers) return false;
            if (special.Matches(pwd).Count < numSpecial) return false;

            //' Passed all checks. 
            return true;
        }

        /// <summary>
        ///     Converts a string to an escaped JavaString string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        ///     The JS string.
        /// </returns>
        /// <summary>
        /// Converts to jsstring.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ToJsString([CanBeNull] this string str)
        {
            if (!str.IsSet())
                return str;

            str = str.Replace("\\", @"\\");
            str = str.Replace("'", @"\'");
            str = str.Replace("\r", @"\r");
            str = str.Replace("\n", @"\n");
            str = str.Replace("\"", @"\""");

            return str;
        }

        /// <summary>
        ///     Function to check a max word length, used i.e. in topic names.
        /// </summary>
        /// <param name="text">
        ///     The raw string to format
        /// </param>
        /// <param name="maxWordLength">
        ///     The max Word Length.
        /// </param>
        /// <returns>
        ///     The formatted string
        /// </returns>
        /// <summary>
        /// Ares the maximum length of any words over.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxWordLength">Maximum length of the word.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreAnyWordsOverMaxLength([NotNull] this string text, int maxWordLength)
        {
            //CodeContracts.VerifyNotNull(text, "text");

            if (maxWordLength <= 0 || text.Length <= 0)
                return false;

            var overMax = text.Split(' ').Where(w => w.IsSet() && w.Length > maxWordLength);

            return overMax.Any();
        }

        /// <summary>
        ///     Function to remove words in a string based on a max string length, used i.e. in search.
        /// </summary>
        /// <param name="text">
        ///     The raw string to format
        /// </param>
        /// <param name="maxStringLength">
        ///     The max string length.
        /// </param>
        /// <returns>
        ///     The formatted string
        /// </returns>
        /// <summary>
        /// Trims the words over maximum length words preserved.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="maxStringLength">Maximum length of the string.</param>
        /// <returns>System.String.</returns>
        public static string TrimWordsOverMaxLengthWordsPreserved([NotNull] this string text, int maxStringLength)
        {
            // CodeContracts.VerifyNotNull(text, "text");

            var newText = string.Empty;

            if (maxStringLength <= 0 || text.Length <= 0)
                return newText.Trim();

            var texArr = text.Trim().Split(' ');

            var length = 0;
            var count = 0;
            foreach (var s in texArr)
            {
                length += s.Length;
                if (length > maxStringLength)
                {
                    if (count == 0)
                        newText = string.Empty;

                    break;
                }

                count++;
                newText = newText + " " + s;
            }

            return newText.Trim();
        }

        /// <summary>
        ///     Fast index of.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        ///     The fast index of.
        /// </returns>
        /// <summary>
        /// Fasts the index of.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>System.Int32.</returns>
        public static int FastIndexOf([NotNull] this string source, [NotNull] string pattern)
        {
            //  CodeContracts.VerifyNotNull(source, "source");
            //   CodeContracts.VerifyNotNull(pattern, "pattern");

            if (pattern.Length == 0) return 0;

            if (pattern.Length == 1) return source.IndexOf(pattern[0]);

            var limit = source.Length - pattern.Length + 1;
            if (limit < 1) return -1;

            // Temp the first 2 characters of "pattern"
            var c0 = pattern[0];
            var c1 = pattern[1];

            // Find the first occurrence of the first character
            var first = source.IndexOf(c0, 0, limit);
            while (first != -1)
            {
                // Check if the following character is the same like
                // the 2nd character of "pattern"
                if (source[first + 1] != c1)
                {
                    first = source.IndexOf(c0, ++first, limit - first);
                    continue;
                }

                // Check the rest of "pattern" (starting with the 3rd character)
                var found = true;
                for (var j = 2; j < pattern.Length; j++)
                {
                    if (source[first + j] == pattern[j])
                        continue;

                    found = false;
                    break;
                }

                // If the whole word was found, return its index, otherwise try again
                if (found)
                    return first;

                first = source.IndexOf(c0, ++first, limit - first);
            }

            return -1;
        }

        /// <summary>
        ///     Does an action for each character in the input string. Kind of useless, but in a
        ///     useful way. ;)
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="forEachAction">For each action.</param>
        /// <summary>
        /// Fors the each character.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="forEachAction">For each action.</param>
        public static void ForEachChar([NotNull] this string input, [NotNull] Action<char> forEachAction)
        {
            // CodeContracts.VerifyNotNull(input, "input");
            // CodeContracts.VerifyNotNull(forEachAction, "forEachAction");

            foreach (var c in input)
                forEachAction(c);
        }

        /// <summary>
        ///     Formats a string with the provided parameters
        /// </summary>
        /// <param name="s">
        ///     The s.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        /// <returns>
        ///     The formatted string
        /// </returns>
        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>System.String.</returns>
        [StringFormatMethod("s")]
        public static string FormatWith(this string s, params object[] args)
        {
            return s.IsNotSet() ? null : string.Format(s, args);
        }


        /// <summary>
        /// Removes empty strings from the list
        /// </summary>
        /// <param name="inputList">The input list.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="inputList" /> is <c>null</c>.</exception>
        [NotNull]
        public static List<string> GetNewNoEmptyStrings([NotNull] this IEnumerable<string> inputList)
        {
            // CodeContracts.VerifyNotNull(inputList, "inputList");

            return inputList.Where(x => x.IsSet()).ToList();
        }

        /// <summary>
        /// Removes strings that are smaller then <paramref name="minSize" />
        /// </summary>
        /// <param name="inputList">The input list.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        [NotNull]
        public static List<string> GetNewNoSmallStrings([NotNull] this IEnumerable<string> inputList, int minSize)
        {
            // CodeContracts.VerifyNotNull(inputList, "inputList");

            return inputList.Where(x => x.Length >= minSize).ToList();
        }

        /// <summary>
        /// When the string is trimmed, is it <see langword="null" /> or empty?
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>The is <see langword="null" /> or empty trimmed.</returns>
        [ContractAnnotation("str:null => true")]
        public static bool IsNotSet([CanBeNull] this string inputString)
        {
            return string.IsNullOrWhiteSpace(inputString);
        }

        /// <summary>
        /// When the string is trimmed, is it <see langword="null" /> or empty?
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>The is <see langword="null" /> or empty trimmed.</returns>
        [ContractAnnotation("str:null => false")]
        public static bool IsSet([CanBeNull] this string inputString)
        {
            return !string.IsNullOrWhiteSpace(inputString);
        }



        /// <summary>
        /// Removes multiple single quote ' characters from a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The remove multiple single quotes.</returns>
        public static string RemoveMultipleSingleQuotes(this string text)
        {
            var result = string.Empty;
            if (text.IsNotSet())
                return result;

            var r = new Regex(@"\'");
            return r.Replace(text, @"'");
        }

        /// <summary>
        /// Removes multiple whitespace characters from a string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The remove multiple whitespace.</returns>
        public static string RemoveMultipleWhitespace(this string text)
        {
            var result = string.Empty;
            if (text.IsNotSet())
                return result;

            var r = new Regex(@"\s+");
            return r.Replace(text, @" ");
        }

        /// <summary>
        /// Converts a string into it's hexadecimal representation.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>The string to hex bytes.</returns>
        public static string StringToHexBytes(this string inputString)
        {
            var result = string.Empty;
            if (inputString.IsNotSet())
                return result;

            var cryptoServiceProvider = new MD5CryptoServiceProvider();

            var emailBytes = System.Text.Encoding.UTF8.GetBytes(inputString);
            emailBytes = cryptoServiceProvider.ComputeHash(emailBytes);

            var s = new StringBuilder();

            foreach (var b in emailBytes)
                s.Append(b.ToString("x2").ToLower());

            return s.ToString();
        }

        /// <summary>
        /// Converts a string to a list using delimiter.
        /// </summary>
        /// <param name="str">starting string</param>
        /// <param name="delimiter">value that delineates the string</param>
        /// <returns>list of strings</returns>
        public static List<string> StringToList(this string str, char delimiter)
        {
            return str.StringToList(delimiter, new List<string>());
        }

        /// <summary>
        /// Converts a string to a list using delimiter.
        /// </summary>
        /// <param name="str">starting string</param>
        /// <param name="delimiter">value that delineates the string</param>
        /// <param name="exclude">items to exclude from list</param>
        /// <returns>list of strings</returns>
        [NotNull]
        public static List<string> StringToList(
            [NotNull] this string str,
            char delimiter,
            [NotNull] List<string> exclude)
        {
            //    CodeContracts.VerifyNotNull(str, "str");
            //   CodeContracts.VerifyNotNull(exclude, "exclude");

            var list = str.Split(delimiter).ToList();

            list.RemoveAll(exclude.Contains);
            list.Remove(delimiter.ToString());

            return list;
        }

        /// <summary>
        /// Creates a delimited string an enumerable list of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList">The object list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The list to string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="objList" /> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">objList;objList is null.</exception>
        public static string ToDelimitedString<T>(this IEnumerable<T> objList, string delimiter) where T : IConvertible
        {
            if (objList == null)
                throw new ArgumentNullException("objList", "objList is null.");

            var sb = new StringBuilder();

            objList.ForEachFirst(
                (x, isFirst) =>
                {
                    if (!isFirst)
                        sb.Append(delimiter);

                    // append string...
                    sb.Append(x);
                });

            return sb.ToString();
        }

        /// <summary>
        /// Converts to jsonarray.
        /// </summary>
        /// <param name="objList">The object list.</param>
        /// <returns>System.String.</returns>
        public static string ToJsonArray(this List<string> objList)
        {
            string _TmpReturn = "[ ";
            foreach (var i in objList)
            {
                _TmpReturn += "\"" + i + "\", ";
            }
            _TmpReturn = _TmpReturn.TrimEnd(",");
            _TmpReturn += "]";
            return _TmpReturn;
        }
        /// <summary>
        /// Cleans a string into a proper RegEx statement.
        /// E.g. "[b]Whatever[/b]" will be converted to:
        /// "\[b\]Whatever\[\/b\]"
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The to reg ex string.</returns>
        [NotNull]
        public static string ToRegExString([NotNull] this string input)
        {
            //  CodeContracts.VerifyNotNull(input, "input");

            var sb = new StringBuilder();

            input.ForEachChar(
                c =>
                {
                    if (!char.IsWhiteSpace(c) && !char.IsLetterOrDigit(c) && c != '_')
                        sb.Append("\\");

                    sb.Append(c);
                });

            return sb.ToString();
        }

        /// <summary>
        /// Checks the length of the password.
        /// </summary>
        /// <param name="Password">The password.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckPasswordLength(this string Password)
        {
            if (Password == null || Password == "" || Password.Length < 3)
                return false;

            return true;
        }

        /// <summary>
        /// Truncates a string with the specified limits and adds (...) to the end if truncated
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="inputLimit">The input limit.</param>
        /// <param name="cutOfString">The cut of string.</param>
        /// <returns>truncated string</returns>
        public static string Truncate(
            [CanBeNull] this string input,
            int inputLimit,
            [NotNull] string cutOfString = "...")
        {
            //   CodeContracts.VerifyNotNull(cutOfString, "cutOfString");

            var output = input;

            if (input.IsNotSet())
                return null;

            var limit = inputLimit - cutOfString.Length;

            // Check if the string is longer than the allowed amount
            // otherwise do nothing
            if (output.Length > limit && limit > 0)
            {
                // cut the string down to the maximum number of characters
                output = output.Substring(0, limit);

                // Check if the space right after the truncate point 
                // was a space. if not, we are in the middle of a word and 
                // need to cut out the rest of it
                if (input.Substring(output.Length, 1) != " ")
                {
                    var lastSpace = output.LastIndexOf(" ");

                    // if we found a space then, cut back to that space
                    if (lastSpace != -1)
                        output = output.Substring(0, lastSpace);
                }

                // Finally, add the the cut off string...
                output += cutOfString;
            }

            return output;
        }

        /// <summary>
        /// Truncates a string with the specified limits by adding (...) to the middle
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="limit">max size of string</param>
        /// <returns>truncated string</returns>
        public static string TruncateMiddle(this string input, int limit)
        {
            if (input.IsNotSet())
                return null;

            var output = input;
            const string middle = "...";

            // Check if the string is longer than the allowed amount
            // otherwise do nothing
            if (output.Length > limit && limit > 0)
            {
                // figure out how much to make it fit...
                var left = limit / 2 - middle.Length / 2;
                var right = limit - left - middle.Length / 2;

                if (left + right + middle.Length < limit)
                    right++;
                else if (left + right + middle.Length > limit)
                    right--;

                // cut the left side
                output = input.Substring(0, left);

                // add the middle
                output += middle;

                // add the right side...
                output += input.Substring(input.Length - right, right);
            }

            return output;
        }

        /// <summary>
        /// Convert a input string to a byte array and compute the hash.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>The Hexadecimal string.</returns>
        public static string ToMd5Hash(this string value)
        {
            if (value.IsNotSet())
                return value;

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                var originalBytes = System.Text.Encoding.Default.GetBytes(value);
                var encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// Convert a input string to a byte array
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>The Byte String</returns>
        public static byte[] ToBytes(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Converts to bytearrayfrombinarystring.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToByteArrayFromBinaryString(this string x)
        {
            return Byte.FromBinaryString(x);
        }



        /// <summary>
        /// Generates The Stream From a String
        /// </summary>
        /// <param name="s">String To Convert To A Stream</param>
        /// <returns>Stream</returns>
        public static System.IO.Stream GenerateStreamFromString(this string s)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        #endregion
    }
}