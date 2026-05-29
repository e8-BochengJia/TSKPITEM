<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码

        Config config = new Config();
        config.Sys_UpdateApplication();
        config = null;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

        Session["Sys_User"] = "";
        Session["Sys_User_ID"] = "";
        Session["Sys_User_Name"] = "";
        Session["Sys_User_LastLogin"] = "";
        Session["Sys_User_LastLoginIP"] = "";

        Session["member_id"] = "";
        Session["member_email"] = "";
        Session["member_emailverify"] = "";
        Session["member_loginmobile"] = "";
        Session["member_loginmobileverify"] = "";
        Session["member_nickname"] = "";

        Session["member_logined"] = "";
        Session["logintype"] = "";
        Session["member_logincount"] = "";
        Session["member_lastlogin_time"] = "";
        Session["member_lastlogin_ip"] = "";
        Session["member_coinremain"] = "";
        Session["member_coincount"] = "";
        Session["member_grade"] = "";
        Session["Member_AllowSysEmail"] = "";
        Session["U_Member_Realname"] = "";
        Session["Cur_Position"] = "";
        Session["mifno"] = "";

      

    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
