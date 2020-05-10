using Entity;
using Entity.Params;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UtilitieEntity;

namespace BusinessLogic.ClientService
{
    public class UserService
    {
        /// <summary>
        /// 商城: 用户/用户登录等模块业务逻辑
        /// </summary>
        private IUserRepository userRepository;
        private IUserPrintsSumRepository sumRepository;
        private IOrderRepository order;
        public UserService(IUserRepository _userRepository, IUserPrintsSumRepository _sumRepository, IOrderRepository _order)
        {
            userRepository = _userRepository;
            sumRepository = _sumRepository;
            order = _order;
        }


        public DataSet GetProductEarn(GoodsParam goodsParam)
        {
            return sumRepository.GetProductEarn(goodsParam);
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
                userEntity.Referrer = GetUserNameByPhone(user.ReferrerTelephone).Name;
                userEntity.ReferrerTelephone = user.ReferrerTelephone;
                userEntity.IsAdmin = "N";
                userEntity.Password = DESEncrypt.Encrypt(user.Password);
                userEntity.UserType = 1;//新用户注册默认都是经销商
                userEntity.Enable = "Y";
                userEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                userEntity.TourismPorints = 1000;
                userEntity.isHold = "N";
                userRepository.Insert(userEntity);
                //用户汇总初始化数据
                var sumporintsEntity = new UserPrintsSumEntity();
                sumporintsEntity.UserId = userEntity.UserId;
                sumporintsEntity.ProductPorints = 0;
                sumporintsEntity.TreamPorints = 0;
                sumporintsEntity.PorintsSurplus = 0;
                sumporintsEntity.Addtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                sumRepository.Insert(sumporintsEntity);
                return new AjaxResult { state = ResultType.success.ToString(), message = "注册成功！", data = userEntity };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserInfoEntity CheckLogin(string telephone, string password_r)
        {
            return userRepository.FindEntity(x => x.UserTelephone == telephone && x.Enable == "Y" && x.Password == password_r);
        }

        public UserInfoEntity EditPassWord(string telephone, string password)
        {
            UserInfoEntity user = userRepository.FindEntity(x => x.UserTelephone == telephone && x.Enable == "Y");
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

        /// <summary>
        /// 获取我的团队
        /// </summary>
        /// <param name="userTelephone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetMyTream(string userId,string goodsId)
        {
            try
            {
                List<UserTreeData> results = new List<UserTreeData>();
                DataTable treeTable1 = userRepository.GetUserTeamLevel1(userId, goodsId);
                DataTable treeTable2 = userRepository.GetUserTeamLevel2(userId, goodsId);
                for (int i = 0; i < treeTable1.Rows.Count; i++)
                {
                    var obj = treeTable1.Rows[i];
                    int level = Convert.ToInt32(obj["Level"]);
                    //一级用户
                    var treeData = new UserTreeData();
                    treeData.id = obj["UserId"].ToString();
                    int count = Convert.ToInt32(obj["BuyGoodsCount"]);
                    string tel = obj["UserTelephone"].ToString();
                    string tel_new = tel.Substring(0, 3) + "****" + tel.Substring(7);
                    treeData.label = "vip1 " + obj["Name"].ToString() + "(" + tel_new + ")" + "(" + count + ")";
                    for (int j = 0; j < treeTable2.Rows.Count; j++)
                    {
                        //二级用户
                        var dr = treeTable2.Rows[j];
                        if (dr["ReferrerTelephone"].ToString() == tel)
                        {
                            var chirds = new List<UserTreeData>();
                            count = Convert.ToInt32(dr["BuyGoodsCount"]);
                            tel = dr["UserTelephone"].ToString();
                            tel_new = tel.Substring(0, 3) + "****" + tel.Substring(7);
                            chirds.Add(new UserTreeData { id = dr["UserId"].ToString(), label = "vip2 " + dr["Name"].ToString() + "(" + tel_new + ")" + "(" + count + ")" });
                            treeData.children = chirds;
                            results.Add(treeData);
                        }
                    }
                    if (!results.Contains(treeData))
                    {
                        results.Add(treeData);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetUserPayCout(string userId)
        {
            //查询订单表，获取用户购买盒数
            List<OrderInfoEntity> list = order.FindList(x => x.UserId == userId);
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                count += list[i].BuyGoodsNums;
            }
            return count;
        }

            public UserPrintsSumEntity GetUserPorints(string userId)
        {
            return sumRepository.FindEntity(x => x.UserId == userId);
        }
        
    }
}
