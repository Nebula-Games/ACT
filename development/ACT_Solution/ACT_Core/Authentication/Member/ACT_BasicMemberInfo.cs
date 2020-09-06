using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Authentication.Member
{
    public class ACT_BasicMemberInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AliasName { get; set; }
        public string FullName { get; set; }
        public string FullNameWithAlias { get; set; }
        public string EmailAddress { get; set; }
        public string EmailAddressSecondary { get; set; }
        public bool UseEmailForLoginUserName { get; set; }
        public string LoginUserName { get; set; }
        public string Password { get; set; }

        public AddressInformation ShippingInformation { get; set; }
        public AddressInformation BillingInformation { get; set; }

        public class AddressInformation
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string City { get; set; }
            public string State_Province { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
        }

    }
}
