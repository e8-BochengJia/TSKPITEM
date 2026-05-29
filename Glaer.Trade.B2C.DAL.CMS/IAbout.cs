using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public interface IAbout
    {
        bool AddAbout(AboutInfo entity);

        bool EditAbout(AboutInfo entity);

        int DelAbout(int ID);

        AboutInfo GetAboutByID(int ID);

        AboutInfo GetAboutBySign(string Sign);

        IList<AboutInfo> GetAbouts(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
