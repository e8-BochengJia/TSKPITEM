using System;

namespace Glaer.Trade.B2C.Model
{
    public class AboutInfo
    {
        private int _About_ID;
        private int _About_IsActive;
        private string _About_Title;
        private string _About_Sign;
        private string _About_Content;
        private int _About_Sort;
        private string _About_Site;
        private int _About_IsTop;
        private string _About_SEO_Title;
        private string _About_SEO_Keyword;
        private string _About_SEO_Description;

        public string About_SEO_Title
        {
            get { return _About_SEO_Title; }
            set { _About_SEO_Title = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string About_SEO_Keyword
        {
            get { return _About_SEO_Keyword; }
            set { _About_SEO_Keyword = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string About_SEO_Description
        {
            get { return _About_SEO_Description; }
            set { _About_SEO_Description = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        } 

        public int About_IsTop
        {
            get { return _About_IsTop; }
            set { _About_IsTop = value; }
        } 

        public int About_ID
        {
            get { return _About_ID; }
            set { _About_ID = value; }
        }

        public int About_IsActive
        {
            get { return _About_IsActive; }
            set { _About_IsActive = value; }
        }

        public string About_Title
        {
            get { return _About_Title; }
            set { _About_Title = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string About_Sign
        {
            get { return _About_Sign; }
            set { _About_Sign = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string About_Content
        {
            get { return _About_Content; }
            set { _About_Content = value; }
        }

        public int About_Sort
        {
            get { return _About_Sort; }
            set { _About_Sort = value; }
        }

        public string About_Site
        {
            get { return _About_Site; }
            set { _About_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }
}
