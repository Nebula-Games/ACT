using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.UserData;
using ACT.Core.Interfaces.Security.Authentication;

namespace ACT.Plugins.Security.UserData
{
    public class ACT_UserAddress : ACT_Core, I_UserAddress
    {
        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Address1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Address2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Address3 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string City { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PostalCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Phone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Fax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AddressType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryShipping { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryBilling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPrimaryContact { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public I_TestResult Delete()
        {
            throw new NotImplementedException();
        }

        public I_TestResult Save()
        {
            throw new NotImplementedException();
        }

       
    }
}
