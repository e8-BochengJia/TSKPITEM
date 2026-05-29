using System;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class NoticeCate : INoticeCate
    {
        protected DAL.CMS.INoticeCate MyDAL;
        protected IRBAC RBAC;

        public NoticeCate()
        {
            MyDAL = DAL.CMS.NoticeFactory.CreateNoticeCate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddNoticeCate(NoticeCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8c732f78-5431-41c6-a3db-68787f36c223"))
            {
                return MyDAL.AddNoticeCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8c732f78-5431-41c6-a3db-68787f36c223错误");
            }
        }

        public virtual bool EditNoticeCate(NoticeCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "705ff0e0-daa6-4649-bf27-20142c21ba9e"))
            {
                return MyDAL.EditNoticeCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：705ff0e0-daa6-4649-bf27-20142c21ba9e错误");
            }
        }

        public virtual int DelNoticeCate(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e2e67cd1-dd5c-4c63-962a-fdbd0d7dc6a8"))
            {
                return MyDAL.DelNoticeCate(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e2e67cd1-dd5c-4c63-962a-fdbd0d7dc6a8错误");
            }
        }

        public virtual NoticeCateInfo GetNoticeCateByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fb3e87ba-3d4d-480d-934e-80048bcc0100"))
            {
                return MyDAL.GetNoticeCateByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fb3e87ba-3d4d-480d-934e-80048bcc0100错误");
            }
        }

        public virtual IList<NoticeCateInfo> GetNoticeCates(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fb3e87ba-3d4d-480d-934e-80048bcc0100"))
            {
                return MyDAL.GetNoticeCates(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fb3e87ba-3d4d-480d-934e-80048bcc0100错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "fb3e87ba-3d4d-480d-934e-80048bcc0100"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：fb3e87ba-3d4d-480d-934e-80048bcc0100错误");
            }
        }

    }

    public class Notice : INotice
    {
        protected DAL.CMS.INotice MyDAL;
        protected IRBAC RBAC;

        public Notice()
        {
            MyDAL = DAL.CMS.NoticeFactory.CreateNotice();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddNotice(NoticeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d2658816-1905-471f-935e-c60d4620f4d7"))
            {
                return MyDAL.AddNotice(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d2658816-1905-471f-935e-c60d4620f4d7错误");
            }
        }

        public virtual bool EditNotice(NoticeInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "34e5a2e1-5126-4a1f-ad23-dbe7f9e7528a"))
            {
                return MyDAL.EditNotice(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：34e5a2e1-5126-4a1f-ad23-dbe7f9e7528a错误");
            }
        }

        public virtual int DelNotice(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2c551863-a2bd-44a8-aef9-512784f0f4a0"))
            {
                return MyDAL.DelNotice(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2c551863-a2bd-44a8-aef9-512784f0f4a0错误");
            }
        }

        public virtual NoticeInfo GetNoticeByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"))
            {
                return MyDAL.GetNoticeByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7错误");
            }
        }

        public virtual IList<NoticeInfo> GetNotices(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"))
            {
                return MyDAL.GetNotices(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7错误");
            }
        }

        public virtual IList<NoticeInfo> GetNoticeList(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"))
            {
                return MyDAL.GetNoticeList(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7错误");
            }
        }
    }

}
