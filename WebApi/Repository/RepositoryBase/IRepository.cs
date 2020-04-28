﻿using Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryFactory.RepositorysBase
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Insert(TEntity entity);
        int Insert(List<TEntity> entitys);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        int Delete(Expression<Func<TEntity, bool>> predicate);
        TEntity FindEntity(object keyValue);
        TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> IQueryable();
        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList(Pagination pagination);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);
       int ExecuteProc(string strSql, object[] dbParameter);
    }
}