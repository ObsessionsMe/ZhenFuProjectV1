using Entity;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilitieEntity;

namespace BusinessLogic.ClientService
{
    public class UserService
    {
        /// <summary>
        /// 商城: 用户模块业务逻辑
        /// </summary>
        private IUserRepository userRepository;
        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public List<UserInfoEntity> FindUserList()
        {
            return userRepository.IQueryable(ExtLinq.True<UserInfoEntity>()).ToList();
        }

        public UserInfoEntity GetUserNameByPhone(string telephone)
        {
            return userRepository.FindEntity(x => x.UserTelephone == telephone && x.Enable == "Y");
        }

        public AjaxResult UserRegister(UserInfoEntity user)
        {
            try
            {
                //1校验用户
                if (user.UserTelephone == user.ReferrerTelephone)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你输入的手机号和不能推荐人手机号相同", data = "" };
                }
                var data = userRepository.FindEntity(x => x.UserTelephone == user.UserTelephone && x.Enable == "Y");
                if (data != null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你输入的手机号手机号在平台已经注册过了，请直接登录", data = "" };
                }
                data = userRepository.FindEntity(x => x.UserTelephone == user.UserTelephone && x.Enable == "N");
                if (data != null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你输入的手机号为无效账户，请输入其他手机号注册", data = "" };
                }
                //2校验推荐人
                data = userRepository.FindEntity(x => x.UserTelephone == user.ReferrerTelephone && x.Enable == "Y");
                if (data == null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你输入的推荐人手机号在系统中不存在，请重新输入推荐人手机号", data = "" };
                }
                data = userRepository.FindEntity(x => x.UserTelephone == user.ReferrerTelephone && x.Enable == "N");
                if (data != null)
                {
                    return new AjaxResult { state = ResultType.error.ToString(), message = "你输入的推荐人手机号为无效账户，请重新输入推荐人手机号", data = "" };
                }
                //3入库
                UserInfoEntity userEntity = new UserInfoEntity();
                userEntity.UserId = Common.CreateNo();
                userEntity.Name = user.Name;
                userEntity.UserTelephone = user.UserTelephone;
                userEntity.Referrer = GetUserNameByPhone(user.ReferrerTelephone).Referrer;
                userEntity.ReferrerTelephone = user.ReferrerTelephone;
                userEntity.IsAdmin = "N";
                userEntity.Password = DESEncrypt.Encrypt(user.Password);
                userEntity.UserType = 1;//新用户注册默认都是经销商
                userEntity.Enable = "Y";
                userEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
                userEntity.TourismPorints = 5000;
                userEntity.isHold = "N";
                userRepository.Insert(userEntity);
                return new AjaxResult { state = ResultType.success.ToString(), message = "注册成功！", data = "" };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserInfoEntity CheckLogin(string telephone,string password_r)
        {
            return userRepository.FindEntity(x => x.UserTelephone == telephone && x.Enable == "Y" && x.Password == password_r);
        }

        public UserInfoEntity EditPassWord(string telephone, string password)
        {
            UserInfoEntity user = userRepository.FindEntity(x => x.UserTelephone == telephone &&  x.Enable == "Y");
            if (user != null)
            {
                user.Password = DESEncrypt.Encrypt(user.Password);
                userRepository.Update(user);
            }
            return user;
        }

        /// <summary>
        /// 根据用户Id和手机号，获取该用户的积分信息：商品积分
        /// </summary>
        /// <returns></returns>
        public UserBaiseModel GetUserPorints(string userTelephone, string referrer, string referrerTelephone)
        {
            var model = new UserBaiseModel();
            //统计产品收益积分和团队收益积分
            return model;
        }

        /// <summary>
        /// 获取我的团队
        /// </summary>
        /// <param name="userTelephone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetMyTream(string userTelephone, string userId)
        {
            //通过递归实现树形数据，找出推荐人为我的直属下级和间接下级用户
            var model = new UserBaiseModel();

            return model;
        }   
    }
}
