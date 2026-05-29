using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public interface IArticleCate
    {
        bool AddArticleCate(ArticleCateInfo entity, RBACUserInfo UserPrivilege);

        bool EditArticleCate(ArticleCateInfo entity, RBACUserInfo UserPrivilege);

        int DelArticleCate(int ID, RBACUserInfo UserPrivilege);

        ArticleCateInfo GetArticleCateByID(int ID, RBACUserInfo UserPrivilege);

        IList<ArticleCateInfo> GetArticleCates(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        string Get_All_SubCateID(int Cate_ID);

    }

    public interface IArticle
    {
        bool AddArticle(ArticleInfo entity, RBACUserInfo UserPrivilege);

        bool EditArticle(ArticleInfo entity, RBACUserInfo UserPrivilege);

        int DelArticle(int ID, RBACUserInfo UserPrivilege);

        ArticleInfo GetArticleByID(int ID, RBACUserInfo UserPrivilege);

        IList<ArticleInfo> GetArticles(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

        void UpdateArticleViews(int ID, RBACUserInfo UserPrivilege);
        /// <summary>
        /// 添加文章附加分类
        /// </summary>
        /// <param name="entity">文章附加分类实体</param>
        /// <returns>操作结果：true(成功)、false(失败)</returns>
        bool AddArticleCategory(ArticleCategoryInfo entity);

        /// <summary>
        /// 删除指定文章信息的附加分类信息
        /// </summary>
        /// <param name="ID">文章信息ID</param>
        /// <returns>影响记录数量</returns>
        int DelArticleCategory(int ID);
        /// <summary>
        /// 根据查询条件获取文章附加分类
        /// </summary>
        /// <param name="Query">查询条件集合实体</param>
        /// <returns>文章附加分类实体集合</returns>
        IList<ArticleCategoryInfo> GetArticleCategorys(QueryInfo Query);

        /// <summary>
        /// 获取指定分类下所有文章附加分类信息
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="CateIDs">文章分类ID范围（二选一）</param>
        /// <returns>文章附加分类实体集合</returns>
        IList<ArticleCategoryInfo> GetArticleCategorys(int CateID, params string[] CateIDs);

        /// <summary>
        /// 根据文章是否存在指定附加分类
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="ArticleID">文章信息ID</param>
        /// <returns>文章附加分类实体</returns>
        ArticleCategoryInfo IsExistByCateIDAndArticleID(int CateID, int ArticleID);

        /// <summary>
        /// 获取指定分类范围下包含指定文章的附加分类
        /// </summary>
        /// <param name="Aid">文章信息ID</param>
        /// <param name="CateIds">文章分类范围</param>
        /// <returns></returns>
        ArticleCategoryInfo GetArticleCategoryByAIDAndCateID(int Aid, string CateIds);

        /// 获取最新添加的文章信息
        /// </summary>
        /// <param name="UserPrivilege">管理员实体</param>
        /// <returns>文章信息实体</returns>
        ArticleInfo GetGetArticleLastID(RBACUserInfo UserPrivilege);

   
    }

    public interface ISensitiveWords
    {
        bool AddSensitiveWords(SensitiveWordsInfo entity);

        bool EditSensitiveWords(SensitiveWordsInfo entity);

        int DelSensitiveWords(int ID);

        SensitiveWordsInfo GetSensitiveWordsByID(int ID);

        IList<SensitiveWordsInfo> GetSensitiveWordss(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

    }


    public interface ISpecial
    {
        bool AddSpecial(SpecialInfo entity, RBACUserInfo UserPrivilege);

        bool EditSpecial(SpecialInfo entity, RBACUserInfo UserPrivilege);

        int DelSpecial(int ID, RBACUserInfo UserPrivilege);

        SpecialInfo GetSpecialByID(int ID, RBACUserInfo UserPrivilege);

        IList<SpecialInfo> GetSpecials(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);

    }

    /// <summary>
    /// 专题报道接口
    /// </summary>
    public interface IArticleSubject
    {
        bool AddArticleSubject(ArticleSubjectInfo entity, RBACUserInfo UserPrivilege);

        bool EditArticleSubject(ArticleSubjectInfo entity, RBACUserInfo UserPrivilege);

        int DelArticleSubject(int ID, RBACUserInfo UserPrivilege);

        ArticleSubjectInfo GetArticleSubjectByID(int ID, RBACUserInfo UserPrivilege);

        IList<ArticleSubjectInfo> GetArticleSubjects(QueryInfo Query, RBACUserInfo UserPrivilege);

        PageInfo GetPageInfo(QueryInfo Query, RBACUserInfo UserPrivilege);



        ArticleSubjectInfo GetArticleSubjectByName(string SubjectName, int SubjetcID);
    }
}