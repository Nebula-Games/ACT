///-------------------------------------------------------------------------------------------------
// file:	Interfaces\WebServices\Public\I_DataStore.cs
//
// summary:	Declares the I_DataStore interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.WebServices.Public
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for data store.  Public Access. </summary>
    ///
    /// <remarks>   Mark Alicz, 8/29/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public interface I_DataStore
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Stores a data. </summary>
        ///
        /// <param name="Key">  The key. </param>
        /// <param name="PW">   The password. </param>
        /// <param name="Name"> The name. </param>
        /// <param name="Data"> The data. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        bool StoreData(Guid Key, string PW, string Name, byte[] Data);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a data. </summary>
        ///
        /// <param name="Key">  The key. </param>
        /// <param name="PW">   The password. </param>
        /// <param name="Name"> The name. </param>
        ///
        /// <returns>   An array of byte. </returns>
        ///-------------------------------------------------------------------------------------------------

        byte[] GetData(Guid Key, string PW, string Name);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Deletes the data. </summary>
        ///
        /// <param name="Key">  The key. </param>
        /// <param name="PW">   The password. </param>
        /// <param name="Name"> The name. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        bool DeleteData(Guid Key, string PW, string Name);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets storage statistics. </summary>
        ///
        /// <param name="Key">  The key. </param>
        /// <param name="PW">   The password. </param>
        ///
        /// <returns>   An array of int. </returns>
        ///-------------------------------------------------------------------------------------------------

        int[] GetStorageStats(Guid Key, string PW);

    }
}
