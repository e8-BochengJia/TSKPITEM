using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IActivityUser
    {
        bool AddActivityUser(UserInfo entity);

        bool EditActivityUser(UserInfo entity);

        int DelActivityUser(int ID);

        UserInfo GetActivityUserByID(int ID);

        IList<UserInfo> GetActivityUsers(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        UserInfo GetActivityUserByLogin(string name, string password);
        
    }
}
