using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.B2C.DAL;

namespace Glaer.Trade.B2C.BLL.Sys
{
    public interface IRBACUser
    {
        bool AddRBACUser(RBACUserInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACUser(RBACUserInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACUser(int ID, RBACUserInfo UserPrivilege);

        RBACUserInfo GetRBACUserByID(int ID, RBACUserInfo UserPrivilege);

        RBACUserInfo GetRBACUserByName(string UserName, RBACUserInfo UserPrivilege);

        IList<RBACUserInfo> GetRBACUsers(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        bool EditUserPassword(string UserPassword, int UserID);

        bool AddRBACUserLog(RBACUserLogInfo entity);

        int DelRBACUserLog(int ID);

        RBACUserLogInfo GetRBACUserLogByID(int ID);

        IList<RBACUserLogInfo> GetRBACUserLogs(QueryInfo Query);

        PageInfo GetUserLogPageInfo(QueryInfo Query);

        IList<RBACUserLogChannelInfo> GetRBACUserLogChannels(QueryInfo Query);
    }

    public interface IRBACUserGroup
    {
        bool AddRBACUserGroup(RBACUserGroupInfo entity, RBACUserInfo UserPrivilege);

        bool EditRBACUserGroup(RBACUserGroupInfo entity, RBACUserInfo UserPrivilege);

        int DelRBACUserGroup(int ID, RBACUserInfo UserPrivilege);

        RBACUserGroupInfo GetRBACUserGroupByID(int ID, RBACUserInfo UserPrivilege);

        IList<RBACUserGroupInfo> GetRBACUserGroups(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);
    }

    public interface IRBACUserRelateCustomer
    {
        bool AddRBACUserRelateCustomer(RBACUserRelateCustomerInfo entity);

        bool EditRBACUserRelateCustomer(RBACUserRelateCustomerInfo entity);

        int DelRBACUserRelateCustomer(int ID);

        int DelRBACUserRelateCustomerByUserID(int UserID);

        string GetRelateCustomerByUserID(int UserID);

        RBACUserRelateCustomerInfo GetRBACUserRelateCustomerByID(int ID);

        IList<RBACUserRelateCustomerInfo> GetRBACUserRelateCustomers(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }
}
