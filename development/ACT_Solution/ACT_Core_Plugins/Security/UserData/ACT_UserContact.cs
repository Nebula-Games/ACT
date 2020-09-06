using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Security.UserData;
using ACT.Core.Interfaces.Security.Authentication;
namespace ACT.Plugins.Security.UserData
{
    public class ACT_UserContact : ACT_Core, I_UserContact
    {
        public string CompanyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FirstName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LastName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MiddleName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WorkPhone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WorkPhoneExt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MobilePhone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OtherPhone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BirthDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
