using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLBase<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(string Id);
        bool Delete(TEntity entity);
        bool Update(TEntity entity);
        bool Insert(TEntity entity);
        IEnumerable<TEntity> GetByKey(PropertyInfo prop, TEntity entity);
        TEntity GetOneByKey(PropertyInfo prop, TEntity entity);
        TableName GetOneByKeyWithType<TableName>(PropertyInfo prop, TEntity entity) where TableName : class;
        ServiceResult GetByPaging(int page, int pageSize);
    }
}
