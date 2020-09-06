///-------------------------------------------------------------------------------------------------
// file:	Types\Data\ACT_DictionaryOfHolding.cs
//
// summary:	Implements the act dictionary of holding class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Types.Data
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Recursive Dictionary Capable Of Holding an Infinate Structure of Datai,e: Deep JSON File </summary>
    ///
    /// <remarks>   Mark Alicz, 10/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public class ACT_DictionaryOfHolding
    {
        public Dictionary<string, string> Data = new Dictionary<string, string>();

        public Dictionary<string, ACT_DictionaryOfHolding> Children = new Dictionary<string, ACT_DictionaryOfHolding>();

        public bool HasChildren { get; set; }

    }
}
