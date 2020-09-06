///-------------------------------------------------------------------------------------------------
// file:	Security\Licensing\I_LicenseEngine.cs
//
// summary:	Declares the I_LicenseEngine interface
///-------------------------------------------------------------------------------------------------

namespace ACT.Core.Security.Licensing
{
    /// <summary>
    /// Represents a License Engine To Process the Data
    /// </summary>
    public interface I_LicenseEngine
    {
        /// <summary>
        /// Generate License Method.  Should Not Be Redistributed to End Users
        /// </summary>
        /// <param name="LicenseData"></param>
        /// <returns></returns>
        [ACT.Core.CustomAttributes.RestrictPublicUsage(RequiredFilePath = @"c:\program files(x86)\act\checksumadmin.act", RequiredFileHash = "askjasdjaskdjkasd12dqwisja")]
        byte[] GenerateLicense(dynamic LicenseData);

    }
}
