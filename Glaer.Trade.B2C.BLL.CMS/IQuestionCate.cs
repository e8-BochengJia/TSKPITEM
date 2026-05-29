using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public interface IQuestionCate
    {
        bool AddQuestionCate(QuestionCateInfo entity);

        bool EditQuestionCate(QuestionCateInfo entity);

        int DelQuestionCate(int ID);

        QuestionCateInfo GetQuestionCateByID(int ID);

        IList<QuestionCateInfo> GetQuestionCates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }

}
