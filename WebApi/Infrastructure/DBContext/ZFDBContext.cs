using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
 }
