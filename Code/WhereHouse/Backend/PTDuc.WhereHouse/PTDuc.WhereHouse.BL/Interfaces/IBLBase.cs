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
        bool Delete(TEntity entity);
        bool Update(TDTO entity);
        bool Insert(TEntity entity);
        IEnumerable<TEntity> GetByKey(string key, string value);
        TEntity GetOneByKey(string key, string value);
        
        bool Validate(TEntity entity);
        bool ValidateCustom(TEntity entity);
        bool BeforeInsert(ref TEntity entity);
        bool BeforeUpdate(ref TEntity entity);
        ServiceResult GetByPaging(int page, int pageSize);
    }
}
