using System;
using System.Collections.Generic;
using System.Text;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IAuthenticationManager
    {
        public Object Authenticate(string username, string password);
    }
}
