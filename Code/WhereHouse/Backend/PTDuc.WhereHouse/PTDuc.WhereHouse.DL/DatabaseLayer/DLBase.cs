using Microsoft.Extensions.Configuration;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PTDuc.WhereHouse.DBContext.Models;
using System.Dynamic;
using System.Linq;
using PTDuc.WhereHouse.Utils;
using Microsoft.Scripting.Runtime;
using PTDuc.WhereHouse.EntityModels.Attribute;
using PTDuc.WhereHouse.EntityModels;
using System.Reflection;
using System.Data.Entity.Infrastructure;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLBase<TEntity> : IDLBase<TEntity> where TEntity : class
    {
        protected WhereHouseContext _context;
        //String _connectionString =null;
        //SqlConnection _dbConnection =null;
        protected String tableName = "";

        protected DbSet<TEntity> _dbSet;

        public DLBase(WhereHouseContext context)
        {
            _context = context;
            tableName = typeof(TEntity).Name;

        }

        public bool Delete(TEntity entity)
        {
            var res = 0;
            var idKey = $"{ tableName}Id";
            var Id = entity.GetValueByKey<TEntity>(idKey);
            _dbSet = _context.Set<TEntity>();
            var record = _dbSet.Find(Guid.Parse(Id.ToString()));
            if (record != null)
            {
                _dbSet.Remove(record);
                res = _context.SaveChanges();
            }
            return (res > 0 ? true : false);
        }

        public IEnumerable<TEntity> GetAll()
        {
            _dbSet = _context.Set<TEntity>();
            return _dbSet;
        }

        public TEntity GetByID(string Id)
        {
            _dbSet = _context.Set<TEntity>();
            return _dbSet.Find(Guid.Parse(Id));
        }

        public bool Insert(TEntity entity)
        {
            _dbSet = _context.Set<TEntity>();
            _dbSet.Add(entity);
            var res = _context.SaveChanges();
            return (res > 0 ? true : false);
        }

        public bool Update(TEntity entity)
        {
            var res = 0;
            var idKey = $"{ tableName}Id";
            var Id = entity.GetValueByKey<TEntity>(idKey);
            _dbSet = _context.Set<TEntity>();
            var record = _dbSet.Find(Guid.Parse(Id.ToString()));
            if (record != null)
            {
                _dbSet.Attach(record);
                record = entity;
                res = _context.SaveChanges();
            }
            return (res > 0 ? true : false);
        }

        public IEnumerable<TEntity> GetByKey(PropertyInfo prop, TEntity entity)
        {

            IEnumerable<TEntity> res;
            var key = prop.Name;
            var value = prop.GetValue(entity);
            _dbSet = _context.Set<TEntity>();
            //var type = typeof(TEntity).GetProperty(key).PropertyType;
            //var valueFind = Convert.ChangeType(value, type);
            res = _dbSet.FromSqlRaw($"select * from {tableName} where {key} = '{value}'");

            return res;

        }
        public TEntity GetOneByKey(PropertyInfo prop, TEntity entity)
        {

            TEntity res = null;
            var key = prop.Name;
            var value = prop.GetValue(entity);
            _dbSet = _context.Set<TEntity>();
            //var type = typeof(TEntity).GetProperty(key).PropertyType;
            //var valueFind = Convert.ChangeType(value, type);
            res = _dbSet.FromSqlRaw($"select * from {tableName} where {key} = '{value}'").FirstOrDefault();
            return res;

        }
        public TableName GetOneByKeyWithType<TableName>(PropertyInfo prop, TEntity entity) where TableName : class
        {
            TableName res = null;
            var key = prop.Name;
            var value = prop.GetValue(entity);
            var table = typeof(TableName).Name;
            var dbSet = _context.Set<TableName>();
            //var type = typeof(TEntity).GetProperty(key).PropertyType;
            //var valueFind = Convert.ChangeType(value, type);
            res = dbSet.FromSqlRaw($"select * from [{table}] where {key} = '{value}'").FirstOrDefault();
            return res;

        }

        public ServiceResult GetByPaging(int page, int pageSize)
        {
            _dbSet = _context.Set<TEntity>();
            var totalRecords = _dbSet.Count();
            var skip = (page - 1) * pageSize;
            var res = new ServiceResult();
            if (skip >= 0 && pageSize > 0)
            {
                var data = _dbSet.Skip(skip).Take(pageSize);
                if (typeof(TEntity) == typeof(House))
                {
                }
                res.Data = new { TotalRecords = totalRecords, Data = data };
            }
            return res;
        }
    }
}
