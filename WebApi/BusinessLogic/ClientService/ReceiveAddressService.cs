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
        ///  商品模块业务逻辑
        /// </summary>
        private IReceiveAddressRepository receiveAddressRepository;
        public ReceiveAddressService(IReceiveAddressRepository _receiveAddressRepository)
        {
            receiveAddressRepository = _receiveAddressRepository;
        }

        /// <summary>
        /// 获取所有有效商品
        /// </summary>
        /// <returns></returns>
        public ReceiveAddressEntity GetUserReveiveAddressList(string userId)
        {
            return receiveAddressRepository.FindEntity(x => x.UserId == userId && x.isDefalut == "Y");
        }
    }
}
