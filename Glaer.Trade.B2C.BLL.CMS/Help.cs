using System;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class HelpCate : IHelpCate
    {
        protected DAL.CMS.IHelpCate MyDAL;
        protected IRBAC RBAC;

        public HelpCate()
        {
            MyDAL = DAL.CMS.HelpFactory.CreateHelpCate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddHelpCate(HelpCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e98d5669-6825-47e0-9f0b-d5c6af91f72e"))
            {
                return MyDAL.AddHelpCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e98d5669-6825-47e0-9f0b-d5c6af91f72e错误");
            }
        }

        public virtual bool EditHelpCate(HelpCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a0059a41-e628-4625-a67a-9da2f8b20fe1"))
            {
                return MyDAL.EditHelpCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a0059a41-e628-4625-a67a-9da2f8b20fe1错误");
            }
        }

        public virtual int DelHelpCate(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b14f283a-740b-48e1-b243-60105b87a9a6"))
            {
                return MyDAL.DelHelpCate(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b14f283a-740b-48e1-b243-60105b87a9a6错误");
            }
        }

        public virtual HelpCateInfo GetHelpCateByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e2e6aec7-ff11-407b-9c3a-6317b06b1a7e"))
            {
                return MyDAL.GetHelpCateByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e2e6aec7-ff11-407b-9c3a-6317b06b1a7e错误");
            }
        }

        public virtual IList<HelpCateInfo> GetHelpCates(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e2e6aec7-ff11-407b-9c3a-6317b06b1a7e"))
            {
                return MyDAL.GetHelpCates(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e2e6aec7-ff11-407b-9c3a-6317b06b1a7e错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e2e6aec7-ff11-407b-9c3a-6317b06b1a7e"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e2e6aec7-ff11-407b-9c3a-6317b06b1a7e错误");
            }
        }

    }

    public class Help : IHelp
    {
        protected DAL.CMS.IHelp MyDAL;
        protected IRBAC RBAC;

        public Help()
        {
            MyDAL = DAL.CMS.HelpFactory.CreateHelp();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddHelp(HelpInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e64214dc-33e4-4576-bab9-deb7802bad6c"))
            {
                return MyDAL.AddHelp(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e64214dc-33e4-4576-bab9-deb7802bad6c错误");
            }
        }

        public virtual bool EditHelp(HelpInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "14422eb0-8367-45e1-b955-c40aee162094"))
            {
                return MyDAL.EditHelp(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：14422eb0-8367-45e1-b955-c40aee162094错误");
            }
        }

        public virtual int DelHelp(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c8585704-c4d5-40e8-8f5c-89940b5d7dfc"))
            {
                return MyDAL.DelHelp(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c8585704-c4d5-40e8-8f5c-89940b5d7dfc错误");
            }
        }

        public virtual HelpInfo GetHelpByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a015e960-173c-429d-98d2-69e5a023b5dc"))
            {
                return MyDAL.GetHelpByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a015e960-173c-429d-98d2-69e5a023b5dc错误");
            }
        }

        public virtual IList<HelpInfo> GetHelps(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a015e960-173c-429d-98d2-69e5a023b5dc"))
            {
                return MyDAL.GetHelps(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a015e960-173c-429d-98d2-69e5a023b5dc错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "a015e960-173c-429d-98d2-69e5a023b5dc"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：a015e960-173c-429d-98d2-69e5a023b5dc错误");
            }
        }

    }

}
