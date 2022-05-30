using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Utils
{
    public static class ExtensionMethod
    {
        public static object GetValueByKey<TEntity>(this TEntity obj,string key)
        {
            return  obj.GetType().GetProperty(key).GetValue(obj);
        }
        public static string ByteArrayToBase64String(this byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }
        public static byte[] Base64StringToByteArray(this string str)
        {
            return Convert.FromBase64String(str);
        }
    }
}
