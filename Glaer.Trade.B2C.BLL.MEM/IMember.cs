using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.MEM
{
    /// <summary>
    /// 会员接口
    /// </summary>
    public interface IMember
    {
        /// <summary>
        /// 添加会员信息
        /// </summary>
        /// <param name="entity">会员信息实体</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool AddMember(MemberInfo entity, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <param name="entity">会员信息实体</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool EditMember(MemberInfo entity, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 更新会员登录信息
        /// </summary>
        /// <param name="Member_ID">会员编号</param>
        /// <param name="Count">登录次数</param>
        /// <param name="Remote_IP">登录ID</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool UpdateMemberLogin(int Member_ID, int Count, string Remote_IP, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 删除会员信息
        /// </summary>
        /// <param name="ID">会员编号</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>大于0(成功)/等于0(失败)</returns>
        int DelMember(int ID, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 根据会员编号获取会员信息实体
        /// </summary>
        /// <param name="ID">会员编号</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>会员信息实体</returns>
        MemberInfo GetMemberByID(int ID, RBACUserInfo UserPrivilege);

        MemberInfo GetMemberByOpenID(string openid, RBACUserInfo UserPrivilege);
        /// <summary>
        /// 根据Email获取会员信息实体
        /// </summary>
        /// <param name="email">会员Email</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>会员信息实体</returns>
        MemberInfo GetMemberByEmail(string email, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 根据会员昵称获取会员信息实体
        /// </summary>
        /// <param name="NickName">会员昵称</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>会员信息实体</returns>
        MemberInfo GetMemberByNickName(string NickName, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 根据会员昵称、邮箱和手机号获取会员实体信息
        /// </summary>
        /// <param name="member_name">登陆名</param>
        /// <returns>会员信息实体</returns>
        MemberInfo Member_Login(string member_name, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 登录时获取会员信息实体
        /// </summary>
        /// <param name="nickname">会员昵称</param>
        /// <param name="password">会员密码</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>会员信息实体</returns>
        MemberInfo GetMemberByLogin(string nickname, string password, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 获取会员信息实体集合
        /// </summary>
        /// <param name="Query">获取会员信息实体集合查询条件</param>
        /// <param name="UserPrivilege">权限</param>
        /// <returns>会员信息实体集合</returns>
        IList<MemberInfo> GetMembers(QueryInfo Query, RBACUserInfo UserPrivilege);

        /// <summary>
        /// 获取会员信息实体集合分页信息
        /// </summary>
        /// <param name="Query">获取会员信息实体集合分页信息查询条件</param>
        /// <param name="UserPrivilege"></param>
        /// <returns>会员信息实体集合分页信息</returns>
        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

      
    }

    public interface IMemberLog
    {
        /// <summary>
        /// 添加会员日志
        /// </summary>
        /// <param name="entity">会员日志信息实体</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool AddMemberLog(MemberLogInfo entity);

        /// <summary>
        /// 根据编号删除会员日志
        /// </summary>
        /// <param name="ID">日志编号</param>
        /// <returns>大于0(成功)/等于0(失败)</returns>
        int DelMemberLog(int ID);

        /// <summary>
        /// 获取会员日志实体集合
        /// </summary>
        /// <param name="Query">获取会员日志实体集合查询条件</param>
        /// <returns>会员日志实体集合</returns>
        IList<MemberLogInfo> GetMemberLogs(QueryInfo Query);
    }
}
