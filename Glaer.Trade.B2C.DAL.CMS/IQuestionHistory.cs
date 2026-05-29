using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.CMS
{
   
    public interface IQuestionHistory
    {
        bool AddQuestionHistory(QuestionHistoryInfo entity);

        bool EditQuestionHistory(QuestionHistoryInfo entity);

        int DelQuestionHistory(int ID);

        QuestionHistoryInfo GetQuestionHistoryByID(int ID);

        IList<QuestionHistoryInfo> GetQuestionHistorys(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

}
