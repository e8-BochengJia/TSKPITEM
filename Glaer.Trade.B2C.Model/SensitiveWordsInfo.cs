using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class SensitiveWordsInfo
    {
        private int _ID;
        private string _Name;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

    }
}
