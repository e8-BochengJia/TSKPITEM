using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
   
    public interface IQuestionHistory
    {
        bool AddQuestionHistory(QuestionHistoryInfo entity, RBACUserInfo UserPrivilege);

        bool EditQuestionHistory(QuestionHistoryInfo entity, RBACUserInfo UserPrivilege);

        int DelQuestionHistory(int ID, RBACUserInfo UserPrivilege);

        QuestionHistoryInfo GetQuestionHistoryByID(int ID, RBACUserInfo UserPrivilege);

        IList<QuestionHistoryInfo> GetQuestionHistorys(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

}
