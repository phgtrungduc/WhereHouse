using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLBase<TEntity,TDTO>
    {
        IEnumerable<TDTO> GetAll();
        TEntity GetByID(string Id);
        bool Delete(string id);
        bool Update(TEntity entity,string id );
        bool Insert(TEntity entity);
        IEnumerable<TEntity> GetByKey(string key, string value);
        TEntity GetOneByKey(string key, string value);
        TableName GetOneByKeyWithType<TableName>(PropertyInfo prop, TEntity entity) where TableName : class;
        ServiceResult GetByPaging(int page, int pageSize);
        bool DeleteById(string id);
    }
}
