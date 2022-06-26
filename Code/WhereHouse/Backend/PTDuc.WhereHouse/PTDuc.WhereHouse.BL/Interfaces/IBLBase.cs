using PTDuc.WhereHouse.EntityModels;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLBase<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(string Id);
        bool Delete(TEntity entity);
        bool Update(TEntity entity);
        bool Insert(TEntity entity);
        IEnumerable<TEntity> GetByKey(PropertyInfo prop, TEntity entity);
        TEntity GetOneByKey(PropertyInfo prop, TEntity entity);
        
        bool Validate(TEntity entity);
        bool ValidateCustom(TEntity entity);
        bool BeforeInsert(ref TEntity entity);
        bool BeforeUpdate(ref TEntity entity);
        ServiceResult GetByPaging(int page, int pageSize);
    }
}
