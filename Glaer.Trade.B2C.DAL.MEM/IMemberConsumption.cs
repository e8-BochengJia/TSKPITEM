using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    /// <summary>
    /// 会员积分接口
    /// </summary>
    public interface IMemberConsumption
    {
        /// <summary>
        /// 添加会员积分信息
        /// </summary>
        /// <param name="entity">会员积分信息实体</param>
        /// <returns>true(成功)/false(失败)</returns>
        bool AddMemberConsumption(MemberConsumptionInfo entity);

        /// <summary>
        /// 根据编号删除会员积分信息
        /// </summary>
        /// <param name="ID">会员积分信息编号</param>
        /// <returns>大于0(成功)/等于0(失败)</returns>
        int DelMemberConsumption(int ID);

        /// <summary>
        ///  获取会员积分信息实体集合
        /// </summary>
        /// <param name="Query">获取会员积分信息实体集合查询条件</param>
        /// <returns>会员积分信息信息实体集合</returns>
        IList<MemberConsumptionInfo> GetMemberConsumptions(QueryInfo Query);

        /// <summary>
        /// 获取会员积分信息信息实体集合分页信息
        /// </summary>
        /// <param name="Query">获取会员积分信息实体集合分页信息查询条件</param>
        /// <returns>会员积分信息信息实体集合分页信息</returns>
        PageInfo GetPageInfo(QueryInfo Query);

        MemberConsumptionInfo GetMemberConsumptionByMemID(int memID, int Consump_Qid);
        
    }
}
