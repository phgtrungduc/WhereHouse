using PTDuc.WhereHouse.BL.Interfaces;
using System;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.DL.Models;
using PTDuc.WhereHouse.EntityModels;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLBase<TEntity> : IBLBase<TEntity>
    {
        protected IDLBase<TEntity> _dlBase;
        protected ServiceResult _serviceResult = new ServiceResult();
        public BLBase(IDLBase<TEntity> dlBase)
        {
            _dlBase = dlBase;
        }

        public virtual bool BeforeInsert(ref TEntity entity)
        {
            return true;
        }

        public bool BeforeUpdate(ref TEntity entity)
        {
            return true;
        }

        public bool Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var res = _dlBase.GetAll();
            return res;
        }

        public TEntity GetByID(string Id)
        {
            var res = _dlBase.GetByID(Id);
            return res;
        }

        public IEnumerable<TEntity> GetByKey(PropertyInfo prop, TEntity entity)
        {
            var res = _dlBase.GetByKey(prop, entity); 
            return res;
        }

        public TEntity GetOneByKey(PropertyInfo prop, TEntity entity)
        {
            var res = _dlBase.GetOneByKey(prop, entity);
            return res;
        }

        public virtual bool Insert(TEntity entity)
        {
            var res = false;
            if (this.Validate(entity))
            {
                if (this.ValidateCustom(entity))
                {
                    if (this.BeforeInsert(ref entity))
                    {
                        res = _dlBase.Insert(entity);
                    }
                }
            }
            return res;
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }


        public bool Validate(TEntity entity)
        {
            var mesArrayError = new List<string>();
            var isValidate = true;
            // Đọc các Property:
            var properties = entity.GetType().GetProperties();

            //Với mỗi thuộc tính, kiểm tra attribute để validate nó
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);

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
                    var entityDuplicate = GetByKey(property, entity);
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

        public bool ValidateCustom(TEntity entity)
        {
            return true;
        }
    }
}
    