using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //string executingAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //string mappingAssemblePath = Path.Combine(executingAssemblyDirectory, "NFine.Mapping.dll");

            //if (!File.Exists(mappingAssemblePath))
            //    throw new Exception($"{mappingAssemblePath}文件不存在");

            //Assembly asm = Assembly.LoadFile(mappingAssemblePath);

            //var typesToRegister = asm.GetTypes()
            //.Where(type => !String.IsNullOrEmpty(type.Namespace)) .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            //foreach (var type in typesToRegister)
            //{
            //    object configurationInstance = Activator.CreateInstance(type);

            //    modelBuilder.AddConfiguration(type, configurationInstance);
            //}
            ////modelBuilder.AddConfiguration(new UserMap());
            //base.OnModelCreating(modelBuilder);
        }

    }
 }
