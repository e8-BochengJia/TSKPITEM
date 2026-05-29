using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.SAL
{
    public class FriendlyLinkCate : IFriendlyLinkCate
    {
        protected DAL.SAL.IFriendlyLinkCate MyDAL;
        protected IRBAC RBAC;

        public FriendlyLinkCate()
        {
            MyDAL = DAL.SAL.FriendlyLinkFactory.CreateFriendlyLinkCate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddFriendlyLinkCate(FriendlyLinkCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0a9f21bd-88cb-4121-94b8-f865a9de2c3b"))
            {
                return MyDAL.AddFriendlyLinkCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0a9f21bd-88cb-4121-94b8-f865a9de2c3b错误");
            }
        }

        public virtual bool EditFriendlyLinkCate(FriendlyLinkCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0a9f21bd-88cb-4121-94b8-f865a9de2c3b"))
            {
                return MyDAL.EditFriendlyLinkCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0a9f21bd-88cb-4121-94b8-f865a9de2c3b错误");
            }
        }

        public virtual int DelFriendlyLinkCate(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0a9f21bd-88cb-4121-94b8-f865a9de2c3b"))
            {
                return MyDAL.DelFriendlyLinkCate(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0a9f21bd-88cb-4121-94b8-f865a9de2c3b错误");
            }
        }

        public virtual FriendlyLinkCateInfo GetFriendlyLinkCateByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0a9f21bd-88cb-4121-94b8-f865a9de2c3b"))
            {
                return MyDAL.GetFriendlyLinkCateByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0a9f21bd-88cb-4121-94b8-f865a9de2c3b错误");
            }
        }

        public virtual IList<FriendlyLinkCateInfo> GetFriendlyLinkCates(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0a9f21bd-88cb-4121-94b8-f865a9de2c3b"))
            {
                return MyDAL.GetFriendlyLinkCates(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0a9f21bd-88cb-4121-94b8-f865a9de2c3b错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0a9f21bd-88cb-4121-94b8-f865a9de2c3b"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0a9f21bd-88cb-4121-94b8-f865a9de2c3b错误");
            }
        }

    }

    public class FriendlyLink : IFriendlyLink
    {
        protected DAL.SAL.IFriendlyLink MyDAL;
        protected IRBAC RBAC;

        public FriendlyLink()
        {
            MyDAL = DAL.SAL.FriendlyLinkFactory.CreateFriendlyLink();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddFriendlyLink(FriendlyLinkInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "54dd622d-fc2d-434d-a36a-c4968caf18b3"))
            {
                return MyDAL.AddFriendlyLink(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

        public virtual bool EditFriendlyLink(FriendlyLinkInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "363bfb90-0d0b-42ae-af25-54004fd061e3"))
            {
                return MyDAL.EditFriendlyLink(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

        public virtual int DelFriendlyLink(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "be7c3360-d8c7-4343-8171-4a54a85ca5a5"))
            {
                return MyDAL.DelFriendlyLink(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

        public virtual FriendlyLinkInfo GetFriendlyLinkByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"))
            {
                return MyDAL.GetFriendlyLinkByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

        public virtual IList<FriendlyLinkInfo> GetFriendlyLinks(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"))
            {
                return MyDAL.GetFriendlyLinks(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限");
            }
        }

    }
}
