using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeeMoreApp.WebUI.Infrastructure.Abstract;
using System.Web.Security;

//The implementation of the Authenticate model calls the static methods that we wanted to keep out 
//of the controller.

namespace SeeMoreApp.WebUI.Infrastructure.Concrete
{

    public class FormsAuthProvider : IAuthProvider
    {

        public bool Authenticate(string username, string password)
        {

            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}