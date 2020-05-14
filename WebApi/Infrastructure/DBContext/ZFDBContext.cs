﻿using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ViewEntity;

namespace Infrastructure.DBContext
{
    public class ZFDBContext : DbContext
    {
        public ZFDBContext(DbContextOptions<ZFDBContext> options)
        : base(options)
        { }
        public DbSet<UserInfoEntity> UserEntity { get; set; }
        public DbSet<OrderInfoEntity> OrderInfoEntity { get; set; }
        public DbSet<GoodsEntity> GoodsEntity { get; set; }
        public DbSet<ReceiveAddressEntity> ReceiveAddressEntity { get; set; }
        public DbSet<UserPorintsRecordEntity> UserPorintsRecordEntity { get; set; }
        public DbSet<UserPrintsSumEntity> UserPrintsSumEntity { get; set; }

        public DbSet<CashInfoEntity> CashInfoEntity { get; set; }

        public DbSet<AttachMentInfoEntity> AttachMentInfoEntity { get; set; }

        public DbSet<UserPorintListEntity> UserPorintListEntity { get; set; }

        public DbSet<WorkDateEntity> WorkDateEntity { get; set; }
        
    }
 }
