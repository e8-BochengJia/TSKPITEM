using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class Member : IMember
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Member()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMember(MemberInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_ID"] = entity.Member_ID;
            DrAdd["Member_Email"] = entity.Member_Email;
            DrAdd["Member_Emailverify"] = entity.Member_Emailverify;
            DrAdd["Member_LoginMobile"] = entity.Member_LoginMobile;
            DrAdd["Member_LoginMobileverify"] = entity.Member_LoginMobileverify;
            DrAdd["Member_NickName"] = entity.Member_NickName;
            DrAdd["Member_Password"] = entity.Member_Password;
            DrAdd["Member_VerifyCode"] = entity.Member_VerifyCode;
            DrAdd["Member_LoginCount"] = entity.Member_LoginCount;
            DrAdd["Member_LastLogin_IP"] = entity.Member_LastLogin_IP;
            DrAdd["Member_LastLogin_Time"] = entity.Member_LastLogin_Time;
            DrAdd["Member_CoinCount"] = entity.Member_CoinCount;
            DrAdd["Member_CoinRemain"] = entity.Member_CoinRemain;
            DrAdd["Member_Addtime"] = entity.Member_Addtime;
            DrAdd["Member_Trash"] = entity.Member_Trash;
            DrAdd["Member_Grade"] = entity.Member_Grade;
            DrAdd["Member_Account"] = entity.Member_Account;
            DrAdd["Member_Frozen"] = entity.Member_Frozen;
            DrAdd["Member_AllowSysEmail"] = entity.Member_AllowSysEmail;
            DrAdd["Member_AllowSysMobile"] = entity.Member_AllowSysMobile;
            DrAdd["Member_Site"] = entity.Member_Site;
            DrAdd["Member_Source"] = entity.Member_Source;
            DrAdd["U_Member_QQ"] = entity.U_Member_QQ;
            DrAdd["U_Member_MSN"] = entity.U_Member_MSN;
            DrAdd["U_Member_Question"] = entity.U_Member_Question;
            DrAdd["U_Member_Answer"] = entity.U_Member_Answer;
            DrAdd["U_Member_Male"] = entity.U_Member_Male;
            DrAdd["U_MeMber_Birth"] = entity.U_MeMber_Birth;
            DrAdd["U_Member_Bloodtype"] = entity.U_Member_Bloodtype;
            DrAdd["U_Member_Realname"] = entity.U_Member_Realname;
            DrAdd["U_Member_Country"] = entity.U_Member_Country;
            DrAdd["U_Member_Province"] = entity.U_Member_Province;
            DrAdd["U_Member_City"] = entity.U_Member_City;
            DrAdd["U_Member_Address"] = entity.U_Member_Address;
            DrAdd["U_Member_Job"] = entity.U_Member_Job;
            DrAdd["U_Member_Postcode"] = entity.U_Member_Postcode;
            DrAdd["U_Member_Edu"] = entity.U_Member_Edu;
            DrAdd["U_Member_School"] = entity.U_Member_School;
            DrAdd["U_Member_IDCard"] = entity.U_Member_IDCard;
            DrAdd["U_Member_Mark"] = entity.U_Member_Mark;
            DrAdd["U_Member_Article_Commend"] = entity.U_Member_Article_Commend;
            DrAdd["U_Member_State"] = entity.U_Member_State;
            DrAdd["U_Member_OpenID"] = entity.U_Member_OpenID;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditMember(MemberInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member WHERE Member_ID = " + entity.Member_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_ID"] = entity.Member_ID;
                    DrAdd["Member_Email"] = entity.Member_Email;
                    DrAdd["Member_Emailverify"] = entity.Member_Emailverify;
                    DrAdd["Member_LoginMobile"] = entity.Member_LoginMobile;
                    DrAdd["Member_LoginMobileverify"] = entity.Member_LoginMobileverify;
                    DrAdd["Member_NickName"] = entity.Member_NickName;
                    DrAdd["Member_Password"] = entity.Member_Password;
                    DrAdd["Member_VerifyCode"] = entity.Member_VerifyCode;
                    DrAdd["Member_LoginCount"] = entity.Member_LoginCount;
                    DrAdd["Member_LastLogin_IP"] = entity.Member_LastLogin_IP;
                    DrAdd["Member_LastLogin_Time"] = entity.Member_LastLogin_Time;
                    DrAdd["Member_CoinCount"] = entity.Member_CoinCount;
                    DrAdd["Member_CoinRemain"] = entity.Member_CoinRemain;
                    DrAdd["Member_Addtime"] = entity.Member_Addtime;
                    DrAdd["Member_Trash"] = entity.Member_Trash;
                    DrAdd["Member_Grade"] = entity.Member_Grade;
                    DrAdd["Member_Account"] = entity.Member_Account;
                    DrAdd["Member_Frozen"] = entity.Member_Frozen;
                    DrAdd["Member_AllowSysEmail"] = entity.Member_AllowSysEmail;
                    DrAdd["Member_AllowSysMobile"] = entity.Member_AllowSysMobile;
                    DrAdd["Member_Site"] = entity.Member_Site;
                    DrAdd["Member_Source"] = entity.Member_Source;
                    DrAdd["U_Member_QQ"] = entity.U_Member_QQ;
                    DrAdd["U_Member_MSN"] = entity.U_Member_MSN;
                    DrAdd["U_Member_Question"] = entity.U_Member_Question;
                    DrAdd["U_Member_Answer"] = entity.U_Member_Answer;
                    DrAdd["U_Member_Male"] = entity.U_Member_Male;
                    DrAdd["U_MeMber_Birth"] = entity.U_MeMber_Birth;
                    DrAdd["U_Member_Bloodtype"] = entity.U_Member_Bloodtype;
                    DrAdd["U_Member_Realname"] = entity.U_Member_Realname;
                    DrAdd["U_Member_Country"] = entity.U_Member_Country;
                    DrAdd["U_Member_Province"] = entity.U_Member_Province;
                    DrAdd["U_Member_City"] = entity.U_Member_City;
                    DrAdd["U_Member_Address"] = entity.U_Member_Address;
                    DrAdd["U_Member_Job"] = entity.U_Member_Job;
                    DrAdd["U_Member_Postcode"] = entity.U_Member_Postcode;
                    DrAdd["U_Member_Edu"] = entity.U_Member_Edu;
                    DrAdd["U_Member_School"] = entity.U_Member_School;
                    DrAdd["U_Member_IDCard"] = entity.U_Member_IDCard;
                    DrAdd["U_Member_Mark"] = entity.U_Member_Mark;
                    DrAdd["U_Member_Article_Commend"] = entity.U_Member_Article_Commend;
                    DrAdd["U_Member_State"] = entity.U_Member_State;
                    DrAdd["U_Member_OpenID"] = entity.U_Member_OpenID;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual bool UpdateMemberLogin(int Member_ID,int Count,string Remote_IP)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member WHERE Member_ID = " + Member_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_LoginCount"] = Count;
                    DrAdd["Member_LastLogin_IP"] = Remote_IP;
                    DrAdd["Member_LastLogin_Time"] = DateTime.Now;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual int DelMember(int ID)
        {
            string SqlAdd = "DELETE FROM Member WHERE Member_ID = " + ID;
            try { return DBHelper.ExecuteNonQuery(SqlAdd); }
            catch (Exception ex) { throw ex; }
        }

        public virtual MemberInfo GetMemberByID(int ID)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new MemberInfo();

                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                    entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                    entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                    entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                    entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                    entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                    entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                    entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                    entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                    entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                    entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                    entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                    entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                    entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                    entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                    entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                    entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                    entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                    entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                    entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                    entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                }
                RdrList.Close();
                RdrList = null;
              
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual MemberInfo GetMemberByOpenID(string openid)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE U_Member_OpenID = '" + openid + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                    entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                    entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                    entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                    entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                    entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                    entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                    entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                    entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                    entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                    entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                    entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                    entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                    entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                    entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                    entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                    entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                    entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                    entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                    entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                    entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                }
                RdrList.Close();
                RdrList = null;
             
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual MemberInfo GetMemberByEmail(string email)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_Email = '" + email + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                    entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                    entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                    entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                    entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                    entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                    entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                    entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                    entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                    entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                    entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                    entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                    entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                    entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                    entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                    entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                    entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                    entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                    entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                    entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                    entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                }

               
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual MemberInfo GetMemberByNickName(string NickName)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_NickName = '" + NickName + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                    entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                    entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                    entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                    entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                    entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                    entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                    entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                    entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                    entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                    entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                    entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                    entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                    entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                    entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                    entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                    entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                    entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                    entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                    entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                    entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                }

               
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual MemberInfo Member_Login(string member_name)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_NickName = '" + member_name + "' OR Member_Email='" + member_name + "' OR Member_LoginMobile='" + member_name + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                    entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                    entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                    entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                    entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                    entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                    entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                    entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                    entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                    entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                    entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                    entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                    entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                    entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                    entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                    entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                    entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                    entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                    entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                    entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                    entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                }

              
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual MemberInfo GetMemberByLogin(string nickname,string password)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE (Member_NickName = '" + nickname + "'  OR Member_Email='" + nickname + "' OR Member_LoginMobile='" + nickname + "') and Member_Password='" + password + "' and Member_Trash=0";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                    entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                    entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                    entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                    entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                    entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                    entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                    entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                    entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                    entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                    entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                    entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                    entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                    entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                    entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                    entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                    entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                    entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                    entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                    entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                    entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                }
                RdrList.Close();
                RdrList = null;
              
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<MemberInfo> GetMembers(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberInfo> entitys = null;
            MemberInfo entity = null;
          
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<MemberInfo>();
                    while (RdrList.Read()) {
                        entity = new MemberInfo();
                        entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                        entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                        entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                        entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                        entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                        entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                        entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                        entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                        entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                        entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                        entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                        entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                        entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                        entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                        entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                        entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                        entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                        entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                        entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                        entity.Member_AllowSysMobile = Tools.NullInt(RdrList["Member_AllowSysMobile"]);
                        entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                        entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                        entity.U_Member_QQ = Tools.NullStr(RdrList["U_Member_QQ"]);
                        entity.U_Member_MSN = Tools.NullStr(RdrList["U_Member_MSN"]);
                        entity.U_Member_Question = Tools.NullStr(RdrList["U_Member_Question"]);
                        entity.U_Member_Answer = Tools.NullStr(RdrList["U_Member_Answer"]);
                        entity.U_Member_Male = Tools.NullInt(RdrList["U_Member_Male"]);
                        entity.U_MeMber_Birth = Tools.NullDate(RdrList["U_MeMber_Birth"]);
                        entity.U_Member_Bloodtype = Tools.NullStr(RdrList["U_Member_Bloodtype"]);
                        entity.U_Member_Realname = Tools.NullStr(RdrList["U_Member_Realname"]);
                        entity.U_Member_Country = Tools.NullStr(RdrList["U_Member_Country"]);
                        entity.U_Member_Province = Tools.NullStr(RdrList["U_Member_Province"]);
                        entity.U_Member_City = Tools.NullStr(RdrList["U_Member_City"]);
                        entity.U_Member_Address = Tools.NullStr(RdrList["U_Member_Address"]);
                        entity.U_Member_Job = Tools.NullStr(RdrList["U_Member_Job"]);
                        entity.U_Member_Postcode = Tools.NullStr(RdrList["U_Member_Postcode"]);
                        entity.U_Member_Edu = Tools.NullStr(RdrList["U_Member_Edu"]);
                        entity.U_Member_School = Tools.NullStr(RdrList["U_Member_School"]);
                        entity.U_Member_IDCard = Tools.NullStr(RdrList["U_Member_IDCard"]);
                        entity.U_Member_Mark = Tools.NullInt(RdrList["U_Member_Mark"]);
                        entity.U_Member_Article_Commend = Tools.NullInt(RdrList["U_Member_Article_Commend"]);
                        entity.U_Member_State = Tools.NullInt(RdrList["U_Member_State"]);
                        entity.U_Member_OpenID = Tools.NullStr(RdrList["U_Member_OpenID"]);
                        entitys.Add(entity);
                        entity = null;
                     
                    }
                }
                RdrList.Close();
                RdrList = null;
                

             
                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try {
                Page = new PageInfo();
                SqlTable = "Member";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

      

    }

    public class MemberLog : IMemberLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberLog(MemberLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Log_ID"] = entity.Log_ID;
            DrAdd["Log_Member_ID"] = entity.Log_Member_ID;
            DrAdd["Log_Member_Name"] = entity.Log_Member_Name;
            DrAdd["Log_Member_Result"] = entity.Log_Member_Result;
            DrAdd["Log_Member_Action"] = entity.Log_Member_Action;
            DrAdd["Log_Addtime"] = entity.Log_Addtime;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        public virtual int DelMemberLog(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Log WHERE Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<MemberLogInfo> GetMemberLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberLogInfo> entitys = null;
            MemberLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberLogInfo();
                        entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                        entity.Log_Member_ID = Tools.NullInt(RdrList["Log_Member_ID"]);
                        entity.Log_Member_Action = Tools.NullStr(RdrList["Log_Member_Action"]);

                        entity.Log_Member_Name=Tools.NullStr(RdrList["Log_Member_Name"]);
                         entity.Log_Member_Result=Tools.NullInt(RdrList["Log_Member_Result"]);
                        entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RdrList != null)
                {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }


    }


}
