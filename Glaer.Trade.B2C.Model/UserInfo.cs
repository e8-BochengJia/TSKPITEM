using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class UserInfo
    {
        private int _User_ID;
        private int _User_Type;
        private string _User_Province;
        private string _User_Name;
        private string _User_Password;
        private int _User_AddPower;
        private int _User_EditPower;
        private int _User_DelPower;
        private int _User_AuditPower;
        private DateTime _User_AddTime;

        public int User_ID
        {
            get { return _User_ID; }
            set { _User_ID = value; }
        }

        public int User_Type
        {
            get { return _User_Type; }
            set { _User_Type = value; }
        }

        public string User_Province
        {
            get { return _User_Province; }
            set { _User_Province = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string User_Name
        {
            get { return _User_Name; }
            set { _User_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string User_Password
        {
            get { return _User_Password; }
            set { _User_Password = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int User_AddPower
        {
            get { return _User_AddPower; }
            set { _User_AddPower = value; }
        }

        public int User_EditPower
        {
            get { return _User_EditPower; }
            set { _User_EditPower = value; }
        }

        public int User_DelPower
        {
            get { return _User_DelPower; }
            set { _User_DelPower = value; }
        }

        public int User_AuditPower
        {
            get { return _User_AuditPower; }
            set { _User_AuditPower = value; }
        }

        public DateTime User_AddTime
        {
            get { return _User_AddTime; }
            set { _User_AddTime = value; }
        }

    }
}
