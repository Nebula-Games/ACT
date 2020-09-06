///-------------------------------------------------------------------------------------------------
// file:	Types\UserProfile\JSON_UserMetaData.cs
//
// summary:	Implements the JSON user meta data class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ACT.Core.Types.UserProfile
{
    
    public class JSON_UserMetaData
    {
        Dictionary<string, string> BaseProperties = new Dictionary<string, string>();



        public JSON_UserMetaData(string JSONData)
        {
            var _ProfileMetaData = JObject.Parse(JSONData);

            foreach(var prop in _ProfileMetaData)
            {
                if (prop.Value is JArray)
                {

                }
                else if (prop.Value is JProperty)
                {

                }
                else if (prop.Value is JObject)
                {

                }
                else
                {

                }
            }

        }
    }
}
