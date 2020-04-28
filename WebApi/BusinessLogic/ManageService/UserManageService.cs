using Entity;
using Infrastructure;
using RepositoryFactory.ServiceInterface;
using System.Collections.Generic;

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
            var expression = ExtLinq.True<UserInfoEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
            }
            return userRepository.FindList(expression, pagination);
        }
    }
}
