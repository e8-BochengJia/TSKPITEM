using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class QuestionHistoryInfo
    {
        private int _ID;
        private string _Q;
        private int _Q_Hit;
        private DateTime _Q_AddDate;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Q
        {
            get { return _Q; }
            set { _Q = value.Length > 500 ? value.Substring(0, 500) : value.ToString(); }
        }

        public int Q_Hit
        {
            get { return _Q_Hit; }
            set { _Q_Hit = value; }
        }

        public DateTime Q_AddDate
        {
            get { return _Q_AddDate; }
            set { _Q_AddDate = value; }
        }

    }
}
