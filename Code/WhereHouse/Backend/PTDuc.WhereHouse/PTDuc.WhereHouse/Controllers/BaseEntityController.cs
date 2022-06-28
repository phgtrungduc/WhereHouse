using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;

namespace PTDuc.WhereHouse.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class BaseEntityController<TEntity> : Controller where TEntity : class
    {
        IBLBase<TEntity> _BLBase;
        protected WhereHouseContext context = new WhereHouseContext();
        public BaseEntityController(IBLBase<TEntity> BLBase)
        {
            _BLBase = BLBase;
            
        }

        /// <summary>
        /// Lấy toàn bộ 
        /// </summary>
        /// <returns></returns>
        /// createdBy:PTDuc(23/12/2020)
        [HttpGet]
        public virtual IActionResult Get()
        {
            
            return Ok(_BLBase.GetAll());
        }

        // GET api/<CustomersController>/5
        /// <summary>
        /// Tìm kiếm nhân viên theo ID
        /// </summary>
        /// <param name="id">Mã khách hàng</param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// CreatedBy: PTDuc(4/12/2020)
        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            var res = _BLBase.GetByID(id);
            return Ok(res);
        }

        // POST api/<CustomersController>
        /// <summary>
        /// Thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng truyền lên từ client</param>
        /// <returns></returns>
        /// CreatedBy: PTDuc(4/12/2020)
        [HttpPost]
        public virtual IActionResult Post([FromBody] TEntity entity)
        {

            var res = new ServiceResult();
            try
            {
                res.Data = _BLBase.Insert(entity);
                return Ok(res);
            }
            catch (System.Exception e )
            {
                return BadRequest(e);
                throw;
            }
            

        }

        /// <summary>
        /// Mucj ddich
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created By: PTDuc1 - 04.06.200
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] TEntity entity)
        {
            var res = _BLBase.Update(entity);
            return Ok(res);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] TEntity entity)
        {
            var res = _BLBase.Delete(entity);
            return Ok(res);
        }

        //[HttpGet("getbykey")]
        //public IActionResult GetByKey(string key,string value)
        //{
        //    var res = _BLBase.GetByKey(key,value);
        //    return Ok(res);
        //}


        [HttpGet("GetByPaging")]
        public virtual IActionResult GetByPaging(int page,int pageSize)
        {
            return Ok(_BLBase.GetByPaging(page, pageSize));
        }
    }
}
