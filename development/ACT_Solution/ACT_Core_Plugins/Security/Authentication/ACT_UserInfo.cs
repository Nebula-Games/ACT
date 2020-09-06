using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces;
using ACT.Core;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Interfaces.Security.UserData;

namespace ACT.Plugins.Security.Authentication
{
    /// <summary>
    /// Generic Implementation of IUserInfo.  Used For Purely Login Purposes Does not support groups, addresses etc..
    /// </summary>
    public class ACT_UserInfo : ACT.Plugins.ACT_Core, I_UserInfo
    {
        public bool Active
        {
            get;
            set;
        }

        public Dictionary<string, string> AdditionalInfo
        {
            get;
            set;
        }

        public bool Authenticated
        {
            get;
            set;
        }
        public List<string> RelatedCompanyIdentifiers { get; set; }
        public string AuthenticationToken { get; set; }
        public string CompanyName
        {
            get;
            set;
        }

        public string ConfirmationCode
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string EncryptionKey
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public List<string> Groups
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string MobilePhone
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string UserKey
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string WorkPhone
        {
            get;
            set;
        }

        public string WorkPhoneExt
        {
            get;
            set;
        }

        public void AddAddress(I_UserAddress NewAddress)
        {
            throw new NotImplementedException();
        }

        public void AddContact(I_UserContact ContactToAdd)
        {
            throw new NotImplementedException();
        }

        public ACT.Core.Interfaces.Common.I_TestResult Delete()
        {
            throw new NotImplementedException();
        }

        public void DeleteAddress(I_UserAddress AddressToDelete)
        {
            throw new NotImplementedException();
        }

        public void DeleteContact(I_UserContact ContactToDelete)
        {
            throw new NotImplementedException();
        }

        public I_UserAddress GetAddressByID(string ID)
        {
            throw new NotImplementedException();
        }

        public I_UserAddress GetAddressByID(Guid ID)
        {
            throw new NotImplementedException();
        }

        public I_UserAddress GetAddressByName(string Name)
        {
            throw new NotImplementedException();
        }

        public List<I_UserAddress> GetAddresses()
        {
            throw new NotImplementedException();
        }

        public I_UserContact GetContact(Guid ID)
        {
            throw new NotImplementedException();
        }

        public List<string> GetGroups()
        {
            throw new NotImplementedException();
        }

        public I_UserAddress GetPrimaryBillingAddress()
        {
            throw new NotImplementedException();
        }

        public I_UserAddress GetPrimaryContactAddress()
        {
            throw new NotImplementedException();
        }

        public I_UserAddress GetPrimaryShippingAddress()
        {
            throw new NotImplementedException();
        }

        public string GetUnencryptedPassword()
        {
            throw new NotImplementedException();
        }

        public List<I_UserContact> GetUserContacts()
        {
            throw new NotImplementedException();
        }

        public void Init(I_SecurityProvider Provider = null)
        {
            throw new NotImplementedException();
        }
        public bool Login(string UserName, string PassWord)
        {
            throw new NotImplementedException();
        }

        public ACT.Core.Interfaces.Common.I_TestResult Save()
        {
            throw new NotImplementedException();
        }
        public List<I_UserContact> Search(string SearchString)
        {
            throw new NotImplementedException();
        }

        public void UpdateAddress(I_UserAddress AddressToUpdate)
        {
            throw new NotImplementedException();
        }
        public void UpdateContact(I_UserContact ContactToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
