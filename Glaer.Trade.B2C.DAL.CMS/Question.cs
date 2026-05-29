using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.CMS
{


    public class Question : IQuestion
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Question()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddQuestion(QuestionInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Question";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["Q_Cate"] = entity.Q_Cate;
            DrAdd["Q_Question"] = entity.Q_Question;
            DrAdd["Q_Option_A"] = entity.Q_Option_A;
            DrAdd["Q_Option_B"] = entity.Q_Option_B;
            DrAdd["Q_Option_C"] = entity.Q_Option_C;
            DrAdd["Q_Option_D"] = entity.Q_Option_D;
            DrAdd["Q_Answer"] = entity.Q_Answer;

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

        public virtual bool EditQuestion(QuestionInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Question WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["Q_Cate"] = entity.Q_Cate;
                    DrAdd["Q_Question"] = entity.Q_Question;
                    DrAdd["Q_Option_A"] = entity.Q_Option_A;
                    DrAdd["Q_Option_B"] = entity.Q_Option_B;
                    DrAdd["Q_Option_C"] = entity.Q_Option_C;
                    DrAdd["Q_Option_D"] = entity.Q_Option_D;
                    DrAdd["Q_Answer"] = entity.Q_Answer;

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

        public virtual int DelQuestion(int ID)
        {
            string SqlAdd = "DELETE FROM Question WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual QuestionInfo GetQuestionByID(int ID)
        {
            QuestionInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Question WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new QuestionInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.Q_Cate = Tools.NullInt(RdrList["Q_Cate"]);
                    entity.Q_Question = Tools.NullStr(RdrList["Q_Question"]);
                    entity.Q_Option_A = Tools.NullStr(RdrList["Q_Option_A"]);
                    entity.Q_Option_B = Tools.NullStr(RdrList["Q_Option_B"]);
                    entity.Q_Option_C = Tools.NullStr(RdrList["Q_Option_C"]);
                    entity.Q_Option_D = Tools.NullStr(RdrList["Q_Option_D"]);
                    entity.Q_Answer = Tools.NullStr(RdrList["Q_Answer"]);

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

        public virtual IList<QuestionInfo> GetQuestions(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<QuestionInfo> entitys = null;
            QuestionInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Question";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<QuestionInfo>();
                    while (RdrList.Read())
                    {
                        entity = new QuestionInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.Q_Cate = Tools.NullInt(RdrList["Q_Cate"]);
                        entity.Q_Question = Tools.NullStr(RdrList["Q_Question"]);
                        entity.Q_Option_A = Tools.NullStr(RdrList["Q_Option_A"]);
                        entity.Q_Option_B = Tools.NullStr(RdrList["Q_Option_B"]);
                        entity.Q_Option_C = Tools.NullStr(RdrList["Q_Option_C"]);
                        entity.Q_Option_D = Tools.NullStr(RdrList["Q_Option_D"]);
                        entity.Q_Answer = Tools.NullStr(RdrList["Q_Answer"]);

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

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Question";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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



