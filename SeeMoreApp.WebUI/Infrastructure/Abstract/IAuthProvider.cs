using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeMoreApp.WebUI.Infrastructure.Abstract
{
    interface IAuthProvider
    {

        bool Authenticate(string username, string password);
    }
}
