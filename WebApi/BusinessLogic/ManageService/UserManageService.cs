using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ViewEntity;

namespace BusinessLogic.ManageService
{
    /// <summary>
    /// 商城管理后台: 用户模块业务逻辑
    /// </summary>
    public class UserManageService
    {
        private IUserRepository userRepository;
        public UserManageService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="password_r"></param>
        /// <returns></returns>
        public UserInfoEntity CheckLogin(string userId, string password)
        {
            return userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y" && x.Password == password && x.IsAdmin == "Y");
        }
        public UserInfoEntity GetUserByUserId(string userId)
        {
            return userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y" && x.IsAdmin == "Y");
        }

        public List<UserInfoEntity> GetUserList(Pagination pagination, string keyword)
        {
            //var expression = ExtLinq.True<UserInfoEntity>();
            //expression = expression.And(t => t.Enable == "Y");
            //expression = expression.And(t => t.IsAdmin == "N");
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    expression = expression.And(t => t.Name.Contains(keyword));
            //    expression = expression.Or(t => t.UserTelephone.Contains(keyword));
            //}
            Expression<Func<UserInfoEntity, bool>> expression = b => (b.Enable == "Y" && b.IsAdmin == "N" && string.IsNullOrEmpty(b.Name)) || (b.Enable == "Y" && b.IsAdmin == "N" &&
            !string.IsNullOrEmpty(b.Name) && b.Name.Contains(keyword));
            return userRepository.FindList(expression, pagination).ToList();
        }

        public AjaxResult PayPorints(int payNum, string userId, int type)
        {
            var user = new UserInfoEntity();
            user = userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y");
            if (user == null)
            {
                return null;
            }
            if (type == 1)
            {
                user.PorintsSurplus = (user.PorintsSurplus) + (payNum);
            }
            else if (type == 2) {
                user.PecialItemPorints = (user.PecialItemPorints) + (payNum);
            }
            int i = userRepository.Update(user);
            if (i < 1)
            {
                return null;
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "充值成功！", data = "" };
        }

        public AjaxResult PayPorints(decimal payNum, string userId)
        {
            var user = new UserInfoEntity();
            user = userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y");
            if (user == null)
            {
                return null;
            }
            user.PorintsSurplus = Convert.ToInt32(user.PorintsSurplus + payNum);
            int i = userRepository.Update(user);
            if (i < 1)
            {
                return null;
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "充值成功！", data = "" };
        }

        public AjaxResult PayPorints_shop(decimal payNum, string userId)
        {
            var user = new UserInfoEntity();
            user = userRepository.FindEntity(x => x.UserId == userId && x.Enable == "Y");
            if (user == null)
            {
                return null;
            }
            user.PorintsSurplus = Convert.ToInt32(user.PorintsSurplus + payNum);
            int i = userRepository.Update(user);
            if (i < 1)
            {
                return null;
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "充值成功！", data = "" };
        }

        public AjaxResult DeleteUser(string userId)
        {
            //删除用户表，汇总表，记录表，订单表
            string procStr = string.Format(" p_delete_user '{0}' ", userId);
            int i =  userRepository.ExecuteProc(procStr, new object[] { });
            if (i <= 0) {
                return new AjaxResult { state = ResultType.error.ToString(), message = "删除失败！", data = "" };
            }
            return new AjaxResult { state = ResultType.success.ToString(), message = "删除成功！", data = "" };
        }
    }
}
