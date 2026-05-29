using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class Vote : IVote
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Vote()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddVote(VoteInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Vote";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Vote_ID"] = entity.Vote_ID;
            DrAdd["Vote_Name"] = entity.Vote_Name;
            DrAdd["Vote_Source"] = entity.Vote_Source;
            DrAdd["Vote_Start"] = entity.Vote_Start;
            DrAdd["Vote_End"] = entity.Vote_End;
            DrAdd["Vote_IsActive"] = entity.Vote_IsActive;
            DrAdd["Vote_Number"] = entity.Vote_Number;
            DrAdd["Vote_AddTime"] = entity.Vote_AddTime;
            DrAdd["Vote_Remarks"] = entity.Vote_Remarks;
            DrAdd["Vote_SN"] = entity.Vote_SN;
            DrAdd["Vote_Type"] = entity.Vote_Type;

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

        public virtual bool EditVote(VoteInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Vote WHERE Vote_ID = " + entity.Vote_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Vote_ID"] = entity.Vote_ID;
                    DrAdd["Vote_Name"] = entity.Vote_Name;
                    DrAdd["Vote_Source"] = entity.Vote_Source;
                    DrAdd["Vote_Start"] = entity.Vote_Start;
                    DrAdd["Vote_End"] = entity.Vote_End;
                    DrAdd["Vote_IsActive"] = entity.Vote_IsActive;
                    DrAdd["Vote_Number"] = entity.Vote_Number;
                    DrAdd["Vote_AddTime"] = entity.Vote_AddTime;
                    DrAdd["Vote_Remarks"] = entity.Vote_Remarks;
                    DrAdd["Vote_SN"] = entity.Vote_SN;
                    DrAdd["Vote_Type"] = entity.Vote_Type;

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

        public virtual int DelVote(int ID)
        {
            string SqlAdd = "DELETE FROM Vote WHERE Vote_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual VoteInfo GetVoteByID(int ID)
        {
            VoteInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Vote WHERE Vote_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new VoteInfo();

                    entity.Vote_ID = Tools.NullInt(RdrList["Vote_ID"]);
                    entity.Vote_Name = Tools.NullStr(RdrList["Vote_Name"]);
                    entity.Vote_Source = Tools.NullInt(RdrList["Vote_Source"]);
                    entity.Vote_Start = Tools.NullDate(RdrList["Vote_Start"]);
                    entity.Vote_End = Tools.NullDate(RdrList["Vote_End"]);
                    entity.Vote_IsActive = Tools.NullInt(RdrList["Vote_IsActive"]);
                    entity.Vote_Number = Tools.NullInt(RdrList["Vote_Number"]);
                    entity.Vote_AddTime = Tools.NullDate(RdrList["Vote_AddTime"]);
                    entity.Vote_Remarks = Tools.NullStr(RdrList["Vote_Remarks"]);
                    entity.Vote_SN = Tools.NullStr(RdrList["Vote_SN"]);
                    entity.Vote_Type = Tools.NullInt(RdrList["Vote_Type"]);
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

        public virtual VoteInfo GetVoteBySN(string SN)
        {
            VoteInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Vote WHERE Vote_SN = '" + SN+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new VoteInfo();

                    entity.Vote_ID = Tools.NullInt(RdrList["Vote_ID"]);
                    entity.Vote_Name = Tools.NullStr(RdrList["Vote_Name"]);
                    entity.Vote_Source = Tools.NullInt(RdrList["Vote_Source"]);
                    entity.Vote_Start = Tools.NullDate(RdrList["Vote_Start"]);
                    entity.Vote_End = Tools.NullDate(RdrList["Vote_End"]);
                    entity.Vote_IsActive = Tools.NullInt(RdrList["Vote_IsActive"]);
                    entity.Vote_Number = Tools.NullInt(RdrList["Vote_Number"]);
                    entity.Vote_AddTime = Tools.NullDate(RdrList["Vote_AddTime"]);
                    entity.Vote_Remarks = Tools.NullStr(RdrList["Vote_Remarks"]);
                    entity.Vote_SN = Tools.NullStr(RdrList["Vote_SN"]);
                    entity.Vote_Type = Tools.NullInt(RdrList["Vote_Type"]);
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

        public virtual int UpdateVoteNumber(int ID)
        {
            string SqlAdd = "  update Vote set Vote_Number=Vote_Number+1 where Vote_ID= " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<VoteInfo> GetVotes(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<VoteInfo> entitys = null;
            VoteInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Vote";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<VoteInfo>();
                    while (RdrList.Read())
                    {
                        entity = new VoteInfo();
                        entity.Vote_ID = Tools.NullInt(RdrList["Vote_ID"]);
                        entity.Vote_Name = Tools.NullStr(RdrList["Vote_Name"]);
                        entity.Vote_Source = Tools.NullInt(RdrList["Vote_Source"]);
                        entity.Vote_Start = Tools.NullDate(RdrList["Vote_Start"]);
                        entity.Vote_End = Tools.NullDate(RdrList["Vote_End"]);
                        entity.Vote_IsActive = Tools.NullInt(RdrList["Vote_IsActive"]);
                        entity.Vote_Number = Tools.NullInt(RdrList["Vote_Number"]);
                        entity.Vote_AddTime = Tools.NullDate(RdrList["Vote_AddTime"]);
                        entity.Vote_Remarks = Tools.NullStr(RdrList["Vote_Remarks"]);
                        entity.Vote_SN = Tools.NullStr(RdrList["Vote_SN"]);
                        entity.Vote_Type = Tools.NullInt(RdrList["Vote_Type"]);

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

        public virtual PageInfo GetVotePageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Vote";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Vote_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool AddVoteSelect(VoteSelectInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Vote_Select";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Vote_Select_ID"] = entity.Vote_Select_ID;
            DrAdd["Vote_Select_Name"] = entity.Vote_Select_Name;
            DrAdd["Vote_Select_VoteID"] = entity.Vote_Select_VoteID;
            DrAdd["Vote_Select_Number"] = entity.Vote_Select_Number;

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

        public virtual bool EditVoteSelect(VoteSelectInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Vote_Select WHERE Vote_Select_ID = " + entity.Vote_Select_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Vote_Select_ID"] = entity.Vote_Select_ID;
                    DrAdd["Vote_Select_Name"] = entity.Vote_Select_Name;
                    DrAdd["Vote_Select_VoteID"] = entity.Vote_Select_VoteID;
                    DrAdd["Vote_Select_Number"] = entity.Vote_Select_Number;

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

        public virtual int DelVoteSelect(int ID)
        {
            string SqlAdd = "DELETE FROM Vote_Select WHERE Vote_Select_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int UpdateVoteSelectNumber(int ID)
        {
            string SqlAdd = "  update Vote_Select set Vote_Select_Number=Vote_Select_Number+1 where Vote_Select_ID= " + ID;

            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public virtual VoteSelectInfo GetVoteSelectByID(int ID)
        {
            VoteSelectInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Vote_Select WHERE Vote_Select_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new VoteSelectInfo();

                    entity.Vote_Select_ID = Tools.NullInt(RdrList["Vote_Select_ID"]);
                    entity.Vote_Select_Name = Tools.NullStr(RdrList["Vote_Select_Name"]);
                    entity.Vote_Select_VoteID = Tools.NullInt(RdrList["Vote_Select_VoteID"]);
                    entity.Vote_Select_Number = Tools.NullInt(RdrList["Vote_Select_Number"]);

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

        public virtual IList<VoteSelectInfo> GetVoteSelects(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<VoteSelectInfo> entitys = null;
            VoteSelectInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Vote_Select";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<VoteSelectInfo>();
                    while (RdrList.Read())
                    {
                        entity = new VoteSelectInfo();
                        entity.Vote_Select_ID = Tools.NullInt(RdrList["Vote_Select_ID"]);
                        entity.Vote_Select_Name = Tools.NullStr(RdrList["Vote_Select_Name"]);
                        entity.Vote_Select_VoteID = Tools.NullInt(RdrList["Vote_Select_VoteID"]);
                        entity.Vote_Select_Number = Tools.NullInt(RdrList["Vote_Select_Number"]);

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

        public virtual IList<VoteSelectInfo> GetVoteSelectsByVoteID(int ID)
        {

            IList<VoteSelectInfo> entitys = null;
            VoteSelectInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {

                SqlList = "SELECT * FROM Vote_Select WHERE Vote_Select_VoteID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<VoteSelectInfo>();
                    while (RdrList.Read())
                    {
                        entity = new VoteSelectInfo();
                        entity.Vote_Select_ID = Tools.NullInt(RdrList["Vote_Select_ID"]);
                        entity.Vote_Select_Name = Tools.NullStr(RdrList["Vote_Select_Name"]);
                        entity.Vote_Select_VoteID = Tools.NullInt(RdrList["Vote_Select_VoteID"]);
                        entity.Vote_Select_Number = Tools.NullInt(RdrList["Vote_Select_Number"]);

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

        public virtual bool AddVoteMember(VoteMemberInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Vote_Member";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Vote_Member_ID"] = entity.Vote_Member_ID;
            DrAdd["Vote_Member_VoteID"] = entity.Vote_Member_VoteID;
            DrAdd["Vote_Member_VoteSelectID"] = entity.Vote_Member_VoteSelectID;
            DrAdd["Vote_Member_MemberID"] = entity.Vote_Member_MemberID;
            DrAdd["Vote_Member_AddTime"] = entity.Vote_Member_AddTime;

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

        public virtual bool EditVoteMember(VoteMemberInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Vote_Member WHERE Vote_Member_ID = " + entity.Vote_Member_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Vote_Member_ID"] = entity.Vote_Member_ID;
                    DrAdd["Vote_Member_VoteID"] = entity.Vote_Member_VoteID;
                    DrAdd["Vote_Member_VoteSelectID"] = entity.Vote_Member_VoteSelectID;
                    DrAdd["Vote_Member_MemberID"] = entity.Vote_Member_MemberID;
                    DrAdd["Vote_Member_AddTime"] = entity.Vote_Member_AddTime;

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

        public virtual int DelVoteMember(int ID)
        {
            string SqlAdd = "DELETE FROM Vote_Member WHERE Vote_Member_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual VoteMemberInfo GetVoteMemberByID(int ID)
        {
            VoteMemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Vote_Member WHERE Vote_Member_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new VoteMemberInfo();

                    entity.Vote_Member_ID = Tools.NullInt(RdrList["Vote_Member_ID"]);
                    entity.Vote_Member_VoteID = Tools.NullInt(RdrList["Vote_Member_VoteID"]);
                    entity.Vote_Member_VoteSelectID = Tools.NullInt(RdrList["Vote_Member_VoteSelectID"]);
                    entity.Vote_Member_MemberID = Tools.NullInt(RdrList["Vote_Member_MemberID"]);
                    entity.Vote_Member_AddTime = Tools.NullDate(RdrList["Vote_Member_AddTime"]);

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

        public virtual IList<VoteMemberInfo> GetVoteMembers(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<VoteMemberInfo> entitys = null;
            VoteMemberInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Vote_Member";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<VoteMemberInfo>();
                    while (RdrList.Read())
                    {
                        entity = new VoteMemberInfo();
                        entity.Vote_Member_ID = Tools.NullInt(RdrList["Vote_Member_ID"]);
                        entity.Vote_Member_VoteID = Tools.NullInt(RdrList["Vote_Member_VoteID"]);
                        entity.Vote_Member_VoteSelectID = Tools.NullInt(RdrList["Vote_Member_VoteSelectID"]);
                        entity.Vote_Member_MemberID = Tools.NullInt(RdrList["Vote_Member_MemberID"]);
                        entity.Vote_Member_AddTime = Tools.NullDate(RdrList["Vote_Member_AddTime"]);

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

        public virtual PageInfo GetVoteMemberPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Vote_Member";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Vote_Member_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
