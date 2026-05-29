using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public interface INoticeCate
    {
        bool AddNoticeCate(NoticeCateInfo entity, RBACUserInfo UserPrivilege);

        bool EditNoticeCate(NoticeCateInfo entity, RBACUserInfo UserPrivilege);

        int DelNoticeCate(int ID, RBACUserInfo UserPrivilege);

        NoticeCateInfo GetNoticeCateByID(int ID, RBACUserInfo UserPrivilege);

        IList<NoticeCateInfo> GetNoticeCates(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

    public interface INotice
    {
        bool AddNotice(NoticeInfo entity, RBACUserInfo UserPrivilege);

        bool EditNotice(NoticeInfo entity, RBACUserInfo UserPrivilege);

        int DelNotice(int ID, RBACUserInfo UserPrivilege);

        NoticeInfo GetNoticeByID(int ID, RBACUserInfo UserPrivilege);

        IList<NoticeInfo> GetNotices(QueryInfo Query, RBACUserInfo UserPrivilege);

        IList<NoticeInfo> GetNoticeList(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }
}
