using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.EntityModels
{
    public class Enumeration
    {
        public enum ResultCode
        {
            Failed = 0,
            Success = 1,    
            Authenticated = 204,   
            UserNotFound = 205,    
            PasswordNotCorrect = 206,
            NotTrueParam = 199,
            NotExistFile  = 207,
            NotExistFolder = 208
        }

        public enum HouseType
        {
            Street =1,//nhà mặt phố
            Alley = 2 , //Ngõ, hẻm
            Villa = 3, //Nhà biệt thự
            AdjoiningStreet =4 // Nhà phố liền kề
        }
    }
}
