using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ACT.WebAPI.Controllers.Authentication
{
  
    [ApiController]
    public class Authenticate : ControllerBase
    {

        
        [HttpGet]
        [Route("/api/authenticateapplication2222/")]
         //[SecurityCritical( SecurityCriticalScope.Explicit)]
        public string AuthenticateApplication(string JSONData)
        {
          
             
                return "true";
        }
    }
}
