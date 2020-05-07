using Entity;
using Infrastructure;
using Repository.RepositoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.ClientService
{

    public class ReceiveAddressService
    {
        /// <summary>
        ///  收货地址模块业务逻辑
        /// </summary>
        private IReceiveAddressRepository receiveAddressRepository;
        public ReceiveAddressService(IReceiveAddressRepository _receiveAddressRepository)
        {
            receiveAddressRepository = _receiveAddressRepository;
        }

        /// <summary>
        /// 获取默认收货地址,没有就返回第一个
        /// </summary>
        /// <returns></returns>
        public ReceiveAddressEntity GetDefalutReveiveAddress(string userId)
        {
            var entity = receiveAddressRepository.FindEntity(x => x.UserId == userId && x.isDefalut == "Y");
            if (entity != null)
            {
                return entity;
            }
            var list = receiveAddressRepository.FindList(x => x.UserId == userId);
            if (list.Count == 0)
            {
                return null;
            }
            return list.SingleOrDefault();
        }

        public List<ReceiveAddressEntity> GetUserAllAddress(string userId)
        {
            return receiveAddressRepository.FindList(x => x.UserId == userId);
        }
    }
}
