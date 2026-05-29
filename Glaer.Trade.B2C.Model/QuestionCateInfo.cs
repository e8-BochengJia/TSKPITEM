using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class QuestionCateInfo
    {
        private int _ID;
        private string _Q_Cate_Name;
        private int _Q_Cate_Valid;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Q_Cate_Name
        {
            get { return _Q_Cate_Name; }
            set { _Q_Cate_Name = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Q_Cate_Valid
        {
            get { return _Q_Cate_Valid; }
            set { _Q_Cate_Valid = value; }
        }

    }
}
