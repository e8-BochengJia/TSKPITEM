using System;

namespace Glaer.Trade.B2C.Model
{
    public class HelpCateInfo
    {
        private int _Help_Cate_ID;
        private int _Help_Cate_ParentID;
        private string _Help_Cate_Name;
        private int _Help_Cate_Sort;
        private string _Help_Cate_Site;
        private string _Help_Cate_SEO_Title;
        private string _Help_Cate_SEO_Keyword;
        private string _Help_Cate_SEO_Description;

        public int Help_Cate_ID
        {
            get { return _Help_Cate_ID; }
            set { _Help_Cate_ID = value; }
        }

        public int Help_Cate_ParentID
        {
            get { return _Help_Cate_ParentID; }
            set { _Help_Cate_ParentID = value; }
        }

        public string Help_Cate_Name
        {
            get { return _Help_Cate_Name; }
            set { _Help_Cate_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Help_Cate_Sort
        {
            get { return _Help_Cate_Sort; }
            set { _Help_Cate_Sort = value; }
        }

        public string Help_Cate_Site
        {
            get { return _Help_Cate_Site; }
            set { _Help_Cate_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Help_Cate_SEO_Title
        {
            get { return _Help_Cate_SEO_Title; }
            set { _Help_Cate_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Help_Cate_SEO_Keyword
        {
            get { return _Help_Cate_SEO_Keyword; }
            set { _Help_Cate_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Help_Cate_SEO_Description
        {
            get { return _Help_Cate_SEO_Description; }
            set { _Help_Cate_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        } 

    }

    public class HelpInfo
    {
        private int _Help_ID;
        private int _Help_CateID;
        private int _Help_IsFAQ;
        private int _Help_IsActive;
        private string _Help_Title;
        private string _Help_Content;
        private int _Help_Sort;
        private string _Help_Site;
        private string _Help_SEO_Title;
        private string _Help_SEO_Keyword;
        private string _Help_SEO_Description;

        public string Help_SEO_Title
        {
            get { return _Help_SEO_Title; }
            set { _Help_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Help_SEO_Keyword
        {
            get { return _Help_SEO_Keyword; }
            set { _Help_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Help_SEO_Description
        {
            get { return _Help_SEO_Description; }
            set { _Help_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        } 
        public int Help_ID
        {
            get { return _Help_ID; }
            set { _Help_ID = value; }
        }

        public int Help_CateID
        {
            get { return _Help_CateID; }
            set { _Help_CateID = value; }
        }

        public int Help_IsFAQ
        {
            get { return _Help_IsFAQ; }
            set { _Help_IsFAQ = value; }
        }

        public int Help_IsActive
        {
            get { return _Help_IsActive; }
            set { _Help_IsActive = value; }
        }

        public string Help_Title
        {
            get { return _Help_Title; }
            set { _Help_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Help_Content
        {
            get { return _Help_Content; }
            set { _Help_Content = value; }
        }

        public int Help_Sort
        {
            get { return _Help_Sort; }
            set { _Help_Sort = value; }
        }

        public string Help_Site
        {
            get { return _Help_Site; }
            set { _Help_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
