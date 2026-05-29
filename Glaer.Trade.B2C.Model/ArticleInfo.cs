using System;

namespace Glaer.Trade.B2C.Model
{
    public class ArticleCateInfo
    {
        private int _Article_Cate_ID;
        private int _Article_Cate_ParentID;
        private string _Article_Cate_Name;
        private int _Article_Cate_Sort;
        private string _Article_Cate_Site;
        private string _Article_Cate_Href;
        private string _Article_Cate_SEO_Title;
        private string _Article_Cate_SEO_Keyword;
        private string _Article_Cate_SEO_Description;
        private int _Article_Cate_IsTop;
        private int _Article_Cate_Type;

        public int Article_Cate_Type
        {
            get { return _Article_Cate_Type; }
            set { _Article_Cate_Type = value; }
        }

        public int Article_Cate_IsTop
        {
            get { return _Article_Cate_IsTop; }
            set { _Article_Cate_IsTop = value; }
        }

        public string Article_Cate_SEO_Title
        {
            get { return _Article_Cate_SEO_Title; }
            set { _Article_Cate_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Article_Cate_SEO_Keyword
        {
            get { return _Article_Cate_SEO_Keyword; }
            set { _Article_Cate_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Article_Cate_SEO_Description
        {
            get { return _Article_Cate_SEO_Description; }
            set { _Article_Cate_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        } 

        public string Article_Cate_Href
        {
            get { return _Article_Cate_Href; }
            set { _Article_Cate_Href = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public int Article_Cate_ID
        {
            get { return _Article_Cate_ID; }
            set { _Article_Cate_ID = value; }
        }

        public int Article_Cate_ParentID
        {
            get { return _Article_Cate_ParentID; }
            set { _Article_Cate_ParentID = value; }
        }

        public string Article_Cate_Name
        {
            get { return _Article_Cate_Name; }
            set { _Article_Cate_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Article_Cate_Sort
        {
            get { return _Article_Cate_Sort; }
            set { _Article_Cate_Sort = value; }
        }

        public string Article_Cate_Site
        {
            get { return _Article_Cate_Site; }
            set { _Article_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class ArticleInfo
    {
        private int _Article_ID;
        private int _Article_CateID;
        private string _Article_Title;
        private string _Article_Source;
        private string _Article_Author;
        private string _Article_Img;
        private string _Article_Keyword;
        private string _Article_Intro;
        private string _Article_Content;
        private DateTime _Article_Addtime;
        private int _Article_Hits;
        private int _Article_IsRecommend;
        private int _Article_IsAudit;
        private int _Article_Sort;
        private string _Article_Site;
        private string _Article_Hyperlink;
        private int _Article_ContentID;
        private string _Article_SEO_Title;
        private string _Article_SEO_Keyword;
        private string _Article_SEO_Description;
        private int _Article_PageViews;
        private string _Artide_ShoulderTitle;
        private int _Artide_ShoulderTitleSize;
        private int _Article_HyperlinkSize;
        private int _Artide_IsTop;
        private int _Subject_ID;
        private int _Artide_SouceType;
        private int _Article_memberID;

        public string Artide_ShoulderTitle
        {
            get { return _Artide_ShoulderTitle; }
            set { _Artide_ShoulderTitle = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Subject_ID
        {
            get { return _Subject_ID; }
            set { _Subject_ID = value; }
        }

        public int Article_memberID
        {
            get { return _Article_memberID; }
            set { _Article_memberID = value; }
        }

        public int Artide_SouceType
        {
            get { return _Artide_SouceType; }
            set { _Artide_SouceType = value; }
        } 

        public int Artide_ShoulderTitleSize
        {
            get { return _Artide_ShoulderTitleSize; }
            set { _Artide_ShoulderTitleSize = value; }
        }

        public int Article_HyperlinkSize
        {
            get { return _Article_HyperlinkSize; }
            set { _Article_HyperlinkSize = value; }
        }

        public int Artide_IsTop
        {
            get { return _Artide_IsTop; }
            set { _Artide_IsTop = value; }
        }

        public int Article_ID
        {
            get { return _Article_ID; }
            set { _Article_ID = value; }
        }

        public int Article_CateID
        {
            get { return _Article_CateID; }
            set { _Article_CateID = value; }
        }

        public string Article_Title
        {
            get { return _Article_Title; }
            set { _Article_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Article_Source
        {
            get { return _Article_Source; }
            set { _Article_Source = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Article_Author
        {
            get { return _Article_Author; }
            set { _Article_Author = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Article_Img
        {
            get { return _Article_Img; }
            set { _Article_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Article_Keyword
        {
            get { return _Article_Keyword; }
            set { _Article_Keyword = value.Length > 250 ? value.Substring(0, 250) : value.ToString(); }
        }

        public string Article_Intro
        {
            get { return _Article_Intro; }
            set { _Article_Intro = value.Length > 250 ? value.Substring(0, 250) : value.ToString(); }
        }

        public string Article_Content
        {
            get { return _Article_Content; }
            set { _Article_Content = value; }
        }

        public DateTime Article_Addtime
        {
            get { return _Article_Addtime; }
            set { _Article_Addtime = value; }
        }

        public int Article_Hits
        {
            get { return _Article_Hits; }
            set { _Article_Hits = value; }
        }

        public int Article_IsRecommend
        {
            get { return _Article_IsRecommend; }
            set { _Article_IsRecommend = value; }
        }

        public int Article_IsAudit
        {
            get { return _Article_IsAudit; }
            set { _Article_IsAudit = value; }
        }

        public int Article_Sort
        {
            get { return _Article_Sort; }
            set { _Article_Sort = value; }
        }

        public string Article_Site
        {
            get { return _Article_Site; }
            set { _Article_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Article_Hyperlink
        {
            get { return _Article_Hyperlink; }
            set { _Article_Hyperlink = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Article_ContentID
        {
            get { return _Article_ContentID; }
            set { _Article_ContentID = value; }
        }

        public string Article_SEO_Title
        {
            get { return _Article_SEO_Title; }
            set { _Article_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Article_SEO_Keyword
        {
            get { return _Article_SEO_Keyword; }
            set { _Article_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Article_SEO_Description
        {
            get { return _Article_SEO_Description; }
            set { _Article_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public int Article_PageViews
        {
            get { return _Article_PageViews; }
            set { _Article_PageViews = value; }
        }

    }

    public class SpecialInfo
    {
        private int _Special_ID;
        private string _Special_Title;
        private string _Special_Intro;
        private string _Special_Img;
        private string _Special_BannerImg;
        private int _Special_Sort;
        private int _Special_IsRecommend;
        private int _Special_IsAudit;
        private string _Special_Site;
        private DateTime _Special_Addtime;
        private int _Special_CateID;

        public int Special_ID
        {
            get { return _Special_ID; }
            set { _Special_ID = value; }
        }

        public string Special_Title
        {
            get { return _Special_Title; }
            set { _Special_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Special_Intro
        {
            get { return _Special_Intro; }
            set { _Special_Intro = value.Length > 250 ? value.Substring(0, 250) : value.ToString(); }
        }

        public string Special_Img
        {
            get { return _Special_Img; }
            set { _Special_Img = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Special_BannerImg
        {
            get { return _Special_BannerImg; }
            set { _Special_BannerImg = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Special_Sort
        {
            get { return _Special_Sort; }
            set { _Special_Sort = value; }
        }

        public int Special_IsRecommend
        {
            get { return _Special_IsRecommend; }
            set { _Special_IsRecommend = value; }
        }

        public int Special_IsAudit
        {
            get { return _Special_IsAudit; }
            set { _Special_IsAudit = value; }
        }

        public string Special_Site
        {
            get { return _Special_Site; }
            set { _Special_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Special_Addtime
        {
            get { return _Special_Addtime; }
            set { _Special_Addtime = value; }
        }

        public int Special_CateID
        {
            get { return _Special_CateID; }
            set { _Special_CateID = value; }
        }
    }

    /// <summary>
    /// 资讯附加分类
    /// </summary>
    public class ArticleCategoryInfo
    {
        private int _Article_Category_ID;
        private int _Article_Category_ArticleID;
        private int _Article_Category_CategoryID;

        public int Article_Category_ID
        {
            get { return _Article_Category_ID; }
            set { _Article_Category_ID = value; }
        }

        public int Article_Category_ArticleID
        {
            get { return _Article_Category_ArticleID; }
            set { _Article_Category_ArticleID = value; }
        }

        public int Article_Category_CategoryID
        {
            get { return _Article_Category_CategoryID; }
            set { _Article_Category_CategoryID = value; }
        }

    }
}
