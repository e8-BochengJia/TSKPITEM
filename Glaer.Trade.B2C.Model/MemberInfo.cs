using System;

namespace Glaer.Trade.B2C.Model
{
    /// <summary>
    /// 会员信息实体
    /// </summary>
    public class MemberInfo
    {
        private int _Member_ID;
        private string _Member_Email;
        private int _Member_Emailverify;
        private string _Member_LoginMobile;
        private int _Member_LoginMobileverify;
        private string _Member_NickName;
        private string _Member_Password;
        private string _Member_VerifyCode;
        private int _Member_LoginCount;
        private string _Member_LastLogin_IP;
        private DateTime _Member_LastLogin_Time;
        private int _Member_CoinCount;
        private int _Member_CoinRemain;
        private DateTime _Member_Addtime;
        private int _Member_Trash;
        private int _Member_Grade;
        private double _Member_Account;
        private double _Member_Frozen;
        private int _Member_AllowSysEmail;
        private int _Member_AllowSysMobile;
        private string _Member_Site;
        private string _Member_Source;
        private string _U_Member_QQ;
        private string _U_Member_MSN;
        private string _U_Member_Question;
        private string _U_Member_Answer;
        private int _U_Member_Male;
        private DateTime _U_MeMber_Birth;
        private string _U_Member_Bloodtype;
        private string _U_Member_Realname;
        private string _U_Member_Country;
        private string _U_Member_Province;
        private string _U_Member_City;
        private string _U_Member_Address;
        private string _U_Member_Job;
        private string _U_Member_Postcode;
        private string _U_Member_School;
        private string _U_Member_Edu;
        private string _U_Member_IDCard;
        private int _U_Member_Mark;
        private int _U_Member_Article_Commend;
        private int _U_Member_State;
        private string _U_Member_OpenID;
        public int Member_ID
        {
            get { return _Member_ID; }
            set { _Member_ID = value; }
        }

        public string Member_Email
        {
            get { return _Member_Email; }
            set { _Member_Email = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public int Member_Emailverify
        {
            get { return _Member_Emailverify; }
            set { _Member_Emailverify = value; }
        }

        public string Member_LoginMobile
        {
            get { return _Member_LoginMobile; }
            set { _Member_LoginMobile = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Member_LoginMobileverify
        {
            get { return _Member_LoginMobileverify; }
            set { _Member_LoginMobileverify = value; }
        }

        public string Member_NickName
        {
            get { return _Member_NickName; }
            set { _Member_NickName = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Password
        {
            get { return _Member_Password; }
            set { _Member_Password = value.Length > 64 ? value.Substring(0, 64) : value.ToString(); }
        }

        public string Member_VerifyCode
        {
            get { return _Member_VerifyCode; }
            set { _Member_VerifyCode = value.Length > 128 ? value.Substring(0, 128) : value.ToString(); }
        }

        public int Member_LoginCount
        {
            get { return _Member_LoginCount; }
            set { _Member_LoginCount = value; }
        }

        public string Member_LastLogin_IP
        {
            get { return _Member_LastLogin_IP; }
            set { _Member_LastLogin_IP = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public DateTime Member_LastLogin_Time
        {
            get { return _Member_LastLogin_Time; }
            set { _Member_LastLogin_Time = value; }
        }

        public int Member_CoinCount
        {
            get { return _Member_CoinCount; }
            set { _Member_CoinCount = value; }
        }

        public int Member_CoinRemain
        {
            get { return _Member_CoinRemain; }
            set { _Member_CoinRemain = value; }
        }

        public DateTime Member_Addtime
        {
            get { return _Member_Addtime; }
            set { _Member_Addtime = value; }
        }
        public string U_Member_Edu
        {
            get { return _U_Member_Edu; }
            set { _U_Member_Edu = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        } 
        public int Member_Trash
        {
            get { return _Member_Trash; }
            set { _Member_Trash = value; }
        }

        public int Member_Grade
        {
            get { return _Member_Grade; }
            set { _Member_Grade = value; }
        }

        public double Member_Account
        {
            get { return _Member_Account; }
            set { _Member_Account = value; }
        }

        public double Member_Frozen
        {
            get { return _Member_Frozen; }
            set { _Member_Frozen = value; }
        }

        public int Member_AllowSysEmail
        {
            get { return _Member_AllowSysEmail; }
            set { _Member_AllowSysEmail = value; }
        }

        public int Member_AllowSysMobile
        {
            get { return _Member_AllowSysMobile; }
            set { _Member_AllowSysMobile = value; }
        }

        public string Member_Site
        {
            get { return _Member_Site; }
            set { _Member_Site = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string Member_Source
        {
            get { return _Member_Source; }
            set { _Member_Source = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public string U_Member_QQ
        {
            get { return _U_Member_QQ; }
            set { _U_Member_QQ = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_MSN
        {
            get { return _U_Member_MSN; }
            set { _U_Member_MSN = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_Question
        {
            get { return _U_Member_Question; }
            set { _U_Member_Question = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string U_Member_Answer
        {
            get { return _U_Member_Answer; }
            set { _U_Member_Answer = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public int U_Member_Male
        {
            get { return _U_Member_Male; }
            set { _U_Member_Male = value; }
        }


        public DateTime U_MeMber_Birth
        {
            get { return _U_MeMber_Birth; }
            set { _U_MeMber_Birth = value; }
        }

        public string U_Member_Bloodtype
        {
            get { return _U_Member_Bloodtype; }
            set { _U_Member_Bloodtype = value.Length > 4 ? value.Substring(0, 4) : value.ToString(); }
        }

        public string U_Member_Realname
        {
            get { return _U_Member_Realname; }
            set { _U_Member_Realname = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_Country
        {
            get { return _U_Member_Country; }
            set { _U_Member_Country = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_Province
        {
            get { return _U_Member_Province; }
            set { _U_Member_Province = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_City
        {
            get { return _U_Member_City; }
            set { _U_Member_City = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_Address
        {
            get { return _U_Member_Address; }
            set { _U_Member_Address = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_Job
        {
            get { return _U_Member_Job; }
            set { _U_Member_Job = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public string U_Member_Postcode
        {
            get { return _U_Member_Postcode; }
            set { _U_Member_Postcode = value.Length > 10 ? value.Substring(0, 10) : value.ToString(); }
        }

        public string U_Member_School
        {
            get { return _U_Member_School; }
            set { _U_Member_School = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string U_Member_IDCard
        {
            get { return _U_Member_IDCard; }
            set { _U_Member_IDCard = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int U_Member_Mark
        {
            get { return _U_Member_Mark; }
            set { _U_Member_Mark = value; }
        }

        public int U_Member_Article_Commend
        {
            get { return _U_Member_Article_Commend; }
            set { _U_Member_Article_Commend = value; }
        }

        public int U_Member_State
        {
            get { return _U_Member_State; }
            set { _U_Member_State = value; }
        }

        public string U_Member_OpenID
        {
            get { return _U_Member_OpenID; }
            set { _U_Member_OpenID = value.Length > 150 ? value.Substring(0, 150) : value.ToString(); }
        } 

    }

    public class MemberLogInfo
    {
        private int _Log_ID;
        private int _Log_Member_ID;
        private string _Log_Member_Name;
        private int _Log_Member_Result;
        private string _Log_Member_Action;
        private DateTime _Log_Addtime;

        public int Log_ID
        {
            get { return _Log_ID; }
            set { _Log_ID = value; }
        }

        public int Log_Member_ID
        {
            get { return _Log_Member_ID; }
            set { _Log_Member_ID = value; }
        }

        public string Log_Member_Name
        {
            get { return _Log_Member_Name; }
            set { _Log_Member_Name = value; }
        }

        public int Log_Member_Result
        {
            get { return _Log_Member_Result; }
            set { _Log_Member_Result = value; }
        }

        public string Log_Member_Action
        {
            get { return _Log_Member_Action; }
            set { _Log_Member_Action = value.Length > 100 ? value.Substring(0, 100) : value.ToString(); }
        }

        public DateTime Log_Addtime
        {
            get { return _Log_Addtime; }
            set { _Log_Addtime = value; }
        }

    }
}
