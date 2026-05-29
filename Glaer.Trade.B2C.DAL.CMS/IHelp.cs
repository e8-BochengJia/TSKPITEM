using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public interface IHelpCate
    {
        bool AddHelpCate(HelpCateInfo entity);

        bool EditHelpCate(HelpCateInfo entity);

        int DelHelpCate(int ID);

        HelpCateInfo GetHelpCateByID(int ID);

        IList<HelpCateInfo> GetHelpCates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface IHelp
    {
        bool AddHelp(HelpInfo entity);

        bool EditHelp(HelpInfo entity);

        int DelHelp(int ID);

        HelpInfo GetHelpByID(int ID);

        IList<HelpInfo> GetHelps(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
