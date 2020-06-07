using Entity;
using Infrastructure.DBContext;
using Jayrock.Json.Conversion.Converters;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class GoodsRepository : Repository<GoodsEntity>, IGoodsRepository
    {
        public GoodsRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

        public List<dynamic> GetGoodsListByType(string type)
        {
            var sql = new StringBuilder(@"select
g.Id,g.GoodsId,g.GoodsName,g.isProduct,g.GoodsType,g.GoodsLevel,g.UnitPrice,g.GoodsDescribe,zai.AttachmentName GoodsMainImg,g.GoodsDetailsImg,g.ItemPoints,g.DirectPoints,g.IndirectPoints,g.StockCount,g.GoodsFreight,g.Enable,g.Addtime,g.Exterd1,g.Exterd2,g.Exterd3
From  zf_goods_info g
                        join 
  zf_attachment_info zai on g.GoodsId =zai.MainId
                        where zai.AttachmentType = 4
    and g.Enable = 'Y' and g.isProduct = 'N'");

            if (!string.IsNullOrEmpty(type))
            {
                sql.Append($" and g.GoodsType={type} ");
            }

            return ExecuteSql.SqlQuery(new DatabaseFacade(dbcontext), sql.ToString(), new object[] { }).AsEnumerable().Select(s=>new {
                GoodsId=s.Field<string>("GoodsId"),
                GoodsMainImg = s.Field<string>("GoodsMainImg"),
                GoodsName = s.Field<string>("GoodsName"),
                UnitPrice = s.Field<int>("UnitPrice"),
                GoodsType= s.Field<string>("GoodsType")
                
            } as dynamic).ToList();


            //         if (string.IsNullOrEmpty(type))
            //         {
            //             return (from g in base.dbcontext.GoodsEntity
            //                     join a in
            //base.dbcontext.AttachMentInfoEntity on g.GoodsId equals a.MainId
            //into temp
            //                     from tt in temp.DefaultIfEmpty()
            //                     where tt.AttachmentType == 4
            // & g.Enable == "Y" & g.isProduct == "N"

            //                     select new GoodsEntity
            //                     {
            //                         Addtime = g.Addtime,
            //                         DirectPoints = g.DirectPoints,
            //                         Enable = g.Enable,
            //                         Exterd1 = g.Exterd1,
            //                         Exterd2 = g.Exterd2,
            //                         Exterd3 = g.Exterd3,
            //                         GoodsDescribe = g.GoodsDescribe,
            //                         GoodsDetailsImg = g.GoodsDetailsImg,
            //                         GoodsId = g.GoodsId,
            //                         GoodsLevel = g.GoodsLevel,
            //                         GoodsName = g.GoodsName,
            //                         GoodsType = g.GoodsType,
            //                         Id = g.Id,
            //                         IndirectPoints = g.IndirectPoints,
            //                         isProduct = g.isProduct,
            //                         ItemPoints = g.ItemPoints,
            //                         StockCount = g.StockCount,
            //                         UnitPrice = g.UnitPrice,
            //                         GoodsMainImg = tt == null ? string.Empty : tt.AttachmentName,

            //                     }).ToList();
            //         }
            //         else
            //         {
            //             return (from g in base.dbcontext.GoodsEntity
            //                     join a in
            //base.dbcontext.AttachMentInfoEntity on g.GoodsId equals a.MainId
            //into temp
            //                     from tt in temp.DefaultIfEmpty()
            //                     where tt.AttachmentType == 4
            // & g.Enable == "Y" & g.isProduct == "N" & g.GoodsType == type

            //                     select new GoodsEntity
            //                     {
            //                         Addtime = g.Addtime,
            //                         DirectPoints = g.DirectPoints,
            //                         Enable = g.Enable,
            //                         Exterd1 = g.Exterd1,
            //                         Exterd2 = g.Exterd2,
            //                         Exterd3 = g.Exterd3,
            //                         GoodsDescribe = g.GoodsDescribe,
            //                         GoodsDetailsImg = g.GoodsDetailsImg,
            //                         GoodsId = g.GoodsId,
            //                         GoodsLevel = g.GoodsLevel,
            //                         GoodsName = g.GoodsName,
            //                         GoodsType = g.GoodsType,
            //                         Id = g.Id,
            //                         IndirectPoints = g.IndirectPoints,
            //                         isProduct = g.isProduct,
            //                         ItemPoints = g.ItemPoints,
            //                         StockCount = g.StockCount,
            //                         UnitPrice = g.UnitPrice,
            //                         GoodsMainImg = tt == null ? string.Empty : tt.AttachmentName,

            //                     }).ToList();
            //         }
        }
    }
}