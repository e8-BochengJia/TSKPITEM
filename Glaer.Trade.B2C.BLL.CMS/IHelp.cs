using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public interface IHelpCate
    {
        bool AddHelpCate(HelpCateInfo entity, RBACUserInfo UserPrivilege);

        bool EditHelpCate(HelpCateInfo entity, RBACUserInfo UserPrivilege);

        int DelHelpCate(int ID, RBACUserInfo UserPrivilege);

        HelpCateInfo GetHelpCateByID(int ID, RBACUserInfo UserPrivilege);

        IList<HelpCateInfo> GetHelpCates(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

    public interface IHelp
    {
        bool AddHelp(HelpInfo entity, RBACUserInfo UserPrivilege);

        bool EditHelp(HelpInfo entity, RBACUserInfo UserPrivilege);

        int DelHelp(int ID, RBACUserInfo UserPrivilege);

        HelpInfo GetHelpByID(int ID, RBACUserInfo UserPrivilege);

        IList<HelpInfo> GetHelps(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

}