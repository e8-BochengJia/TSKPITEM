using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class QuestionInfo
    {
        private int _ID;
        private int _Q_Cate;
        private string _Q_Question;
        private string _Q_Option_A;
        private string _Q_Option_B;
        private string _Q_Option_C;
        private string _Q_Option_D;
        private string _Q_Answer;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int Q_Cate
        {
            get { return _Q_Cate; }
            set { _Q_Cate = value; }
        }

        public string Q_Question
        {
            get { return _Q_Question; }
            set { _Q_Question = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string Q_Option_A
        {
            get { return _Q_Option_A; }
            set { _Q_Option_A = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Q_Option_B
        {
            get { return _Q_Option_B; }
            set { _Q_Option_B = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Q_Option_C
        {
            get { return _Q_Option_C; }
            set { _Q_Option_C = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Q_Option_D
        {
            get { return _Q_Option_D; }
            set { _Q_Option_D = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Q_Answer
        {
            get { return _Q_Answer; }
            set { _Q_Answer = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

    }
}
