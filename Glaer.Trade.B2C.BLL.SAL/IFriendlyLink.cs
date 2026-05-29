using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.SAL
{
    public interface IFriendlyLinkCate
    {
        bool AddFriendlyLinkCate(FriendlyLinkCateInfo entity, RBACUserInfo UserPrivilege);

        bool EditFriendlyLinkCate(FriendlyLinkCateInfo entity, RBACUserInfo UserPrivilege);

        int DelFriendlyLinkCate(int ID, RBACUserInfo UserPrivilege);

        FriendlyLinkCateInfo GetFriendlyLinkCateByID(int ID, RBACUserInfo UserPrivilege);

        IList<FriendlyLinkCateInfo> GetFriendlyLinkCates(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

    public interface IFriendlyLink
    {
        bool AddFriendlyLink(FriendlyLinkInfo entity, RBACUserInfo UserPrivilege);

        bool EditFriendlyLink(FriendlyLinkInfo entity, RBACUserInfo UserPrivilege);

        int DelFriendlyLink(int ID, RBACUserInfo UserPrivilege);

        FriendlyLinkInfo GetFriendlyLinkByID(int ID, RBACUserInfo UserPrivilege);

        IList<FriendlyLinkInfo> GetFriendlyLinks(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

}
