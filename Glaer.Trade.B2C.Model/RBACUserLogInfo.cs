using System;
using System.Collections.Generic;

namespace Glaer.Trade.B2C.Model
{
    public class RBACUserLogInfo
    {
        private int _Log_ID;
        private int _Log_Channel;
        private int _Log_UserID;
        private string _Log_UserName;
        private string _Log_User_ObjectID;
        private string _Log_Action;
        private string _Log_Description;
        private int _Log_Result;
        private string _Log_IP;
        private DateTime _Log_Addtime;
        private string _Log_Site;

        public int Log_ID
        {
            get { return _Log_ID; }
            set { _Log_ID = value; }
        }

        public int Log_Channel
        {
            get { return _Log_Channel; }
            set { _Log_Channel = value; }
        }

        public int Log_UserID
        {
            get { return _Log_UserID; }
            set { _Log_UserID = value; }
        }

        public string Log_UserName
        {
            get { return _Log_UserName; }
            set { _Log_UserName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Log_User_ObjectID
        {
            get { return _Log_User_ObjectID; }
            set { _Log_User_ObjectID = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public string Log_Action
        {
            get { return _Log_Action; }
            set { _Log_Action = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Log_Description
        {
            get { return _Log_Description; }
            set { _Log_Description = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int Log_Result
        {
            get { return _Log_Result; }
            set { _Log_Result = value; }
        }

        public string Log_IP
        {
            get { return _Log_IP; }
            set { _Log_IP = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public DateTime Log_Addtime
        {
            get { return _Log_Addtime; }
            set { _Log_Addtime = value; }
        }

        public string Log_Site
        {
            get { return _Log_Site; }
            set { _Log_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

    }

    public class RBACUserLogChannelInfo
    {
        private int _Log_Channel_ID;
        private string _Log_Channel_Name;
        private int _Log_Channel_ParentID;
        private int _Log_Channel_Type;

        public int Log_Channel_ID
        {
            get { return _Log_Channel_ID; }
            set { _Log_Channel_ID = value; }
        }

        public string Log_Channel_Name
        {
            get { return _Log_Channel_Name; }
            set { _Log_Channel_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Log_Channel_ParentID
        {
            get { return _Log_Channel_ParentID; }
            set { _Log_Channel_ParentID = value; }
        }

        public int Log_Channel_Type
        {
            get { return _Log_Channel_Type; }
            set { _Log_Channel_Type = value; }
        }

    }
}


