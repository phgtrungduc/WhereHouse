using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.DL.Interfaces.Login
{
    public interface IDLLogin:IDLBase<LoginParam, LoginParam>
    {
        LoginEntity Login(string Username);
        int UpdatePasswordByEmployeeCode(string employeecode, string password, string salt);
    }
}
