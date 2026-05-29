using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public interface IQuestion
    {
        bool AddQuestion(QuestionInfo entity, RBACUserInfo UserPrivilege);

        bool EditQuestion(QuestionInfo entity, RBACUserInfo UserPrivilege);

        int DelQuestion(int ID, RBACUserInfo UserPrivilege);

        QuestionInfo GetQuestionByID(int ID, RBACUserInfo UserPrivilege);

        IList<QuestionInfo> GetQuestions(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

}
