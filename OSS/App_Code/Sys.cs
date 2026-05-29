using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.BLL.Sys;
using System.Text.RegularExpressions;

/// <summary>
///System 的摘要说明
/// </summary>
public class Sys
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private IRBACUser MyBLL;
    private IRBACRole MyRoleBLL;
    private IRBACUserRelateCustomer MyUserRelateCustomer;

    public Sys()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyBLL = RBACUserFactory.CreateRBACUser();
        MyRoleBLL = RBACRoleFactory.CreateRBACRole();
        MyUserRelateCustomer = RBACUserRelateCustomerFactory.CreateRBACUserRelateCustomer();
    }

    public string IPAddress()
    {
        try
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理
                if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];    //找到不是内网的地址
                            }
                        }
                    }
                    else if (IsIPAddress(result)) //代理即是IP格式 ,IsIPAddress判断是否是IP的方法,
                        return result;
                    else
                        result = null;    //代理中的内容 非IP，取IP
                }

            }

            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }
        catch
        {
            return tools.NullStr(Request.ServerVariables["Remote_Addr"]);
        }

    }

     /// <summary>  
     /// 判断输入的ip地址是否正确，返回TRUE or FALSE  
     /// </summary>  
     /// <param name="strJudgeString">等待判断的字符串</param>  
     /// <returns>TRUE OR FALSE</returns>  
     private bool IsIPAddress(string strJudgeString)
     {
         bool blnTest = false;
         bool _Result = true;

         Regex regex = new Regex("^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$");
         blnTest = regex.IsMatch(strJudgeString);
         if (blnTest == true)
         {
             string[] strTemp = strJudgeString.Split(new char[] { '.' }); // textBox1.Text.Split(new char[] { ‘.’ });  
             int nDotCount = strTemp.Length - 1; //字符串中.的数量，若.的数量小于3，则是非法的ip地址  
             if (3 == nDotCount)//判断字符串中.的数量  
             {
                 for (int i = 0; i < strTemp.Length; i++)
                 {
                     if (Convert.ToInt32(strTemp[i]) > 255)
                     { //大于255则提示，不符合IP格式  

                         _Result = false;
                         //txtbox_ServerIP.Text = "";  
                     }
                 }
             }
             else
             {

                 _Result = false;
             }
         }
         else
         {
             //输入非数字则提示，不符合IP格式  

             _Result = false;
             // txtbox_ServerIP.Text = "";  
         }
         return _Result;
     }  
    /// <summary>
    /// 创建一个用户权限实例
    /// </summary>
    /// <param name="PrivilegeCode">权限代码</param>
    /// <returns></returns>
    public RBACUserInfo CreateUserLoginPrivilege()
    {
        RBACUserInfo UserInfo = new RBACUserInfo();

        UserInfo.RBACRoleInfos = new List<RBACRoleInfo>();
        UserInfo.RBACRoleInfos.Add(new RBACRoleInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos = new List<RBACPrivilegeInfo>();

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos.Add(new RBACPrivilegeInfo());

        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[0].RBAC_Privilege_ID = "f7fb595e-75cf-4dd2-8557-fadfa5756058";
        UserInfo.RBACRoleInfos[0].RBACPrivilegeInfos[1].RBAC_Privilege_ID = "b47f8b43-cd62-4afc-8538-9acc6ba2a762";

        return UserInfo;
    }

    public void login()
    {
        string verifycode, username, password, userremember;
        verifycode = tools.CheckStr(Request["verifycode"]);
        if (Session["Trade_Verify"] == null || verifycode == "" || verifycode != Session["Trade_Verify"].ToString())
        {
            if (Request.Form["backurl"] != null && Request.Form["backurl"].Length > 0)
            {
                Response.Redirect(tools.NullStr(Request.Form["backurl"]));
                return;
            }
            else
            {
                Response.Redirect("login.aspx?tip=ErrorVerifyCode");
                return;
            }
        }

        username = tools.CheckStr(Request.Form["username"]);
        password = tools.CheckStr(Request.Form["password"]);
        password = encrypt.MD5(password);
        if (Check_Login_Err(username))
        {
            Response.Redirect("login.aspx?tip=ErrorAmount");
        }
        userremember = tools.CheckStr(Request.Form["userremember"]);
        Session["User_Name"] = username;
        RBACUserInfo userInfo = MyBLL.GetRBACUserByName(username, CreateUserLoginPrivilege());
        if (userInfo != null)
        {
            if (userInfo.RBAC_User_Password == password)
            {
                Session["UserLogin"] = "true";
                Session["User_ID"] = userInfo.RBAC_User_ID;
                Session["User_GroupID"] = userInfo.RBAC_User_GroupID;
                Session["User_Name"] = userInfo.RBAC_User_Name;
                Session["User_LastLogin"] = userInfo.RBAC_User_LastLogin;
                Session["User_LastLoginIP"] = userInfo.RBAC_User_LastLoginIP;
                Session["User_Addtime"] = userInfo.RBAC_User_Addtime;
                Session["User_Privilege"] = userInfo.RBACRoleInfos;
                userInfo.RBAC_User_LastLogin = DateTime.Now;
                //userInfo.RBAC_User_LastLoginIP = Request.UserHostAddress;
                userInfo.RBAC_User_LastLoginIP = IPAddress();
                MyBLL.EditRBACUser(userInfo, CreateUserLoginPrivilege());
                
                Session["UserPrivilege"] = userInfo;
                
                Response.Cookies["username"].Expires = DateTime.Now.AddYears(1);
                if (userremember == "1")
                {
                    Response.Cookies["username"].Value = Server.UrlEncode(username);
                }
                else
                {
                    Response.Cookies["username"].Value = "";
                }
                if (Request.Form["backurl"] != null && Request.Form["backurl"].Length > 0)
                {
                    Public.AddRBACUserLog(1, userInfo.RBAC_User_ID.ToString(), "系统用户登录", "呼叫中心登录", 1);
                    if (Session["backurl"] != null)
                    {
                        Response.Redirect(Session["backurl"].ToString());
                    }
                    Response.Redirect("/callcenter/");
                }
                Public.AddRBACUserLog(1, userInfo.RBAC_User_ID.ToString(), "系统用户登录", "系统后台登录", 1);
                Response.Redirect("index.aspx");
                return;
            }
            else
            {
                Session["UserLogin"] = "false";
                Public.AddRBACUserLog(1, userInfo.RBAC_User_ID.ToString(), "系统用户登录", "系统后台登录", 0);
                Response.Redirect("login.aspx?tip=ErrorInfo");
                return;
            }
        }
        else
        {
            Session["UserLogin"] = "false";
            Public.AddRBACUserLog(1, "0", "系统用户登录", "系统后台登录", 0);
            Response.Redirect("login.aspx?tip=ErrorInfo");
            return;
        }
    }

    public void loginout()
    {
        Public.AddRBACUserLog(1, tools.NullInt(Session["User_ID"]).ToString(), "系统用户退出", "系统用户退出", 1);
        Session["UserLogin"] = "false";
        Session["User_ID"] = 0;
        Session["User_GroupID"] = 0;
        Session["User_Name"] = "";
        Session["User_Addtime"] = null;
        
        Response.Write("<script type\"text/javascript\">");
        Response.Write("parent.location.href='/login.aspx?time='+ new Date().getTime();");
        Response.Write("</script>");
    }

    //管理员错误登录检查
    public bool Check_Login_Err(string username)
    {
        bool result = false;
        int Log_ID = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 6;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_UserName", "=", username));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogInfo.Log_Result", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Action", "=", "系统用户登录"));
        
        Query.OrderInfos.Add(new OrderInfo("RBACUserLogInfo.Log_ID", "Desc"));
        IList<RBACUserLogInfo> entitys = MyBLL.GetRBACUserLogs(Query);
        if (entitys != null)
        {
            Log_ID = entitys[0].Log_ID;
        }
        Query = new QueryInfo();
        Query.PageSize = 6;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogInfo.Log_ID", ">", Log_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_UserName", "=", username));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Action", "=", "系统用户登录"));
        
        Query.OrderInfos.Add(new OrderInfo("RBACUserLogInfo.Log_ID", "Desc"));
        entitys = MyBLL.GetRBACUserLogs(Query);
        if (entitys != null)
        {
            if (entitys.Count > 4)
            {
                if (DateTime.Now.AddMinutes(-30) <= entitys[0].Log_Addtime)
                {
                    result = true;
                }
            }
        }

        return result;
    }

    public void AddRBACUser()
    {
        int RBAC_User_ID = tools.CheckInt(Request.Form["RBAC_User_ID"]);
        int RBAC_User_GroupID = tools.CheckInt(Request.Form["RBAC_User_GroupID"]);
        string RBAC_User_Name = tools.CheckStr(Request.Form["RBAC_User_Name"]);
        string RBAC_User_Password = tools.CheckStr(Request.Form["RBAC_User_Password"]);
        string RBAC_User_Password_Confirm = tools.CheckStr(Request.Form["RBAC_User_Password_Confirm"]);

        if (RBAC_User_Name == "" || RBAC_User_Password == "") { Public.Msg("error", "错误信息", "请输入用户名或密码", false, "{back}"); return; }
        if (RBAC_User_Password != RBAC_User_Password_Confirm) { Public.Msg("error", "错误信息", "两次输入密码不一致", false, "{back}"); return; }

        string[] strRole = tools.CheckStr(Request.Form["role_id"]).Split(',');
        IList<RBACRoleInfo> roleList = new List<RBACRoleInfo>();
        RBACRoleInfo role;
        foreach (string role_id in strRole)
        {
            if (role_id != "")
            {
                role = new RBACRoleInfo();
                role.RBAC_Role_ID = int.Parse(role_id);
                roleList.Add(role);
                role = null;
            }
        }

        RBACUserInfo entity = new RBACUserInfo();
        entity.RBAC_User_ID = RBAC_User_ID;
        entity.RBAC_User_GroupID = RBAC_User_GroupID;
        entity.RBAC_User_Name = RBAC_User_Name;
        entity.RBAC_User_Password = encrypt.MD5(RBAC_User_Password);
        entity.RBAC_User_LastLogin = DateTime.Now;
        entity.RBAC_User_LastLoginIP = Request.UserHostAddress;
        entity.RBAC_User_Addtime = DateTime.Now;
        entity.RBAC_User_Site = Public.GetCurrentSite();
        entity.RBACRoleInfos = roleList;

        if (MyBLL.AddRBACUser(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(67, "", "后台用户添加", RBAC_User_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "user_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(67, "", "后台用户添加", RBAC_User_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACUser()
    {

        int RBAC_User_ID = tools.CheckInt(Request.Form["RBAC_User_ID"]);
        int RBAC_User_GroupID = tools.CheckInt(Request.Form["RBAC_User_GroupID"]);
        string RBAC_User_Password = tools.CheckStr(Request.Form["RBAC_User_Password"]);
        string RBAC_User_Password_Confirm = tools.CheckStr(Request.Form["RBAC_User_Password_Confirm"]);

        string[] strRole = tools.CheckStr(Request.Form["role_id"]).Split(',');
        IList<RBACRoleInfo> roleList = new List<RBACRoleInfo>();
        RBACRoleInfo role;
        foreach (string role_id in strRole)
        {
            if (role_id != "")
            {
                role = new RBACRoleInfo();
                role.RBAC_Role_ID = int.Parse(role_id);
                roleList.Add(role);
                role = null;
            }
        }

        RBACUserInfo entity = MyBLL.GetRBACUserByID(RBAC_User_ID, Public.GetUserPrivilege());

        if (entity == null) { Public.Msg("error", "错误信息", "该用户不存在", false, "{back}"); return; }

        if (RBAC_User_Password != "")
        {
            if (RBAC_User_Password != RBAC_User_Password_Confirm)
            {
                Public.Msg("error", "错误信息", "两次输入密码不一致", false, "{back}"); return;
            }
            entity.RBAC_User_Password = encrypt.MD5(RBAC_User_Password);
        }

        entity.RBAC_User_ID = RBAC_User_ID;
        entity.RBAC_User_GroupID = RBAC_User_GroupID;
        entity.RBAC_User_Site = Public.GetCurrentSite();
        entity.RBACRoleInfos = roleList;

        if (MyBLL.EditRBACUser(entity, Public.GetUserPrivilege()))
        {
            //DelRBACUserRelateCustomerByUserID(entity.RBAC_User_ID);

            Public.AddRBACUserLog(67, RBAC_User_ID.ToString(), "后台用户修改", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "user_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(67, RBAC_User_ID.ToString(), "后台用户修改", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACUser()
    {
        int RBAC_User_ID = tools.CheckInt(Request.QueryString["RBAC_User_ID"]);
        if (MyBLL.DelRBACUser(RBAC_User_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(67, RBAC_User_ID.ToString(), "后台用户删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "user_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(67, RBAC_User_ID.ToString(), "后台用户删除", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACUserRelateCustomerByUserID(int UserID)
    {
        MyUserRelateCustomer.DelRBACUserRelateCustomerByUserID(UserID);
    }

    public string GetRBACUserRelateCustomerIDByUserID(int UserID)
    {
        return MyUserRelateCustomer.GetRelateCustomerByUserID(UserID);
    }

    //public void SessionBigCustomer(int UserID)
    //{
    //    string member = GetRBACUserRelateCustomerIDByUserID(UserID);

    //    if (member.Length > 0)
    //    {
    //        QueryInfo Query = new QueryInfo();
    //        Query.PageSize = 0;
    //        Query.CurrentPage = 1;
    //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BigCustomerInfo.Big_Customer_ID", ">", "0"));
    //        Query.ParamInfos.Add(new ParamInfo("AND", "int", "BigCustomerInfo.Big_Customer_ID", "in", member));

    //        Query.OrderInfos.Add(new OrderInfo("BigCustomerInfo.Big_Customer_ID", "DESC"));

    //        IList<BigCustomerInfo> entitys = MyBigCustomer.GetBigCustomerList(Query);
    //        if (entitys != null)
    //        {
    //            Session["BigCustomerInfo"] = entitys;
    //        }
    //        else
    //        {
    //            Session["BigCustomerInfo"] = new List<BigCustomerInfo>();
    //        }
    //    }
    //    else
    //    {
    //        Session["BigCustomerInfo"] = new List<BigCustomerInfo>();
    //    }
    //}

    public RBACUserInfo GetRBACUserByID(int cate_id)
    {
        return MyBLL.GetRBACUserByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetRBACUsers()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserInfo.RBAC_User_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACUserInfo> entitys = MyBLL.GetRBACUsers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACUserInfo entity in entitys)
            {
                jsonBuilder.Append("{\"RBACUserInfo.RBAC_User_ID\":" + entity.RBAC_User_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_LastLogin);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_LastLoginIP);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_User_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"user_edit.aspx?rbac_user_id=" + entity.RBAC_User_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('user_do.aspx?action=move&rbac_user_id=" + entity.RBAC_User_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    public string DisplayRoleCheckbox(IList<RBACRoleInfo> roles)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACRoleInfo.RBAC_Role_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("RBACRoleInfo.RBAC_Role_ID", "DESC"));
        IList<RBACRoleInfo> entitys = MyRoleBLL.GetRBACRoles(Query, Public.GetUserPrivilege());
        Query = null;
        if (entitys != null)
        {
            strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
            strHTML.Append("<tr>");
            strHTML.Append("    <td>");
            foreach (RBACRoleInfo entity in entitys)
            {
                strHTML.Append("<input type=\"checkbox\" name=\"role_id\" id=\"role_id" + entity.RBAC_Role_ID + "\" value=\"" + entity.RBAC_Role_ID + "\" " + RoleChecked(entity.RBAC_Role_ID, roles) + "/>" + entity.RBAC_Role_Name + "&nbsp;");
            }
            strHTML.Append("    </td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string RoleChecked(int Role_ID, IList<RBACRoleInfo> roles)
    {
        string valExt = "";
        try
        {
            if (roles != null)
            {
                foreach (RBACRoleInfo entity in roles)
                {
                    if (entity.RBAC_Role_ID == Role_ID)
                    {
                        valExt = "checked=\"checked\"";
                    }
                }
            }
        }
        catch (Exception ex) { }

        return valExt;
    }

    public void EditPassword()
    {
        string RBAC_User_Password = tools.CheckStr(Request.Form["RBAC_User_Password"]);
        string RBAC_User_Password_Confirm = tools.CheckStr(Request.Form["RBAC_User_Password_Confirm"]);

        if (RBAC_User_Password != RBAC_User_Password_Confirm) { Public.Msg("error", "错误信息", "两次输入密码不一致", false, "{back}"); return; }

        if (MyBLL.EditUserPassword(encrypt.MD5(RBAC_User_Password), (int)Session["User_ID"]))
        {
            Public.AddRBACUserLog(67, "", "后台用户修改密码", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "/main.aspx");
        }
        else
        {
            Public.AddRBACUserLog(67, "", "后台用户修改密码", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //添加管理员操作日志
    public bool AddRBACUserLog(int Channel,string Obj_ID,string Action,string Log_Note,int Result)
    {
        
        RBACUserLogInfo entity=new RBACUserLogInfo();
        entity.Log_Channel = Channel;
        entity.Log_UserID = tools.NullInt(Session["User_ID"]);
        entity.Log_UserName = tools.NullStr(Session["User_Name"]);
        entity.Log_User_ObjectID = Obj_ID;
        entity.Log_Action = Action;
        entity.Log_Description = Log_Note;
        entity.Log_Result = Result;
        entity.Log_Addtime = DateTime.Now;
        entity.Log_IP = tools.NullStr(Request.ServerVariables["Remote_Addr"]);
        //entity.Log_IP = IPAddress();
        entity.Log_Site = Public.GetCurrentSite();
        return MyBLL.AddRBACUserLog(entity);
    }

    public string GetRBACUserLogs()
    {
        int channel = tools.CheckInt(Request["channel"]);
        string date_start, date_end,  keyword;
        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);
        keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Site", "=", Public.GetCurrentSite()));
        if (channel == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogInfo.Log_Channel", "=", channel.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogInfo.Log_Channel", ">", "1"));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "RBACUserLogInfo.Log_UserName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "RBACUserLogInfo.Log_User_ObjectID", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "RBACUserLogInfo.Log_Description", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "RBACUserLogInfo.Log_Action", "like", keyword));
        }
        if (date_start.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{RBACUserLogInfo.Log_Addtime})", ">=", "0"));
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Addtime", ">=", date_start));
        }
        if (date_end.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{RBACUserLogInfo.Log_Addtime})", "<=", "0"));
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Addtime", "<=", date_end));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetUserLogPageInfo(Query);

        IList<RBACUserLogInfo> entitys = MyBLL.GetRBACUserLogs(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACUserLogInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Log_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Log_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Log_UserName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Log_User_ObjectID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Log_Description));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Log_Action));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Log_IP);
                jsonBuilder.Append("\",");


                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }


    public string GetRBACUserLogs2()
    {
        int channel = tools.CheckInt(Request["channel"]);
        string date_start, date_end, keyword;
        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);
        keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Site", "=", Public.GetCurrentSite()));
        if (channel > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogInfo.Log_Channel", "=", channel.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogInfo.Log_Channel", "!=", "0"));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "RBACUserLogInfo.Log_UserName", "like", keyword));
            //Query.ParamInfos.Add(new ParamInfo("OR", "str", "RBACUserLogInfo.Log_User_ObjectID", "like", keyword));
            //Query.ParamInfos.Add(new ParamInfo("OR", "str", "RBACUserLogInfo.Log_Description", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "RBACUserLogInfo.Log_Action", "like", keyword));
        }
        //if (date_start.Length > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Addtime", ">=", date_start));
        //}
        //if (date_end.Length > 0)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Addtime", "<=", date_end));
        //}
        if (date_start.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{RBACUserLogInfo.Log_Addtime})", ">=", "0"));
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Addtime", ">=", date_start));
        }
        if (date_end.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{RBACUserLogInfo.Log_Addtime})", "<=", "0"));
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserLogInfo.Log_Addtime", "<=", date_end));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetUserLogPageInfo(Query);

        IList<RBACUserLogInfo> entitys = MyBLL.GetRBACUserLogs(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACUserLogInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Log_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Log_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Log_UserName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Log_User_ObjectID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Log_Description));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Log_Action));
                jsonBuilder.Append("\",");

                if (entity.Log_Result == 1)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("成功");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("失败");
                    jsonBuilder.Append("\",");
                }
                

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Log_IP);
                jsonBuilder.Append("\",");


                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }


    public string GetUserLogChannelOption(int ID)
    {
        string Html = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "RBACUserLogChannelInfo.Log_Channel_Type", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("RBACUserLogChannelInfo.Log_Channel_ID", "Asc"));
        IList<RBACUserLogChannelInfo> entitys = MyBLL.GetRBACUserLogChannels(Query);
        if (entitys != null)
        {
            foreach (RBACUserLogChannelInfo entity in entitys)
            {
                if (entity.Log_Channel_ID == ID)
                {
                    Html = Html + "<option value=\"" + entity.Log_Channel_ID + "\" selected>" + entity.Log_Channel_Name + "</option>";
                }
                else
                {
                    Html = Html + "<option value=\"" + entity.Log_Channel_ID + "\">" + entity.Log_Channel_Name + "</option>";
                }
            }
        }

        return Html;
    }
}

public class RBACUserGroup
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private IRBACUserGroup MyBLL;

    public RBACUserGroup()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyBLL = RBACUserFactory.CreateRBACUserGroup();
    }


    public void AddRBACUserGroup()
    {
        int RBAC_UserGroup_ID = tools.CheckInt(Request.Form["RBAC_UserGroup_ID"]);
        string RBAC_UserGroup_Name = tools.CheckStr(Request.Form["RBAC_UserGroup_Name"]);
        int RBAC_UserGroup_ParentID = tools.CheckInt(Request.Form["RBAC_UserGroup_ParentID"]);

        RBACUserGroupInfo entity = new RBACUserGroupInfo();
        entity.RBAC_UserGroup_ID = RBAC_UserGroup_ID;
        entity.RBAC_UserGroup_Name = RBAC_UserGroup_Name;
        entity.RBAC_UserGroup_ParentID = RBAC_UserGroup_ParentID;
        entity.RBAC_UserGroup_Site = Public.GetCurrentSite();

        if (MyBLL.AddRBACUserGroup(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "usergroup_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditRBACUserGroup()
    {
        int RBAC_UserGroup_ID = tools.CheckInt(Request.Form["RBAC_UserGroup_ID"]);
        string RBAC_UserGroup_Name = tools.CheckStr(Request.Form["RBAC_UserGroup_Name"]);
        int RBAC_UserGroup_ParentID = tools.CheckInt(Request.Form["RBAC_UserGroup_ParentID"]);

        RBACUserGroupInfo entity = new RBACUserGroupInfo();
        entity.RBAC_UserGroup_ID = RBAC_UserGroup_ID;
        entity.RBAC_UserGroup_Name = RBAC_UserGroup_Name;
        entity.RBAC_UserGroup_ParentID = RBAC_UserGroup_ParentID;
        entity.RBAC_UserGroup_Site = Public.GetCurrentSite();

        if (MyBLL.EditRBACUserGroup(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "usergroup_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelRBACUserGroup()
    {
        int RBAC_UserGroup_ID = tools.CheckInt(Request.QueryString["RBAC_UserGroup_ID"]);
        if (MyBLL.DelRBACUserGroup(RBAC_UserGroup_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "usergroup_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public RBACUserGroupInfo GetRBACUserGroupByID(int cate_id)
    {
        return MyBLL.GetRBACUserGroupByID(cate_id, Public.GetUserPrivilege());
    }
    //public RBACUserInfo GetRBACUserByIDAll(int id)
    //{
    //    return MyBLL.GetRBACUserByID(id, CreateUserLoginPrivilege());
    //}

    public string GetRBACUserGroups()
    {

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserGroupInfo.RBAC_UserGroup_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<RBACUserGroupInfo> entitys = MyBLL.GetRBACUserGroups(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (RBACUserGroupInfo entity in entitys)
            {
                jsonBuilder.Append("{\"RBACUserGroupInfo.RBAC_UserGroup_ID\":" + entity.RBAC_UserGroup_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_UserGroup_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.RBAC_UserGroup_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"usergroup_edit.aspx?rbac_usergroup_id=" + entity.RBAC_UserGroup_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('usergroup_do.aspx?action=move&rbac_usergroup_id=" + entity.RBAC_UserGroup_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    public string UserGroupOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "RBACUserGroupInfo.RBAC_UserGroup_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("RBACUserGroupInfo.RBAC_UserGroup_ID", "DESC"));
        IList<RBACUserGroupInfo> entitys = MyBLL.GetRBACUserGroups(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (RBACUserGroupInfo entity in entitys)
            {
                if (entity.RBAC_UserGroup_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.RBAC_UserGroup_ID + "\" selected=\"selected\">" + entity.RBAC_UserGroup_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.RBAC_UserGroup_ID + "\">" + entity.RBAC_UserGroup_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

}

public class SysMenu
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISysMenu MyBLL;
    ArticleCate articleCate;
    public SysMenu()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SysMenuFactory.CreateSysMenu();
        articleCate = new ArticleCate();
    }

    //添加菜单
    public virtual void AddSysMenu()
    {
        int Sys_Menu_ID = 0;
        int Sys_Menu_Channel = tools.CheckInt(Request.Form["Sys_Menu_Channel"]);
        string Sys_Menu_Name = tools.CheckStr(Request.Form["Sys_Menu_Name"]);
        int Sys_Menu_ParentID = tools.CheckInt(Request.Form["Sys_Menu_ParentID"]);
        string Sys_Menu_Privilege = tools.CheckStr(Request.Form["Sys_Menu_Privilege"]);
        string Sys_Menu_Icon = tools.CheckStr(Request.Form["Sys_Menu_Icon"]);
        string Sys_Menu_Url = tools.CheckStr(Request.Form["Sys_Menu_Url"]);
        int Sys_Menu_Target = tools.CheckInt(Request.Form["Sys_Menu_Target"]);

        int Sys_Menu_IsDefault = tools.CheckInt(Request.Form["Sys_Menu_IsDefault"]);
        int Sys_Menu_IsCommon = tools.CheckInt(Request.Form["Sys_Menu_IsCommon"]);
        int Sys_Menu_IsActive = tools.CheckInt(Request.Form["Sys_Menu_IsActive"]);
        int Sys_Menu_Sort = tools.CheckInt(Request.Form["Sys_Menu_Sort"]);
        string Sys_Menu_Site = Public.GetCurrentSite();

        if (Sys_Menu_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写菜单项名称", false, "{back}");
        }

        SysMenuInfo entity = new SysMenuInfo();
        entity.Sys_Menu_ID = Sys_Menu_ID;
        entity.Sys_Menu_Channel = Sys_Menu_Channel;
        entity.Sys_Menu_Name = Sys_Menu_Name;
        entity.Sys_Menu_ParentID = Sys_Menu_ParentID;
        entity.Sys_Menu_Privilege = Sys_Menu_Privilege;
        entity.Sys_Menu_Icon = Sys_Menu_Icon;
        entity.Sys_Menu_Url = Sys_Menu_Url;
        entity.Sys_Menu_Target = Sys_Menu_Target;
        entity.Sys_Menu_IsSystem = 0;
        entity.Sys_Menu_IsDefault = Sys_Menu_IsDefault;
        entity.Sys_Menu_IsCommon = Sys_Menu_IsCommon;
        entity.Sys_Menu_IsActive = Sys_Menu_IsActive;
        entity.Sys_Menu_Sort = Sys_Menu_Sort;
        entity.Sys_Menu_Site = Sys_Menu_Site;

        if (MyBLL.AddSysMenu(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(74, "", "系统菜单添加", Sys_Menu_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_Menu_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(74, "", "系统菜单添加", Sys_Menu_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //栏目选择
    public virtual void Select_Channel(int Channel_ID)
    {
        int i = 1;
        string channel_name = "广告管理,内容管理,系统管理,会员管理";
        Response.Write("<select name=\"Sys_Menu_Channel\">");
        foreach (string substr in channel_name.Split(','))
        {
            if (Channel_ID == i)
            {
                Response.Write("<option value=\"" + i + "\" selected>" + substr + "</option>");
            }
            else
            {
                Response.Write("<option value=\"" + i + "\">" + substr + "</option>");
            }
            i = i + 1;
        }

        Response.Write("</select>");
    }

    //栏目选择
    public virtual void Select_Menu_Channel(int Channel_ID)
    {
        int i = 1;
        string channel_name = "广告,内容管理,系统管理,会员管理";
        Response.Write("<select name=\"Sys_Menu_Channel\" onchange=\"$('#menu_div').load('sys_menu_do.aspx?action=changemenu&channel='+$(this).val()+'&timer='+Math.random())\">");
        foreach (string substr in channel_name.Split(','))
        {
            if (substr.Length > 0)
            {
                if (Channel_ID == i)
                {
                    Response.Write("<option value=\"" + i + "\" selected>" + substr + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + i + "\">" + substr + "</option>");
                }
            }
            i = i + 1;
        }

        Response.Write("</select>");
    }

    //所属菜单选择
    public virtual void Select_Menu_Parent(string Select_Name, int Parent_ID, int Channel_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        if (Channel_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", Channel_ID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_ParentID", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SysMenuInfo.Sys_Menu_ID", "Desc"));
        Response.Write("<select name=\"" + Select_Name + "\">");
        Response.Write("<option value=\"0\">请选择</option>");
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SysMenuInfo entity in entitys)
            {
                if (Parent_ID == entity.Sys_Menu_ID)
                {
                    Response.Write("<option value=\"" + entity.Sys_Menu_ID + "\" selected>" + entity.Sys_Menu_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Sys_Menu_ID + "\">" + entity.Sys_Menu_Name + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

    //获取栏目名称
    public virtual string Get_Channel_Name(int Channel_ID)
    {
        string channel_name = "";
        switch (Channel_ID)
        {
            case 1:
                channel_name = "广告管理";
                break;
            case 2:
                channel_name = "内容管理";
                break;
            case 3:
                channel_name = "系统管理";
                break;
            case 4:
                channel_name = "会员管理";
                break;
           
        }
        return channel_name;
    }

    //菜单列表
    public string GetSysMenus()
    {
        int channel_id;
        channel_id = tools.CheckInt(Request["channel_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsSystem", "=", "0"));
        if (channel_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", channel_id.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        SysMenuInfo menuinfo;
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SysMenuInfo entity in entitys)
            {
                menuinfo = MyBLL.GetSysMenuByID(entity.Sys_Menu_ParentID, Public.GetUserPrivilege());
                jsonBuilder.Append("{\"SysMenuInfo.Sys_Menu_ID\":" + entity.Sys_Menu_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Sys_Menu_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Get_Channel_Name(entity.Sys_Menu_Channel)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (menuinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(menuinfo.Sys_Menu_Name));
                }
                else
                {
                    jsonBuilder.Append("");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_Privilege);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_Url);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_Target == 0)
                {
                    jsonBuilder.Append("框架内");
                }
                else
                {
                    jsonBuilder.Append("新窗口");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_IsDefault == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_IsCommon == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Sys_Menu_IsActive == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Sys_Menu_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("c9ce4dd0-6391-4fb9-aa99-f37c23c04a8a") && entity.Sys_Menu_IsSystem == 0)
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"sys_menu_edit.aspx?menu_id=" + entity.Sys_Menu_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("e5e043cc-5085-41f9-b406-808c319b3a70") && entity.Sys_Menu_IsSystem == 0)
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('sys_menu_do.aspx?action=move&menu_id=" + entity.Sys_Menu_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    //根据编号获取菜单信息
    public SysMenuInfo GetSysMenuByID(int ID)
    {
        return MyBLL.GetSysMenuByID(ID, Public.GetUserPrivilege());
    }

    //修改菜单
    public virtual void EditSysMenu()
    {
        int Sys_Menu_ID = tools.CheckInt(Request.Form["Sys_Menu_ID"]);
        int Sys_Menu_Channel = tools.CheckInt(Request.Form["Sys_Menu_Channel"]);
        string Sys_Menu_Name = tools.CheckStr(Request.Form["Sys_Menu_Name"]);
        int Sys_Menu_ParentID = tools.CheckInt(Request.Form["Sys_Menu_ParentID"]);
        string Sys_Menu_Privilege = tools.CheckStr(Request.Form["Sys_Menu_Privilege"]);
        string Sys_Menu_Icon = tools.CheckStr(Request.Form["Sys_Menu_Icon"]);
        string Sys_Menu_Url = tools.CheckStr(Request.Form["Sys_Menu_Url"]);
        int Sys_Menu_Target = tools.CheckInt(Request.Form["Sys_Menu_Target"]);
        int Sys_Menu_IsDefault = tools.CheckInt(Request.Form["Sys_Menu_IsDefault"]);
        int Sys_Menu_IsCommon = tools.CheckInt(Request.Form["Sys_Menu_IsCommon"]);
        int Sys_Menu_IsActive = tools.CheckInt(Request.Form["Sys_Menu_IsActive"]);
        int Sys_Menu_Sort = tools.CheckInt(Request.Form["Sys_Menu_Sort"]);
        string Sys_Menu_Site = Public.GetCurrentSite();

        if (Sys_Menu_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写菜单项名称", false, "{back}");
        }

        SysMenuInfo entity = new SysMenuInfo();
        entity.Sys_Menu_ID = Sys_Menu_ID;
        entity.Sys_Menu_Channel = Sys_Menu_Channel;
        entity.Sys_Menu_Name = Sys_Menu_Name;
        entity.Sys_Menu_ParentID = Sys_Menu_ParentID;
        entity.Sys_Menu_Privilege = Sys_Menu_Privilege;
        entity.Sys_Menu_Icon = Sys_Menu_Icon;
        entity.Sys_Menu_Url = Sys_Menu_Url;
        entity.Sys_Menu_Target = Sys_Menu_Target;
        entity.Sys_Menu_IsSystem = 0;
        entity.Sys_Menu_IsDefault = Sys_Menu_IsDefault;
        entity.Sys_Menu_IsCommon = Sys_Menu_IsCommon;
        entity.Sys_Menu_IsActive = Sys_Menu_IsActive;
        entity.Sys_Menu_Sort = Sys_Menu_Sort;
        entity.Sys_Menu_Site = Sys_Menu_Site;

        if (MyBLL.EditSysMenu(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(74, Sys_Menu_ID.ToString(), "系统菜单修改", Sys_Menu_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_Menu_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(74, Sys_Menu_ID.ToString(), "系统菜单修改", Sys_Menu_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除菜单项
    public void DelSysMenu()
    {
        int Sys_Menu_ID = tools.CheckInt(Request.QueryString["menu_id"]);
        if (MyBLL.DelSysMenu(Sys_Menu_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(74, Sys_Menu_ID.ToString(), "系统菜单删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "Sys_Menu_list.aspx");
        }
        else
        {
            Public.AddRBACUserLog(74, Sys_Menu_ID.ToString(), "系统菜单删除", "", 1);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void Sys_Menu_Display(int Channel_ID)
    {
        StringBuilder Menu_Item;
        string menu_target;
        string default_css;
        bool Menu_Display = false;
        int num = 0;
        Response.Write("<div id=\"layout-menu\" class=\"menu\" style=\"overflow-y:auto;\">");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_ParentID", "=", "0"));
        if (Channel_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_Channel", "=", Channel_ID.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMenuInfo.Sys_Menu_IsCommon", "=", "1"));
        }
        Query.OrderInfos.Add(new OrderInfo("SysMenuInfo.Sys_Menu_Sort", "Asc"));
        IList<SysMenuInfo> entitys = MyBLL.GetSysMenus(Query, Public.GetUserPrivilege());
        IList<SysMenuInfo> entity_sub;
        if (entitys != null)
        {
            foreach (SysMenuInfo entity in entitys)
            {
                num = num + 1;
                Menu_Item = new StringBuilder();
                Menu_Display = false;
                Menu_Item.Append("<ul>");
                if (num == 1)
                {
                    Menu_Item.Append("<li onclick=\"menuFold(this);\" class=\"group open\"><img src=\"/Images/" + entity.Sys_Menu_Icon + "\" />" + entity.Sys_Menu_Name + "</li>");
                }
                else
                {
                    Menu_Item.Append("<li onclick=\"menuFold(this);\" class=\"group fold\"><img src=\"/Images/" + entity.Sys_Menu_Icon + "\" />" + entity.Sys_Menu_Name + "</li>");
                }
                entity_sub = MyBLL.GetSysMenusSub(entity.Sys_Menu_ID, Public.GetUserPrivilege());
                if (entity_sub != null)
                {
                    foreach (SysMenuInfo ent in entity_sub)
                    {

                        if ((Public.CheckPrivilege(ent.Sys_Menu_Privilege) || ent.Sys_Menu_Privilege == "") && ent.Sys_Menu_IsActive == 1)
                        {
                            if ((Channel_ID == 0 && ent.Sys_Menu_IsCommon == 1) || Channel_ID > 0)
                            {
                                Menu_Display = true;
                                if (ent.Sys_Menu_Target == 0)
                                {
                                    menu_target = "main";
                                }
                                else
                                {
                                    menu_target = "_blank";
                                }
                                if (ent.Sys_Menu_IsDefault == 0 || Channel_ID == 0)
                                {
                                    default_css = "menu_item";
                                }
                                else
                                {
                                    default_css = "menu_itemon";
                                }
                                if (ent.Sys_Menu_Url.IndexOf("?") > 0)
                                {
                                    Menu_Item.Append("<li onclick=\"menuOn(this);\"><a href=\"" + ent.Sys_Menu_Url + "&menu_id=" + ent.Sys_Menu_ID + "\"  target=\"" + menu_target + "\">" + ent.Sys_Menu_Name + "</a></li>");
                                }
                                else
                                {
                                    Menu_Item.Append("<li onclick=\"menuOn(this);\"><a href=\"" + ent.Sys_Menu_Url + "?menu_id=" + ent.Sys_Menu_ID + "\"  target=\"" + menu_target + "\">" + ent.Sys_Menu_Name + "</a></li>");
                                }

                                //if(ent.Sys_Menu_ID==202)
                                //{
                                //    Menu_Item.Append(articleCate.Article_LettList(ent.Sys_Menu_Url));
                                //}
                            }
                        }
                    }
                }
                Menu_Item.Append("</ul>");
                if (Menu_Display)
                {
                    Response.Write(Menu_Item.ToString());
                }
            }
        }
        Response.Write("</div>");
    }

    public virtual string Page_Menu_Title(int Menu_ID)
    {
        string menu_title = "";
        SysMenuInfo entity = GetSysMenuByID(Menu_ID);
        if (entity != null)
        {
            menu_title = entity.Sys_Menu_Name;

            SysMenuInfo parent = GetSysMenuByID(entity.Sys_Menu_ParentID);
            if (parent != null)
            {
                menu_title = parent.Sys_Menu_Name + " > " + menu_title;

                menu_title = Get_Channel_Name(parent.Sys_Menu_Channel) + " > " + menu_title;
            }
            else
            {
                menu_title = Get_Channel_Name(entity.Sys_Menu_Channel) + " > " + menu_title;
            }
        }

        return menu_title;
    }
}