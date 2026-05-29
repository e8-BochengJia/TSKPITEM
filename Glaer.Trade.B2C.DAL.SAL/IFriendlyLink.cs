using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.SAL
{
    public interface IFriendlyLinkCate
    {
        bool AddFriendlyLinkCate(FriendlyLinkCateInfo entity);

        bool EditFriendlyLinkCate(FriendlyLinkCateInfo entity);

        int DelFriendlyLinkCate(int ID);

        FriendlyLinkCateInfo GetFriendlyLinkCateByID(int ID);

        IList<FriendlyLinkCateInfo> GetFriendlyLinkCates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface IFriendlyLink
    {
        bool AddFriendlyLink(FriendlyLinkInfo entity);

        bool EditFriendlyLink(FriendlyLinkInfo entity);

        int DelFriendlyLink(int ID);

        FriendlyLinkInfo GetFriendlyLinkByID(int ID);

        IList<FriendlyLinkInfo> GetFriendlyLinks(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }


}
