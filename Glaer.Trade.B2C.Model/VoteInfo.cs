using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    public class VoteInfo
    {
        private int _Vote_ID;
        private string _Vote_Name;
        private int _Vote_Source;
        private DateTime _Vote_Start;
        private DateTime _Vote_End;
        private int _Vote_IsActive;
        private int _Vote_Number;
        private DateTime _Vote_AddTime;
        private string _Vote_Remarks;
        private string _Vote_SN;
        private int _Vote_Type;

        public int Vote_ID
        {
            get { return _Vote_ID; }
            set { _Vote_ID = value; }
        }

        public string Vote_Name
        {
            get { return _Vote_Name; }
            set { _Vote_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Vote_Source
        {
            get { return _Vote_Source; }
            set { _Vote_Source = value; }
        }

        public DateTime Vote_Start
        {
            get { return _Vote_Start; }
            set { _Vote_Start = value; }
        }

        public DateTime Vote_End
        {
            get { return _Vote_End; }
            set { _Vote_End = value; }
        }

        public int Vote_IsActive
        {
            get { return _Vote_IsActive; }
            set { _Vote_IsActive = value; }
        }

        public int Vote_Number
        {
            get { return _Vote_Number; }
            set { _Vote_Number = value; }
        }

        public DateTime Vote_AddTime
        {
            get { return _Vote_AddTime; }
            set { _Vote_AddTime = value; }
        }

        public string Vote_Remarks
        {
            get { return _Vote_Remarks; }
            set { _Vote_Remarks = value.Length > 200 ? value.Substring(0, 200) : value.ToString(); }
        }

        public string Vote_SN
        {
            get { return _Vote_SN; }
            set { _Vote_SN = value.Length > 20 ? value.Substring(0, 20) : value.ToString(); }
        }

        public int Vote_Type
        {
            get { return _Vote_Type; }
            set { _Vote_Type = value; }
        }

    }

    public class VoteSelectInfo
    {
        private int _Vote_Select_ID;
        private string _Vote_Select_Name;
        private int _Vote_Select_VoteID;
        private int _Vote_Select_Number;

        public int Vote_Select_ID
        {
            get { return _Vote_Select_ID; }
            set { _Vote_Select_ID = value; }
        }

        public string Vote_Select_Name
        {
            get { return _Vote_Select_Name; }
            set { _Vote_Select_Name = value.Length > 50 ? value.Substring(0, 50) : value.ToString(); }
        }

        public int Vote_Select_VoteID
        {
            get { return _Vote_Select_VoteID; }
            set { _Vote_Select_VoteID = value; }
        }

        public int Vote_Select_Number
        {
            get { return _Vote_Select_Number; }
            set { _Vote_Select_Number = value; }
        }

    }

    public class VoteMemberInfo
    {
        private int _Vote_Member_ID;
        private int _Vote_Member_VoteID;
        private int _Vote_Member_VoteSelectID;
        private int _Vote_Member_MemberID;
        private DateTime _Vote_Member_AddTime;

        public int Vote_Member_ID
        {
            get { return _Vote_Member_ID; }
            set { _Vote_Member_ID = value; }
        }

        public int Vote_Member_VoteID
        {
            get { return _Vote_Member_VoteID; }
            set { _Vote_Member_VoteID = value; }
        }

        public int Vote_Member_VoteSelectID
        {
            get { return _Vote_Member_VoteSelectID; }
            set { _Vote_Member_VoteSelectID = value; }
        }

        public int Vote_Member_MemberID
        {
            get { return _Vote_Member_MemberID; }
            set { _Vote_Member_MemberID = value; }
        }

        public DateTime Vote_Member_AddTime
        {
            get { return _Vote_Member_AddTime; }
            set { _Vote_Member_AddTime = value; }
        }

    }
}
