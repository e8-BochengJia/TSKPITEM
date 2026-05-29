using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public interface IMemberFavorites
    {
        bool AddMemberFavorites(MemberFavoritesInfo entity);

        int DelMemberFavorites(int ID);

        MemberFavoritesInfo GetMemberFavoritesByID(int ID);

        MemberFavoritesInfo GetMemberFavoritesByProductID(int Member_ID, int type_id, int Product_ID);

        IList<MemberFavoritesInfo> GetMemberFavoritess(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
