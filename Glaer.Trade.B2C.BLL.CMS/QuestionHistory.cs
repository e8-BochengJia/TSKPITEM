using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
   

    public class QuestionHistory : IQuestionHistory
    {
        protected DAL.CMS.IQuestionHistory MyDAL;
        protected IRBAC RBAC;

        public QuestionHistory()
        {
            MyDAL = DAL.CMS.QuestionHistoryFactory.CreateQuestionHistory();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddQuestionHistory(QuestionHistoryInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "29799310-e6bc-491c-812e-0d87be7200e2"))
            {
                return MyDAL.AddQuestionHistory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：29799310-e6bc-491c-812e-0d87be7200e2错误");
            }
            
        }

        public virtual bool EditQuestionHistory(QuestionHistoryInfo entity, RBACUserInfo UserPrivilege)
        {

            if (RBAC.CheckPrivilege(UserPrivilege, "7e0a9a43-af8f-44c9-b00e-aa8de567f9e7"))
            {
                return MyDAL.EditQuestionHistory(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：7e0a9a43-af8f-44c9-b00e-aa8de567f9e7错误");
            }
        }

        public virtual int DelQuestionHistory(int ID, RBACUserInfo UserPrivilege)
        {

            if (RBAC.CheckPrivilege(UserPrivilege, "4a2a3deb-cc3b-42eb-898e-0de38315fef6"))
            {
                return MyDAL.DelQuestionHistory(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：4a2a3deb-cc3b-42eb-898e-0de38315fef6错误");
            }
        }

        public virtual QuestionHistoryInfo GetQuestionHistoryByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0727f3b4-4edc-4e49-94a0-d728fe7d35ef"))
            {
                return MyDAL.GetQuestionHistoryByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0727f3b4-4edc-4e49-94a0-d728fe7d35ef错误");
            }
          
        }

        public virtual IList<QuestionHistoryInfo> GetQuestionHistorys(QueryInfo Query, RBACUserInfo UserPrivilege)
        {

            if (RBAC.CheckPrivilege(UserPrivilege, "0727f3b4-4edc-4e49-94a0-d728fe7d35ef"))
            {
                return MyDAL.GetQuestionHistorys(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0727f3b4-4edc-4e49-94a0-d728fe7d35ef错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {

            if (RBAC.CheckPrivilege(UserPrivilege, "0727f3b4-4edc-4e49-94a0-d728fe7d35ef"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0727f3b4-4edc-4e49-94a0-d728fe7d35ef错误");
            }
        }

    }
}
