<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%
    Sys sys = new Sys();
    switch (Request["action"])
    {
        case "login":
            sys.login();
            break;
        case "loginout":
            sys.loginout();
            break;

            //case "update":
            //    Session.Clear();
            //    Session.Abandon();
            //    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            //    Response.Write("true");
            //    break;
    }
%>
