using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.MEM
{
    /// <summary>
    /// 会员等级接口
    /// </summary>
    public interface IMemberGrade
    {
        /// <summary>
        /// 添加会员等级信息
        /// </summary>
        /// <param name="entity">会员等级信息实体</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool AddMemberGrade(MemberGradeInfo entity);

        /// <summary>
        /// 修改会员等级信息
        /// </summary>
        /// <param name="entity">会员等级信息实体</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool EditMemberGrade(MemberGradeInfo entity);

        /// <summary>
        /// 根据编号删除会员等级信息
        /// </summary>
        /// <param name="ID">会员等级信息编号</param>
        /// <returns>大于0(成功)/等于0(失败)</returns>
        int DelMemberGrade(int ID);

        /// 根据编号获取会员等级信息实体
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns>会员等级信息实体</returns>
        MemberGradeInfo GetMemberGradeByID(int ID);

        /// <summary>
        ///  获取会员等级信息实体集合
        /// </summary>
        /// <param name="Query">获取会员等级信息实体集合查询条件</param>
        /// <returns>会员等级信息信息实体集合</returns>
        IList<MemberGradeInfo> GetMemberGrades(QueryInfo Query);

        /// <summary>
        /// 获取会员等级信息信息实体集合分页信息
        /// </summary>
        /// <param name="Query">获取会员等级信息实体集合分页信息查询条件</param>
        /// <returns>会员等级信息信息实体集合分页信息</returns>
        PageInfo GetPageInfo(QueryInfo Query);

        /// <summary>
        /// 获取会员默认等级
        /// </summary>
        /// <returns>会员默认等级信息实体</returns>
        MemberGradeInfo GetMemberDefaultGrade();
    }

}
