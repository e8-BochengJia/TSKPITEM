using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class QuestionCate : IQuestionCate
    {
        protected DAL.CMS.IQuestionCate MyDAL;
        protected IRBAC RBAC;

        public QuestionCate()
        {
            MyDAL = DAL.CMS.QuestionCateFactory.CreateQuestionCate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddQuestionCate(QuestionCateInfo entity)
        {
            return MyDAL.AddQuestionCate(entity);
        }

        public virtual bool EditQuestionCate(QuestionCateInfo entity)
        {
            return MyDAL.EditQuestionCate(entity);
        }

        public virtual int DelQuestionCate(int ID)
        {
            return MyDAL.DelQuestionCate(ID);
        }

        public virtual QuestionCateInfo GetQuestionCateByID(int ID)
        {
            return MyDAL.GetQuestionCateByID(ID);
        }

        public virtual IList<QuestionCateInfo> GetQuestionCates(QueryInfo Query)
        {
            return MyDAL.GetQuestionCates(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
