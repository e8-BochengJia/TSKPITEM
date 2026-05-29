using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
///Addr 的摘要说明
/// </summary>
public class Config
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;


    private ITools tools;
    private IConfig MyBLL;
    private Public_Class pub = new Public_Class();

    public Config()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ConfigFactory.CreateConfig();
    }

    public ConfigInfo GetConfigByID(int id)
    {
        return MyBLL.GetConfigByID(id, pub.CreateUserPrivilege("ef84a07f-6281-4f54-84f9-c345adf9d765"));
    }

    public void Sys_UpdateApplication()
    {
        ConfigInfo entity = GetConfigByID(1);
        if (entity == null)
        {

            pub.Msg("error", "错误信息", "系统配置记录不存在", true, "/index.aspx");
            Response.End();
        }
        else
        {
            Application["Sys_Config_ID"] = entity.Sys_Config_ID;
            Application["Upload_Server_URL"] = entity.Upload_Server_URL;
            Application["Upload_Server_Return_WWW"] = entity.Upload_Server_Return_WWW;
            Application["Upload_Server_Return_Admin"] = entity.Upload_Server_Return_Admin;
            Application["Language_Define"] = entity.Language_Define;
            Application["Site_Name"] = entity.Site_Name;
            Application["Site_URL"] = entity.Site_URL;
            Application["Site_Logo"] = entity.Site_Logo;
            Application["Site_Tel"] = entity.Site_Tel;
            Application["Site_Fax"] = entity.Site_Fax;
            Application["Site_Email"] = entity.Site_Email;
            Application["Site_Keyword"] = entity.Site_Keyword;
            Application["Site_Description"] = entity.Site_Description;
            Application["Site_Title"] = entity.Site_Title;
            Application["Mail_Server"] = entity.Mail_Server;
            Application["Mail_ServerPort"] = entity.Mail_ServerPort;
            Application["Mail_EnableSsl"] = entity.Mail_EnableSsl;
            Application["Mail_ServerUserName"] = entity.Mail_ServerUserName;
            Application["Mail_ServerPassWord"] = entity.Mail_ServerPassWord;
            Application["Mail_FromName"] = entity.Mail_FromName;
            Application["Mail_From"] = entity.Mail_From;
            Application["Mail_Replyto"] = entity.Mail_Replyto;
            Application["Mail_Encode"] = entity.Mail_Encode;
            Application["Coin_Name"] = entity.Coin_Name;
            Application["Coin_Rate"] = entity.Coin_Rate;
            Application["Sys_Config_Site"] = entity.Sys_Config_Site;
            Application["Trade_Contract_IsActive"] = entity.Trade_Contract_IsActive;
            Application["Static_IsEnable"] = entity.Static_IsEnable;
            Application["Chinabank_Code"] = entity.Chinabank_Code;
            Application["Chinabank_Key"] = entity.Chinabank_Key;
            Application["Alipay_Email"] = entity.Alipay_Email;
            Application["Alipay_Code"] = entity.Alipay_Code;
            Application["Alipay_Key"] = entity.Alipay_Key;
            Application["Tenpay_Code"] = entity.Tenpay_Code;
            Application["Tenpay_Key"] = entity.Tenpay_Key;
            Application["RepidBuy_IsEnable"] = entity.RepidBuy_IsEnable;
            Application["Coupon_UsedAmount"] = entity.Coupon_UsedAmount;
            Application["Sys_Delivery_Code"] = entity.Sys_Delivery_Code;
            Application["Sys_SMS_Default"] = entity.Sys_SMS_Default;
            Application["Sys_Invite_Reward"] = entity.Sys_Invite_Reward;
            Application["Sys_Invited_Reward"] = entity.Sys_Invited_Reward;
            Application["SharedLabel_IsAudit"] = entity.SharedLabel_IsAudit;
            Application["Sys_OrderPhone"] = entity.Sys_OrderPhone;
            Application["Sys_TopCate"] = entity.Sys_TopCate;
            Application["Sys_CouponNotice"] = entity.Sys_CouponNotice;

        }

    }



}
