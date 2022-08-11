using PTDuc.WhereHouse.EntityModels;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLBase<TEntity,TDTO>
    {
        IEnumerable<TDTO> GetAll();
        TDTO GetByID(string Id);
        bool Delete(string id);
        bool Update(TDTO entity,string id);
        bool Insert(TDTO entity);
        IEnumerable<TEntity> GetByKey(string key, string value);
        TEntity GetOneByKey(string key, string value);
        
        bool Validate(TDTO entity);
        bool ValidateCustom(TDTO entity);
        bool BeforeInsert(ref TDTO entity);
        bool BeforeUpdate(ref TEntity entity);
        ServiceResult GetByPaging(int page, int pageSize);
    }
}
