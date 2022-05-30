using Microsoft.Extensions.Configuration;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces.Login;
using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.DL.DatabaseLayer.Login
{
    public class DLLogin: DLBase<LoginParam>, IDLLogin
    {
        public DLLogin(WhereHouseContext context) : base(context)
        {
        }

        public LoginEntity Login(string Username)
        {
            var entity = new LoginEntity();
            //LoginEntity entity = _dbConnection.Query<LoginEntity>($"Proc_GetLogin", new { @Username = Username }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        public int UpdatePasswordByEmployeeCode(string employeecode, string password, string salt)
        {
            var rows = 0;
            //rows = _dbConnection.Execute("Proc_UpdatePasswordByEmployeeCode", new { EmployeeCode = employeecode, Password = password, Salt = salt }, commandType: CommandType.StoredProcedure);
            return rows;
        }

    }
}
