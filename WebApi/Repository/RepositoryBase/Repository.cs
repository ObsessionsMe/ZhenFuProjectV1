using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace RepositoryFactory.RepositorysBase
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public ZFDBContext dbcontext;
        public Repository(ZFDBContext mydbcontext)
        {
            dbcontext = mydbcontext;
        }

        public int Insert(TEntity entity)
        {
            dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            return dbcontext.SaveChanges();
        }
        public int Insert(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return dbcontext.SaveChanges();
        }
        public int Update(TEntity entity)
        {
            try
            {
                dbcontext.Set<TEntity>().Attach(entity);
                PropertyInfo[] props = entity.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    if (prop.GetValue(entity, null) != null)
                    {
                        if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        {
                            dbcontext.Entry(entity).Property(prop.Name).CurrentValue = null;
                            dbcontext.Entry(entity).Property(prop.Name).IsModified = true;
                        }
                    }
                }
                dbcontext.Update(entity);
                return dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(TEntity entity)
        {
            dbcontext.Set<TEntity>().Attach(entity);
            dbcontext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return dbcontext.SaveChanges();
        }
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitys = dbcontext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => dbcontext.Entry<TEntity>(m).State = EntityState.Deleted);
            return dbcontext.SaveChanges();
        }
        public TEntity FindEntity(object keyValue)
        {
            return dbcontext.Set<TEntity>().Find(keyValue);
        }
        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return dbcontext.Set<TEntity>().FirstOrDefault(predicate);
        }
        public IQueryable<TEntity> IQueryable()
        {
            return dbcontext.Set<TEntity>();
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return dbcontext.Set<TEntity>().Where(predicate);
        }
        public List<TEntity> FindList(Pagination pagination)
        {
            try
            {
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                MethodCallExpression resultExp = null;
                var tempData = dbcontext.Set<TEntity>().AsQueryable();
                foreach (string item in _order)
                {
                    string _orderPart = item;
                    _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                    string[] _orderArry = _orderPart.Split(' ');
                    string _orderField = _orderArry[0];
                    bool sort = isAsc;
                    if (_orderArry.Length == 2)
                    {
                        isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                    }
                    var parameter = Expression.Parameter(typeof(TEntity), "t");
                    var property = typeof(TEntity).GetProperty(_orderField);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                }
                tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
                pagination.records = tempData.Count();
                tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
                return tempData.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            try
            {
                bool isAsc = pagination.sord == null ? true : pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx != null ? pagination.sidx.Split(',') : null;
                MethodCallExpression resultExp = null;
                var tempData = dbcontext.Set<TEntity>().Where(predicate);
                foreach (string item in _order)
                {
                    string _orderPart = item;
                    _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                    string[] _orderArry = _orderPart.Split(' ');
                    string _orderField = _orderArry[0];
                    bool sort = isAsc;
                    if (_orderArry.Length == 2)
                    {
                        isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                    }
                    var parameter = Expression.Parameter(typeof(TEntity), "t");
                    var property = typeof(TEntity).GetProperty(_orderField);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                }
                tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
                if (tempData != null)
                {
                    pagination.records = tempData.Count();
                    tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
                    return tempData.ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                   throw ex;
            }
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var tempData = dbcontext.Set<TEntity>().Where(predicate);
                if (tempData != null)
                {
                    return tempData.ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行存储过程/sql返回受影响函数
        /// </summary>
        /// <param name="strSql">脚本</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public int ExecuteProc(string strSql, object[] dbParameter)
        {
            return dbcontext.Database.ExecuteSqlCommand(strSql, dbParameter);
        }
    }
}
