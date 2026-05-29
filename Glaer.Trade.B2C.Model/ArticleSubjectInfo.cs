using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class ArticleSubjectInfo
    {
        private int _Subject_ID;
        private string _Subject_Name;
        private string _Subject_Img;
        private int _Subject_IsActive;
        private int _Subject_Sort;
        private string _Subject_Site;

        public int Subject_ID
        {
            get { return _Subject_ID; }
            set { _Subject_ID = value; }
        }

        public string Subject_Name
        {
            get { return _Subject_Name; }
            set { _Subject_Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Subject_Img
        {
            get { return _Subject_Img; }
            set { _Subject_Img = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Subject_IsActive
        {
            get { return _Subject_IsActive; }
            set { _Subject_IsActive = value; }
        }

        public int Subject_Sort
        {
            get { return _Subject_Sort; }
            set { _Subject_Sort = value; }
        }

        public string Subject_Site
        {
            get { return _Subject_Site; }
            set { _Subject_Site = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

    }
}
