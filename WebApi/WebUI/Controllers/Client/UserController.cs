﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.ClientService;
using Entity;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryFactory.ServiceInterface;
using UtilitieEntity;
using WebUI.App_Start;
using WebUI.Tool;

namespace WebUI.Controllers.Client
{
    /// <summary>
    ///  用户模块:登录,注册等...
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseControllers
    {
        private readonly IUserRepository userRepository;
        private IUserPrintsSumRepository sumRepository;
        public UserController(IUserRepository _userRepository,IUserPrintsSumRepository _sumRepository)
        {
            userRepository = _userRepository;
            sumRepository = _sumRepository;
        }

        [Route("GetMyTream")]
        public ActionResult GetMyTream()
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            //返回用户层级结构(包含自己总共三层)
            UserService servers = new UserService(userRepository, sumRepository);
            var data = userRepository.FindEntity(x => x.UserId == userModel.UserId && x.UserTelephone == userModel.UserTelephone && x.Enable == "Y");
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "用户不存在", data = "" });
            }
            var result = servers.GetMyTream(userModel.UserTelephone);
            var results = new
            {
                parentName = data.Referrer + data.ReferrerTelephone,
                name = data.Name,
                treeData = result,
            };
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = results });
        }

        /// <summary>
        /// 获取用户积分(商品积分，团队积分)，进入个人首页时调用该方法
        /// </summary>
        /// <param name="userId"></param>
        [Route("GetUserPorints")]
        public ActionResult GetUserPorints(string userId)
        {
            if (userModel == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "Token校验失败", data = "" });
            }
            UserService servers = new UserService(userRepository, sumRepository);
            var data = servers.GetUserPorints(userId);
            if (data == null)
            {
                return Json(new AjaxResult { state = ResultType.error.ToString(), message = "找不到用户相关的积分数据", data = "" });
            }
            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "获取数据成功", data = data });
        }    
    }
}
