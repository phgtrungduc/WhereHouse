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
using AutoMapper;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLBase<TEntity, TDTO> : IDLBase<TEntity, TDTO> where TEntity : class
    {
        protected WhereHouseContext _context;
        //String _connectionString =null;
        //SqlConnection _dbConnection =null;
        protected String tableName = "";

        protected DbSet<TEntity> _dbSet;

        protected readonly IMapper _mapper;
        public DLBase(WhereHouseContext context, IMapper mapper)
        {
            _context = context;
            tableName = typeof(TEntity).Name;
            _mapper = mapper;
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

        public IEnumerable<TDTO> GetAll()
        {
            _dbSet = _context.Set<TEntity>();
            var obj = (TDTO)typeof(TDTO).GetConstructor(new Type[0]).Invoke(new object[0]);
            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<TEntity, TDTO>();
            //});
            //var mapper = configuration.CreateMapper();
            var res = _mapper.Map<List<TEntity>, IEnumerable<TDTO>>(_dbSet.ToList());
            return res;
        }

        public virtual TEntity GetByID(string Id)
        {
            _dbSet = _context.Set<TEntity>();
            var res = _dbSet.Find(Guid.Parse(Id));
            return res;
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
            //var Id = entity.GetValueByKey<TEntity>(idKey);
            _dbSet = _context.Set<TEntity>();
            //var record = _dbSet.Find(Guid.Parse(Id.ToString()));
            //if (record != null)
            //{
            //    record = entity;
            //    _context.Entry(entity).State = EntityState.Modified;
            //    res = _context.SaveChanges();
            //}
            _context.Entry(entity).State = EntityState.Modified;
            res = _context.SaveChanges();
            return (res > 0 ? true : false);
        }

        public IEnumerable<TEntity> GetByKey(string key, string value)
        {

            IEnumerable<TEntity> res;
            _dbSet = _context.Set<TEntity>();
            //var type = typeof(TEntity).GetProperty(key).PropertyType;
            //var valueFind = Convert.ChangeType(value, type);
            res = _dbSet.FromSqlRaw($"select * from {tableName} where {key} = '{value}'");

            return res;

        }
        public TEntity GetOneByKey(string key, string value)
        {

            TEntity res = null;
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

        public virtual ServiceResult GetByPaging(int page, int pageSize)
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
                res.Data = new { TotalRecords = totalRecords, Data = data.ToList() };
            }
            return res;
        }

        public bool DeleteById(string id)
        {
            _dbSet = _context.Set<TEntity>();
            var res = 0;
            var entity = _dbSet.Find(Guid.Parse(id));
            if (entity != null)
            {
                _dbSet.Remove(entity);
                res = _context.SaveChanges();
            }
            return (res > 0 ? true : false);
        }
    }
}
