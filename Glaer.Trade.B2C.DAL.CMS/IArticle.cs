using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public interface IArticle
    {
        bool AddArticle(ArticleInfo entity);

        bool EditArticle(ArticleInfo entity);

        int DelArticle(int ID);

        ArticleInfo GetArticleByID(int ID);

        IList<ArticleInfo> GetArticles(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        void UpdateArticleViews(int ID);

        #region ArticleCategory

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
        /// 获取指定分类范围下包含指定文章的附加分类
        /// </summary>
        /// <param name="Aid">文章信息ID</param>
        /// <param name="CateIds">文章分类范围</param>
        /// <returns></returns>
        ArticleCategoryInfo GetArticleCategoryByAIDAndCateID(int Aid, string CateIds);


        /// <summary>
        /// 根据文章是否存在指定附加分类
        /// </summary>
        /// <param name="CateID">文章分类ID</param>
        /// <param name="ArticleID">文章信息ID</param>
        /// <returns>文章附加分类实体</returns>
        ArticleCategoryInfo IsExistByCateIDAndArticleID(int CateID, int ArticleID);

        /// <summary>
        /// 获取最新添加的文章信息
        /// </summary>
        /// <returns>文章信息实体</returns>
        ArticleInfo GetGetArticleLastID();


        #endregion
    }

    public interface IArticleCate
    {
        bool AddArticleCate(ArticleCateInfo entity);

        bool EditArticleCate(ArticleCateInfo entity);

        int DelArticleCate(int ID);

        ArticleCateInfo GetArticleCateByID(int ID);

        IList<ArticleCateInfo> GetArticleCates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        string Get_All_SubCateID(int Cate_ID);
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
        bool AddSpecial(SpecialInfo entity);

        bool EditSpecial(SpecialInfo entity);

        int DelSpecial(int ID);

        SpecialInfo GetSpecialByID(int ID);

        IList<SpecialInfo> GetSpecials(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    /// <summary>
    /// 专题报道
    /// </summary>
    public interface IArticleSubject
    {
        bool AddArticleSubject(ArticleSubjectInfo entity);

        bool EditArticleSubject(ArticleSubjectInfo entity);

        int DelArticleSubject(int ID);

        ArticleSubjectInfo GetArticleSubjectByID(int ID);

        IList<ArticleSubjectInfo> GetArticleSubjects(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);

        ArticleSubjectInfo GetArticleSubjectByName(string SubjectName, int SubjectID);
    }


}
