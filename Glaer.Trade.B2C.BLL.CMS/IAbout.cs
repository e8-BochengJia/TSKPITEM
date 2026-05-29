using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public interface IAbout
    {
        bool AddAbout(AboutInfo entity, RBACUserInfo UserPrivilege);

        bool EditAbout(AboutInfo entity, RBACUserInfo UserPrivilege);

        int DelAbout(int ID, RBACUserInfo UserPrivilege);

        AboutInfo GetAboutByID(int ID, RBACUserInfo UserPrivilege);

        AboutInfo GetAboutBySign(string Sign, RBACUserInfo UserPrivilege);

        IList<AboutInfo> GetAbouts(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

}
