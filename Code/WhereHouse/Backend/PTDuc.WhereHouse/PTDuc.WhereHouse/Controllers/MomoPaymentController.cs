using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
using PTDuc.WhereHouse.EntityModels.Param;
using PTDuc.WhereHouse.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class MomoPaymentController : Controller
    {
        IConfiguration _configuration;
        string _clientEndpoint;
        string _ngrokEndpoint;
        IBLPost _bLPost;
        public MomoPaymentController(IConfiguration configuration, IBLPost bLPost) {
            _configuration = configuration;
            _clientEndpoint = configuration.GetSection("ServerClient").GetSection("Key").Value;
            _ngrokEndpoint = configuration.GetSection("ServerNgrok").GetSection("Key").Value;
            _bLPost = bLPost;
        }
        [HttpPost("SendPayment")]
        public virtual IActionResult SendPayment([FromBody]PostDTO post)
        {
            try
            {
                string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
                string partnerCode = "MOMOINHA20220704";
                string accessKey = "ZbsEOobzPlhIjS6D";
                string serectkey = "x6kOFBcCUGi1u5HsMp8gEIENZD3SxqIy";
                string orderInfo = "Thanh toán phí đăng bài";
                string redirectUrl = _clientEndpoint + "/MyPost";
                string ipnUrl = _ngrokEndpoint + "/api/v1/MomoPayment/SavePayment"; 

                string amount = "3000";
                string orderid = Guid.NewGuid().ToString(); //mã đơn hàng
                string requestId = Guid.NewGuid().ToString();
                string extraData = "";
                if (post != null)
                {
                    extraData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(post)));
                }
                string requestType = "captureWallet";
                //Before sign HMAC SHA256 signature
                string rawHash = "accessKey=" + accessKey +
                    "&amount=" + amount +
                    "&extraData=" + extraData +
                    "&ipnUrl=" + ipnUrl +
                    "&orderId=" + orderid +
                    "&orderInfo=" + orderInfo +
                    "&partnerCode=" + partnerCode +
                    "&redirectUrl=" + redirectUrl +
                    "&requestId=" + requestId +
                    "&requestType=" + requestType
                    ;


                MoMoUtil crypto = new MoMoUtil();
                //sign signature SHA256
                string signature = crypto.signSHA256(rawHash, serectkey);

                //build body json request
                JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "vi" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };

                string responseFromMomo = MoMoUtil.sendPaymentRequest(endpoint, message.ToString());

                JObject jmessage = JObject.Parse(responseFromMomo);

                return Ok(jmessage.GetValue("payUrl").ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("SavePayment")]
        
        public virtual IActionResult SavePayment([FromBody] MomoParam param)
        {
            try
            {
                if (param.resultCode == 0)
                {
                    var extraData = param.extraData;
                    if (!string.IsNullOrEmpty(extraData))
                    {
                        var postDecode = Encoding.UTF8.GetString(Convert.FromBase64String(extraData));
                        var post = JsonConvert.DeserializeObject<PostDTO>(postDecode);
                        var postData = _bLPost.GetByID(post.PostId.ToString());
                        postData.Status = (int)Enumeration.StatusPost.Pay;
                        _bLPost.Update(postData, post.PostId.ToString());
                    }
                }
            }
            catch (Exception)
            {

            }

            return NoContent();
        }
    }
}
