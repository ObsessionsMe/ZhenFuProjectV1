﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using BusinessLogic.ManageService;
using Entity;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.RepositoryService;
using Repository.ServiceInterface;
using RepositoryFactory.ServiceInterface;
using ViewEntity;
using WebUI.Tool;

namespace WebUI.Controllers.Manage
{
    /// <summary>
    /// 用户管理模块(用户列表，用户提现申请列表)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserManageController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IGoodsRepository goodsRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserPrintsSumRepository sumRepository;
        private readonly IUserPorintsRecordRepository recordRepository;
        private IUserBasePorintsRecordRepository basePorintRepository;
        private IUserProductFrameworkRepository framekRepository;
        public UserManageController(IUserRepository _userRepository, IGoodsRepository _goodsRepository, IOrderRepository _orderRepository,
            IUserPrintsSumRepository _sumRepository, IUserPorintsRecordRepository _recordRepository, IUserBasePorintsRecordRepository _basePorintRepository,
            IUserProductFrameworkRepository _framekRepository
            )
        {
            userRepository = _userRepository;
            goodsRepository = _goodsRepository;
            orderRepository = _orderRepository;
            sumRepository = _sumRepository;
            recordRepository = _recordRepository;
            basePorintRepository = _basePorintRepository;
            framekRepository = _framekRepository;
        }

        /// <summary>
        /// 确认已完成转账(针对--用户提现)
        /// </summary>
        /// <returns></returns>
        [Route("GetAllNoticeMessage")]
        public ActionResult ConfirmTransfer()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        /// 导出用户提现申请列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportUserCashListExcel()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        /// <summary>
        ///  获取用户消息通知(包括:1提现申请通知(数量)，2会员角色流转通知(数量))
        /// </summary>
        /// <returns></returns>
        [Route("GetAllNoticeMessage")]
        public ActionResult GetAllNoticeMessage()
        {
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = "" });
        }

        [Route("LoginOn")]
        public ActionResult LoginOn(string userId, string password)
        {
            UserManageService servers = new UserManageService(userRepository);
            var data = servers.GetUserByUserId(userId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "该手机号在系统中不存在", data = "" });
            }
            //密码解密
            string password_r = DESEncrypt.Decrypt(data.Password);
            if (userId != data.UserId || password != password_r)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "手机号或密码错误", data = "" });
            }
            UserInfoEntity datas = servers.CheckLogin(userId, data.Password);
            if (datas == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户名或密码错误", data = "" });
            }
            string key = $"adminUserInfoCache{Guid.NewGuid().ToString("N")}";
            new CacheHelper().SetCache(key, datas);
            var result = new
            {
                token = key,
                data = datas
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = result });
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Route("GetMemberList")]
        public ActionResult GetMemberList(PaginationParam param)
        {
            UserManageService service = new UserManageService(userRepository);
            var pagination = param.pagination;
            string keyword = param.keyword;
            var data = new
            {
                rows = service.GetUserList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Json(new AjaxResult { state = ResultType.success, message = "获取数据成功", data = data });
        }

        [Route("ManagePayPorints")]
        public ActionResult ManagePayPorints(int payNum, string UserId, int type)
        {
            if (payNum <= 0 || string.IsNullOrEmpty(UserId))
            {
                return null;
            }
            UserManageService service = new UserManageService(userRepository);
            var data = service.PayPorints(payNum, UserId, type);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "充值失败", data = data });
            }
            return Json(data);
        }

        [Route("DeleteUser")]
        public ActionResult DeleteUser(string UserId)
        {
            if (string.IsNullOrEmpty(UserId)) {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = null });
            }
            UserManageService server = new UserManageService(userRepository);
            var data = server.DeleteUser(UserId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "充值失败", data = data });
            }
            return Json(data);
        }
        [Route("EditUser")]
        //修改用户
        public ActionResult EditUser(string jsonString)
        {
            var users = JsonConvert.DeserializeObject<UserPorintListEntity>(jsonString);
            var entity = userRepository.FindEntity(x => x.UserId == users.UserId);
            if (entity == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "该用户在系统中不存在", data = null });
            }
            var entity_r = userRepository.FindEntity(x => x.UserTelephone == users.ReferrerTelephone && x.Enable == "Y");
            if (entity_r == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "你输入的推荐人手机号在系统中不存在", data = null });
            }
            //可以改姓名，推荐人手机号，福豆
            var user = userRepository.FindEntity(x => x.UserId == users.UserId);
            user.Name = users.Name;
            user.Referrer = entity_r.Name;
            user.ReferrerTelephone = entity_r.UserTelephone;
            user.PorintsSurplus = users.PorintsSurplus;
            user.TourismPorints = users.TourismPorints;
            user.TreamPorints = users.TreamPorints;
            user.PecialItemPorints = users.PecialItemPorints;
            user.FieldsPorints = users.FieldsPorints;
            int i = userRepository.Update(user);
            if (i < 1)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "修改用户失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "保存成功", data = "" });
        }
        [Route("GetMyTream")]
        public ActionResult GetMyTream(string userId, string goodsId)
        {
            //返回用户层级结构(包含自己总共三层)
            var data = userRepository.FindEntity(x => x.UserId == userId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "获取数据失败", data = null });
            }
            UserService servers = new UserService(userRepository, sumRepository, orderRepository);
            var result = servers.GetMyTreambyOrderAndUse(userId, goodsId);
            //DataTable treeTable = userRepository.GetUserTeamLevel(userId);
            //int payNum = Convert.ToInt32(treeTable.Compute("sum(BuyGoodsCount)", ""));
            var results = new
            {
                parentName = data.Referrer + data.ReferrerTelephone,
                name = data.Name,
                treeData = result
                //payNum = payNum
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = results });
        }
        [Route("GetUserPwd")]
        public ActionResult GetUserPwd(string phone)
        {
            //返回用户层级结构(包含自己总共三层)
            var data = userRepository.FindEntity(x => x.UserTelephone == phone);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "手机号不存在", data = null });
            }
           string pwd =  DESEncrypt.Decrypt(data.Password);
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = pwd });
        }
        [Route("EditUserPassd")]
        public ActionResult EditUserPassd(string userId, string newPassword)
        {
            var data = userRepository.FindEntity(x => x.UserId == userId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "手机号不存在", data = null });
            }
            data.Password = DESEncrypt.Encrypt(newPassword);
            int i = userRepository.Update(data);
            if (i < 1)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "修改数据失败", data = null });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = null });
        }        
    }
}
