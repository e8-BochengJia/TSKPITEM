using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.DAL;
using Glaer.Trade.B2C.RBAC;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class ArticleCate : IArticleCate
    {
        protected DAL.CMS.IArticleCate MyDAL;
        protected IRBAC RBAC;

        public ArticleCate()
        {
            MyDAL = DAL.CMS.ArticleFactory.CreateArticleCate();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddArticleCate(ArticleCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "e049fd16-8e37-4096-aa7e-e20ecb61c934"))
            {
                return MyDAL.AddArticleCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：e049fd16-8e37-4096-aa7e-e20ecb61c934错误");
            }
        }

        public virtual bool EditArticleCate(ArticleCateInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8e2eb41c-060b-4a1c-9c7c-403d6f1072fa"))
            {
                return MyDAL.EditArticleCate(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8e2eb41c-060b-4a1c-9c7c-403d6f1072fa错误");
            }
        }

        public virtual int DelArticleCate(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8ad36b15-547d-4ef0-aa55-e4fce614af3c"))
            {
                return MyDAL.DelArticleCate(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8ad36b15-547d-4ef0-aa55-e4fce614af3c错误");
            }
        }

        public virtual ArticleCateInfo GetArticleCateByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1a3208d0-70a4-49dd-8010-400f1254535a"))
            {
                return MyDAL.GetArticleCateByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1a3208d0-70a4-49dd-8010-400f1254535a错误");
            }
        }

        public virtual IList<ArticleCateInfo> GetArticleCates(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1a3208d0-70a4-49dd-8010-400f1254535a"))
            {
                return MyDAL.GetArticleCates(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1a3208d0-70a4-49dd-8010-400f1254535a错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetPageInfo(Query);
        }

        public virtual string Get_All_SubCateID(int Cate_ID)
        {
            return MyDAL.Get_All_SubCateID(Cate_ID);
        }

        

    }

    public class Article : IArticle
    {
        protected DAL.CMS.IArticle MyDAL;
        protected IRBAC RBAC;

        public Article()
        {
            MyDAL = DAL.CMS.ArticleFactory.CreateArticle();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddArticle(ArticleInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "870e6332-ab75-41cc-98c3-17e8af7827d3"))
            {
                return MyDAL.AddArticle(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：870e6332-ab75-41cc-98c3-17e8af7827d3错误");
            }
        }

        public virtual bool EditArticle(ArticleInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "1daab676-20b6-4073-af76-132ee8874556"))
            {
                return MyDAL.EditArticle(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1daab676-20b6-4073-af76-132ee8874556错误");
            }
        }

        public virtual int DelArticle(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "cc00c494-d211-438c-baef-ac20d419b066"))
            {
                return MyDAL.DelArticle(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：cc00c494-d211-438c-baef-ac20d419b066错误");
            }
        }

        public virtual ArticleInfo GetArticleByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"))
            {
                return MyDAL.GetArticleByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276错误");
            }
        }

        public virtual IList<ArticleInfo> GetArticles(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"))
            {
                return MyDAL.GetArticles(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276错误");
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276错误");
            }
        }

        public virtual void UpdateArticleViews(int ID, RBACUserInfo UserPrivilege)
        {
            
            if (RBAC.CheckPrivilege(UserPrivilege, "1daab676-20b6-4073-af76-132ee8874556"))
            {
                MyDAL.UpdateArticleViews(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：1daab676-20b6-4073-af76-132ee8874556错误");
            }

        }
        /// <summary>
        /// 添加文章附加分类
        /// </summary>
        /// <param name="entity">文章附加分类实体</param>
        /// <returns>操作结果：true(成功)、flash(失败)</returns>
        public virtual bool AddArticleCategory(ArticleCategoryInfo entity)
        {
            return MyDAL.AddArticleCategory(entity);
        }

        /// <summary>
        /// 删除指定文章信息的附加分类信息
        /// </summary>
        /// <param name="ID">文章信息ID</param>
        /// <returns>影响记录数量</returns>
        public virtual int DelArticleCategory(int ID)
        {
            return MyDAL.DelArticleCategory(ID);
        }
        /// <summary>
        /// 根据查询条件获取文章附加分类
        /// </summary>
        /// <param name="Query">查询条件集合实体</param>
        /// <returns>文章附加分类实体集合</returns>
        public virtual IList<ArticleCategoryInfo> GetArticleCategorys(QueryInfo Query)
        {
            return MyDAL.GetArticleCategorys(Query);
        }

        /// <summary>
        /// 获取指定分类下所有文章附加分类信息
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="CateIDs">文章分类ID范围（二选一）</param>
        /// <returns>文章附加分类实体集合</returns>
        public virtual IList<ArticleCategoryInfo> GetArticleCategorys(int CateID, params string[] CateIDs)
        {
            return MyDAL.GetArticleCategorys(CateID, CateIDs);
        }

        /// <summary>
        /// 根据文章是否存在指定附加分类
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="ArticleID">文章信息ID</param>
        /// <returns>文章附加分类实体</returns>
        public virtual ArticleCategoryInfo IsExistByCateIDAndArticleID(int CateID, int ArticleID)
        {
            return MyDAL.IsExistByCateIDAndArticleID(CateID, ArticleID);
        }

        /// <summary>
        /// 获取指定分类范围下包含指定文章的附加分类
        /// </summary>
        /// <param name="Aid">文章信息ID</param>
        /// <param name="CateIds">文章分类范围</param>
        /// <returns></returns>
        public virtual ArticleCategoryInfo GetArticleCategoryByAIDAndCateID(int Aid, string CateIds)
        {
            return MyDAL.GetArticleCategoryByAIDAndCateID(Aid, CateIds);
        }
        /// <summary>
        /// 获取最新添加的文章信息
        /// </summary>
        /// <returns>文章信息实体</returns>
        public virtual ArticleInfo GetGetArticleLastID(RBACUserInfo UserPrivilege)
        {

            if (RBAC.CheckPrivilege(UserPrivilege, "8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"))
            {
                return MyDAL.GetGetArticleLastID();
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276错误");
            }
        }
    }

    public class SensitiveWords : ISensitiveWords
    {
        protected DAL.CMS.ISensitiveWords MyDAL;
        protected IRBAC RBAC;

        public SensitiveWords()
        {
            MyDAL = DAL.CMS.SensitiveWordsFactory.CreateSensitiveWords();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSensitiveWords(SensitiveWordsInfo entity)
        {
            return MyDAL.AddSensitiveWords(entity);
        }

        public virtual bool EditSensitiveWords(SensitiveWordsInfo entity)
        {
            return MyDAL.EditSensitiveWords(entity);
        }

        public virtual int DelSensitiveWords(int ID)
        {
            return MyDAL.DelSensitiveWords(ID);
        }

        public virtual SensitiveWordsInfo GetSensitiveWordsByID(int ID)
        {
            return MyDAL.GetSensitiveWordsByID(ID);
        }

        public virtual IList<SensitiveWordsInfo> GetSensitiveWordss(QueryInfo Query)
        {
            return MyDAL.GetSensitiveWordss(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

    public class Special : ISpecial
    {
        protected DAL.CMS.ISpecial MyDAL;
        protected IRBAC RBAC;

        public Special()
        {
            MyDAL = DAL.CMS.SpecialFactory.CreateSpecial();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddSpecial(SpecialInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "98ebbcbe-e719-4302-890e-c4c420509ee0"))
            {
                return MyDAL.AddSpecial(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：98ebbcbe-e719-4302-890e-c4c420509ee0错误");
            }

            
        }

        public virtual bool EditSpecial(SpecialInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "d3570eff-1fc9-48bd-a247-ba7db0bc18bd"))
            {
                return MyDAL.EditSpecial(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：d3570eff-1fc9-48bd-a247-ba7db0bc18bd错误");
            }
            
        }

        public virtual int DelSpecial(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "8152aeb2-3302-4ea9-bbc1-a3dcb300c4f8"))
            {
                return MyDAL.DelSpecial(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：8152aeb2-3302-4ea9-bbc1-a3dcb300c4f8错误");
            }
            
        }

        public virtual SpecialInfo GetSpecialByID(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "86aa82ef-9cb2-49e4-a8c9-db708ab33f3a"))
            {
                return MyDAL.GetSpecialByID(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：86aa82ef-9cb2-49e4-a8c9-db708ab33f3a错误");
            }
            
        }

        public virtual IList<SpecialInfo> GetSpecials(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "86aa82ef-9cb2-49e4-a8c9-db708ab33f3a"))
            {
                return MyDAL.GetSpecials(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：86aa82ef-9cb2-49e4-a8c9-db708ab33f3a错误");
            }
            
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "86aa82ef-9cb2-49e4-a8c9-db708ab33f3a"))
            {
                return MyDAL.GetPageInfo(Query);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：86aa82ef-9cb2-49e4-a8c9-db708ab33f3a错误");
            }
            
        }

    }


    public class ArticleSubject : IArticleSubject
    {
        protected DAL.CMS.IArticleSubject MyDAL;
        protected IRBAC RBAC;

        public ArticleSubject()
        {
            MyDAL = DAL.CMS.ArticleFactory.CreateArticleSubject();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddArticleSubject(ArticleSubjectInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "2b40c0e9-1543-48e5-8836-d7addfee4236"))
            {
                return MyDAL.AddArticleSubject(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：2b40c0e9-1543-48e5-8836-d7addfee4236错误");
            }
        }
        public virtual bool EditArticleSubject(ArticleSubjectInfo entity, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "ae5b5047-b85f-4934-84a0-e4f4f898dd78"))
            {
                return MyDAL.EditArticleSubject(entity);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：ae5b5047-b85f-4934-84a0-e4f4f898dd78错误");
            }
        }

        public virtual int DelArticleSubject(int ID, RBACUserInfo UserPrivilege)
        {
            if (RBAC.CheckPrivilege(UserPrivilege, "79d6139b-950d-4598-9a90-1cb67505205e"))
            {
                return MyDAL.DelArticleSubject(ID);
            }
            else
            {
                throw new TradePrivilegeException("没有权限，权限代码：79d6139b-950d-4598-9a90-1cb67505205e错误");
            }
        }

        public virtual ArticleSubjectInfo GetArticleSubjectByID(int ID, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetArticleSubjectByID(ID);
        }


        public virtual ArticleSubjectInfo GetArticleSubjectByName(string SubjectName, int SubjetcID)
        {
            return MyDAL.GetArticleSubjectByName(SubjectName, SubjetcID);
        }

        public virtual IList<ArticleSubjectInfo> GetArticleSubjects(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetArticleSubjects(Query);
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege)
        {
            return MyDAL.GetPageInfo(Query);
        }

    }

}
