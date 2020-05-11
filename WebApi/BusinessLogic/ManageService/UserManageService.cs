using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System.Collections.Generic;
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

        public List<UserPorintListEntity> GetUserList(Pagination pagination, string keyword)
        {
            return userRepository.GetUser_PorintList();
            //var expression = ExtLinq.True<UserInfoEntity>();
            // if (!string.IsNullOrEmpty(keyword))
            // {
            //     expression = expression.And(t => t.Name == keyword);
            // }
            //return userRepository.FindList(expression, pagination);
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
