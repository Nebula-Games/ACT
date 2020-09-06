using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Constants
{
 
    public static class WebConstants
    {
        /// <summary>
        /// The set of characters that are unreserved in RFC 2396 but are NOT unreserved in RFC 3986.
        ///  The URI RFC3986 chars to escape. </summary>
        public static readonly string[] UriRfc3986CharsToEscape = new[] { "!", "*", "'", "(", ")" };


    }
}
