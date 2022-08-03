using PTDuc.WhereHouse.BL.Interfaces;
using System;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.DL.Models;
using PTDuc.WhereHouse.EntityModels;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLBase<TEntity, TDTO> : IBLBase<TEntity,TDTO>
    {
        protected IDLBase<TEntity,TDTO> _dlBase;
        protected ServiceResult _serviceResult = new ServiceResult();

        protected readonly IMapper _mapper;

        public BLBase(IDLBase<TEntity, TDTO> dlBase, IMapper mapper)
        {
            _dlBase = dlBase;
            _mapper = mapper;
        }

        public virtual bool BeforeInsert(ref TDTO entity)
        {
            return true;
        }

        public bool BeforeUpdate(ref TEntity entity)
        {
            return true;
        }

        public bool Delete(TEntity entity)
        {
            return _dlBase.Delete(entity);
        }

        public IEnumerable<TDTO> GetAll()
        {
            var res = _dlBase.GetAll();
            return res;
        }

        public ServiceResult GetByPaging(int page, int pageSize)
        {
            return _dlBase.GetByPaging(page,pageSize);
        }

        public TDTO GetByID(string Id)
        {
            var data = _dlBase.GetByID(Id);
            var res = default(TDTO);
            if (data != null) {
                res = _mapper.Map<TDTO>(data);
            }
            return res;
        }

        public IEnumerable<TEntity> GetByKey(string key, string value)
        {
            var res = _dlBase.GetByKey(key, value); 
            return res;
        }

        public TEntity GetOneByKey(string key, string value)
        {
            var res = _dlBase.GetOneByKey(key, value);
            return res;
        }

        public virtual bool Insert(TDTO entity)
        {
            var res = false;
            if (this.Validate(entity))
            {
                if (this.ValidateCustom(entity))
                {
                    if (this.BeforeInsert(ref entity))
                    {
                        var mapEntity = _mapper.Map<TEntity>(entity);
                        res = _dlBase.Insert(mapEntity);
                    }
                }
            }
            return res;
        }

        public bool Update(TDTO entity,string id )
        {
            var mapEntity = _mapper.Map<TEntity>(entity);
            return _dlBase.Update(mapEntity,id);
        }


        public bool Validate(TDTO entity)
        {
            var mesArrayError = new List<string>();
            var isValidate = true;
            // Đọc các Property:
            var properties = entity.GetType().GetProperties();

            //Với mỗi thuộc tính, kiểm tra attribute để validate nó
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var propertyName = property.Name;
                //sử dụng để kiểm tra attribute DisplayName cuả các thuộc tính Customer hay entity
                var displayName = property.GetCustomAttributes(false)
                .OfType<DisplayNameAttribute>()
                .FirstOrDefault(); ;

                // Kiểm tra xem có attribute cần phải validate không:
                if (property.IsDefined(typeof(Required), false))
                {
                    // Check bắt buộc nhập:
                    if (propertyValue == null)
                    {
                        isValidate = false;
                        if (displayName != null)
                        {
                            mesArrayError.Add($"Thông tin {displayName.DisplayName} không được phép để trống.");
                        }
                        _serviceResult.Messenger = "Điền thiếu thông tin";
                    }
                }

                if (property.IsDefined(typeof(CheckDuplicate), false))
                {
                    // Check trùng dữ liệu:
                    var entityDuplicate = GetByKey(propertyName, propertyValue.ToString());
                    //entityDuplicate!=null tức là đã tồn tại 1 thằng có giá trị property trùng trên DB
                    if (entityDuplicate != null)
                    {
                        isValidate = false;
                        mesArrayError.Add($"Thông tin {displayName.DisplayName} đã có trên hệ thống.");

                    }
                }

                if (property.IsDefined(typeof(MaxLength), false))
                {
                    //Lấy độ dài đã khai báo
                    var attributeMaxLength = property.GetCustomAttributes(typeof(MaxLength), true)[0];
                    var length = (attributeMaxLength as MaxLength).Value;
                    var msg = (attributeMaxLength as MaxLength).ErrorMsg;
                    if (propertyValue.ToString().Trim().Length > length)
                    {
                        isValidate = false;
                        mesArrayError.Add(msg ?? $"Thông tin {displayName.DisplayName} vượt quá {length} độ dài cho phép");
                        _serviceResult.Messenger = "Thông tin vượt quá độ dài cho phép";
                    }
                }
                //if (property.IsDefined(typeof(Email), false)) {
                //    //Check đúng định dạng email
                //    //var validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                //    //                  + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                //    //                  + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"; 
                //    var validEmailPattern = @"/^[^\s@]+@[^\s@]+$/";


                //    var regCheck = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
                //    var check = regCheck.IsMatch(propertyValue.ToString().Trim());
                //    if (!check) {
                //        isValidate = false;
                //        mesArrayError.Add(Properties.Resources.Msg_EmailTypeFail);
                //        _serviceResult.MISACode = Enums.MISACode.NotValid;
                //        _serviceResult.Messenger = Properties.Resources.Msg_NotValidData;
                //    }
                //}
            }
            _serviceResult.Data = mesArrayError;
            //Với các lớp con kế thừa, nếu muốn validate thêm các thông tin  
            //sẽ override lại phương thức validateCustom và sau đó thực hiện gì kệ nó
            //nhớ phải gọi this.validate không thì nó chỉ gọi của thằng cha mà không gọi đến của thằng con
            if (isValidate == true) isValidate = this.ValidateCustom(entity);
            return isValidate;
        }

        public bool ValidateCustom(TDTO entity)
        {
            return true;
        }
    }
}
    