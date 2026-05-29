using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class About : IAbout
    {
        protected DAL.CMS.IAbout MyDAL;
        protected IRBAC RBAC;

        public About()
        { 
            MyDAL = DAL.CMS.AboutFactory.CreateAbout();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddAbout(AboutInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "c747b411-cf59-447b-a2d7-7e5510589f25"))
            {
                return MyDAL.AddAbout(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：c747b411-cf59-447b-a2d7-7e5510589f25错误");
            }
        }

        public virtual bool EditAbout(AboutInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "b15dd1c4-d9c5-4b09-b7c2-3ef4d24af7ef"))
            {
                return MyDAL.EditAbout(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：b15dd1c4-d9c5-4b09-b7c2-3ef4d24af7ef错误");
            }
        }

        public virtual int DelAbout(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "622c8cf4-0cae-47f7-bd02-19bd8b5c169d"))
            {
                return MyDAL.DelAbout(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：622c8cf4-0cae-47f7-bd02-19bd8b5c169d错误");
            }
        }

        public virtual AboutInfo GetAboutByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "db8de73b-9ac0-476e-866e-892dd35589c5"))
            {
                return MyDAL.GetAboutByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：db8de73b-9ac0-476e-866e-892dd35589c5错误");
            }
        }

        public virtual AboutInfo GetAboutBySign(string Sign, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "db8de73b-9ac0-476e-866e-892dd35589c5"))
            {
                return MyDAL.GetAboutBySign(Sign);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：db8de73b-9ac0-476e-866e-892dd35589c5错误");
            }
        }

        public virtual IList<AboutInfo> GetAbouts(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "db8de73b-9ac0-476e-866e-892dd35589c5"))
            {
                return MyDAL.GetAbouts(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：db8de73b-9ac0-476e-866e-892dd35589c5错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "db8de73b-9ac0-476e-866e-892dd35589c5"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：db8de73b-9ac0-476e-866e-892dd35589c5错误");
            }
        }

    }

}
