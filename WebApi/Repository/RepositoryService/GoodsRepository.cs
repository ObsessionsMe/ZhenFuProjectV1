using Entity;
using Infrastructure.DBContext;
using RepositoryFactory.RepositoryBase;
using RepositoryFactory.RepositorysBase;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryFactory.RepositoryService
{
    public class GoodsRepository : Repository<GoodsEntity>, IGoodsRepository
    {
        public GoodsRepository(ZFDBContext _dbcontext) : base(_dbcontext)
        {

        }

        public List<GoodsEntity> GetGoodsListByType(int type)
        {
            return (from g in base.dbcontext.GoodsEntity
                     join  a in
base.dbcontext.AttachMentInfoEntity on g.GoodsId equals a.MainId
into temp from tt in temp.DefaultIfEmpty()
                    where tt.AttachmentType == 4
& g.Enable == "Y" & g.isProduct == "N" & g.GoodsType == type

                    select new GoodsEntity
                    {
                        Addtime = g.Addtime,
                        DirectPoints = g.DirectPoints,
                        Enable = g.Enable,
                        Exterd1 = g.Exterd1,
                        Exterd2 = g.Exterd2,
                        Exterd3 = g.Exterd3,
                        GoodsDescribe = g.GoodsDescribe,
                        GoodsDetailsImg = g.GoodsDetailsImg,
                        GoodsId = g.GoodsId,
                        GoodsLevel = g.GoodsLevel,
                        GoodsName = g.GoodsName,
                        GoodsType = g.GoodsType,
                        Id = g.Id,
                        IndirectPoints = g.IndirectPoints,
                        isProduct = g.isProduct,
                        ItemPoints = g.ItemPoints,
                        StockCount = g.StockCount, 
                        UnitPrice = g.UnitPrice,
                        GoodsMainImg = tt==null?string.Empty:tt.AttachmentName,

                    }).ToList();
        }

    }
}