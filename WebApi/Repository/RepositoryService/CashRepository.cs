﻿using Entity;
using Infrastructure;
using Infrastructure.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewEntity;

namespace RepositoryFactory.RepositoryService
{
    public class CashRepository : Repository<CashInfoEntity>, ICashRepository
    {
        public CashRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

        public DataTable GetCashDetail(string userId, int type,string GoodsId)
        {
            var sqlParams = new SqlParams();
            sqlParams.Params.Add("@userId", userId);
            sqlParams.Params.Add("@type", type);
            sqlParams.Params.Add("@goodsId", GoodsId);
            return ExecuteSql.ProcQuery(new DatabaseFacade(dbcontext), "p_get_cash_detail", sqlParams).Tables[0];
        }

        public List<CashInfoEntity> GetCashList(string userId, int? type)
        {
            var repositorys = new Repository<CashInfoEntity>(dbcontext);
            var where = ExtLinq.True<CashInfoEntity>().And(a=>a.UserId==userId);
            if (type.HasValue)
            {
                where = where.And(a => a.Type == type.Value);
            }
            return repositorys.FindList(where);
        }

        public List<CashListEntity> GetUse_CashList(Pagination pagination, Expression<Func<CashListEntity, bool>> predicate)
        {
            var repositorys = new Repository<CashListEntity>(dbcontext);
            return repositorys.FindList(predicate, pagination);
        }


        /// <summary>
        /// 兑现审批
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cashIds"></param>
        /// <returns></returns>
        public int ApplyUserCash(int type, string cashIds)
        {
            string execSql = string.Format(" exec proc_apply_userCash {0},'{1}' ", type, cashIds);
           return dbcontext.Database.ExecuteSqlCommand(execSql, new object[] { });
        }
    }
}