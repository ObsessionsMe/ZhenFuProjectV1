﻿using Entity;
using Infrastructure.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RepositoryFactory.RepositoryService
{
    public class CashRepository : Repository<CashInfoEntity>, ICashRepository
    {
        public CashRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

        public DataTable GetCashDetail(string userId, int type)
        {
            var sqlParams = new SqlParams();
            sqlParams.Params.Add("@userId", userId);
            sqlParams.Params.Add("@type", type);

            return ExecuteSql.ProcQuery(new DatabaseFacade(dbcontext), "p_get_cash_detail", sqlParams).Tables[0];
        }
    }
}