using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;


namespace Glaer.Trade.B2C.DAL.Sys
{
    public interface IRBACUser
    {
        bool AddRBACUser(RBACUserInfo entity);

        bool EditRBACUser(RBACUserInfo entity);

        int DelRBACUser(int ID);

        RBACUserInfo GetRBACUserByID(int ID);

        RBACUserInfo GetRBACUserByName(string UserName);

        IList<RBACUserInfo> GetRBACUsers(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        IList<RBACRoleInfo> GetRoleListByUser(int User_ID);

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
        bool AddRBACUserGroup(RBACUserGroupInfo entity);

        bool EditRBACUserGroup(RBACUserGroupInfo entity);

        int DelRBACUserGroup(int ID);

        RBACUserGroupInfo GetRBACUserGroupByID(int ID);

        IList<RBACUserGroupInfo> GetRBACUserGroups(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
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