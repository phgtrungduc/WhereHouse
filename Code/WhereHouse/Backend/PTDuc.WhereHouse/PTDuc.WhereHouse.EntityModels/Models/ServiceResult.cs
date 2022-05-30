using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PTDuc.WhereHouse.EntityModels
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            StatusCode = (int)Enumeration.ResultCode.Success;
        }
        /// <summary>
        /// Dữ liệu trả về (Vd thông tin khách hàng đã thêm)
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Câu thông báo gửi về (Vd: Mã khách hàng bị trùng)
        /// </summary>
        public string Messenger { get; set; }
        /// <summary>
        /// Mã code trả ra tự định nghĩa
        /// </summary>
        public int StatusCode { get; set; }
    }
}
