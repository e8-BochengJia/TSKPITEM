using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class Question : IQuestion
    {
        protected DAL.CMS.IQuestion MyDAL;
        protected IRBAC RBAC;

        public Question()
        {
            MyDAL = DAL.CMS.QuestionFactory.CreateQuestion();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddQuestion(QuestionInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "0f8290b2-c31e-4a76-8e1b-078cedbbabcb"))
            {
                return MyDAL.AddQuestion(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：0f8290b2-c31e-4a76-8e1b-078cedbbabcb错误");
            }
           
        }
        
        public virtual bool EditQuestion(QuestionInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d1243e82-cc4e-4b77-a3c9-10c0eb60f499"))
            {
                return MyDAL.EditQuestion(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d1243e82-cc4e-4b77-a3c9-10c0eb60f499错误");
            }
           
        }

        public virtual int DelQuestion(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "46d0f1d1-9bb3-4ffd-afe6-8e214de43db4"))
            {
                return MyDAL.DelQuestion(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：46d0f1d1-9bb3-4ffd-afe6-8e214de43db4错误");
            }
            
        }

        public virtual QuestionInfo GetQuestionByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "318a6535-6af3-4839-9393-816cbc75616d"))
            {
                return MyDAL.GetQuestionByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：318a6535-6af3-4839-9393-816cbc75616d错误");
            }
           
        }

        public virtual IList<QuestionInfo> GetQuestions(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "318a6535-6af3-4839-9393-816cbc75616d"))
            {
                return MyDAL.GetQuestions(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：318a6535-6af3-4839-9393-816cbc75616d错误");
            }
            
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "318a6535-6af3-4839-9393-816cbc75616d"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：318a6535-6af3-4839-9393-816cbc75616d错误");
            }
            
        }

    }
}
