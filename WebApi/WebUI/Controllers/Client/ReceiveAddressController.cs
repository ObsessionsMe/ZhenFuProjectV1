using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using NPOI.HPSF;
using Repository.RepositoryService;
using WebUI.App_Start;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  用户收货地址，添加/删除/修改
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveAddressController : BaseControllers
    {
        private readonly IReceiveAddressRepository receiveRepository;
        ReceiveAddressService servers = null;
        public ReceiveAddressController(IReceiveAddressRepository _receiveRepository)
        {
            receiveRepository = _receiveRepository;
            servers = new ReceiveAddressService(_receiveRepository);
        }

        /// <summary>
        ///  获取用户默认收货地址
        /// </summary>
        /// <returns></returns>
        [Route("GetDefalutAddress")]
        public ActionResult GetUserDefalutAddress()
        {
            var entity = servers.GetDefalutReveiveAddress(userModel.UserId);
            if (entity == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取数据失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = entity });
        }

        /// <summary>
        ///  获取用户所有收货地址
        /// </summary>
        /// <returns></returns>
        [Route("GetUserAllAddress")]
        public ActionResult GetUserAllAddress()
        {
            var receiveEntityList = servers.GetUserAllAddress(userModel.UserId);
            if (receiveEntityList.Count == 0)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取数据失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = receiveEntityList });
        }


        /// <summary>
        /// 用户添加/修改收货地址
        /// </summary>
        /// <returns></returns>
        [Route("SubmitAddress")]
        public ActionResult SubmitAddress(ReceiveAddressEntity receiveEntity)
        {
            if (receiveEntity == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取数据失败", data = null });
            }
            if (receiveEntity.Id == 0 || receiveEntity.AddressId == null)
            {
                receiveEntity.AddressId = "Ad" + Common.GuId();
                receiveRepository.Insert(receiveEntity);
            }
            else {
                receiveRepository.Update(receiveEntity);
            }        
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        [Route("GetAddressById")]
        public ActionResult GetAddressById(int Id)
        {
            var receiveEntity = receiveRepository.FindEntity(x => x.Id == Id);
            if (receiveEntity == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取数据失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = receiveEntity });
        }

        /// <summary>
        /// 用户删除收货地址
        /// </summary>
        /// <returns></returns>
        [Route("RemoveAddress")]
        public ActionResult RemoveAddress()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

    }
}
