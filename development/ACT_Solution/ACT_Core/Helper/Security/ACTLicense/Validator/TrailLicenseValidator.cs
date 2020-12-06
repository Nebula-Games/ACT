namespace ACT.Core.Helper.Security.ACTLicense.Validator
{
	internal sealed class TrailLicenseValidator : AbstractLicenseValidator
	{
		public TrailLicenseValidator(string publicKey)
			: base(publicKey)
		{
			// disable .
			DisableFutureChecks();

			LicenseAttributes.Clear();
		}

		protected override string License { get; set; }
	}
}