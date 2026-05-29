using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public interface IQuestion
    {
        bool AddQuestion(QuestionInfo entity);

        bool EditQuestion(QuestionInfo entity);

        int DelQuestion(int ID);

        QuestionInfo GetQuestionByID(int ID);

        IList<QuestionInfo> GetQuestions(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }


}
