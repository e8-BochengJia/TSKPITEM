using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
using Glaer.Trade.B2C.BLL.AD;


/// <summary>
///AD 的摘要说明
/// </summary>
public class AD
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private IAD WebAD;
    private IADPosition WebADPosition;
    private ITools tools;
    private Public_Class pub = new Public_Class();

    public AD()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        WebAD = ADFactory.CreateAD();
        WebADPosition = ADPositionFactory.CreateADPosition();
        tools = ToolsFactory.CreateTools();
    }

    public void AD_Hits()
    {
        int hits_ad;
        int adv_id = tools.CheckInt(Request["adv_ID"]);
        if (adv_id > 0)
        {
            ADInfo entity = WebAD.GetADByID(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
            if (entity != null)
            {
                if (entity.Ad_IsActive == 1 && (DateTime.Today - entity.Ad_StartDate).TotalDays >= 0 && (DateTime.Today - entity.Ad_EndDate).TotalDays <= 0)
                {
                    hits_ad = WebAD.Adv_Show_Hits_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Response.Redirect(entity.Ad_Link);
                }
                else
                {
                    Response.Redirect("/index.aspx");
                }
            }
            else
            {
                Response.Redirect("/index.aspx");
            }
        }
        else
        {
            Response.Redirect("/index.aspx");
        }
    }

    public string AD_Show(string AD_Position, string Propertys, string DisplayStyle, int Col_Num)
    {
        string sys_Install_Path = "/AD/ad.aspx";
        string Ad_String = "";
        ADPositionInfo Positioninfo = WebADPosition.GetAD_PositionByValue(AD_Position, pub.CreateUserPrivilege("d3aa1596-cc86-46c7-80f0-8bf6248ee31e"));
        if (Positioninfo != null)
        {
            if (Positioninfo.Ad_Position_IsActive == 0)
            {
                return Ad_String;
            }
            else
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "=", AD_Position));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.Ad_IsActive", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ADInfo.Ad_StartDate}, GETDATE())", ">=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ADInfo.Ad_EndDate}, GETDATE())", "<=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", "CN"));
                Query.OrderInfos.Add(new OrderInfo("ADInfo.Ad_Sort", "asc"));
                IList<ADInfo> ADs = WebAD.GetADs(Query, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                if (ADs != null)
                {
                    switch (DisplayStyle)
                    {
                        case "cycle":
                            Ad_String = AD_Cycle_Show(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "cycle_li":
                            Ad_String = AD_Cycle_Show_li(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "cycletext":
                            Ad_String = AD_Cycle_ShowText(ADs, sys_Install_Path, Propertys);
                            break;
                        case "keyword":
                            Ad_String = AD_Keyword_Show(ADs, sys_Install_Path, Propertys);
                            break;
                        case "scroll2":
                            Ad_String = AD_Scroll2(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;

                        case "Special_scroll":
                            Ad_String = AD_SpecialScroll(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;

                    }
                }
                return Ad_String;
            }
        }
        else
        {
            return Ad_String;
        }

    }

    public string AD_Cycle_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        //按照播放频率在固定位置循环投放
        int i = ADs.Count;

        int[,] Freq = new int[i, 2];
        int adv_id = 0;
        int j, Min_ID, Min_Freq;
        j = 0;
        if (i == 1)
        {
            if (Propertys == "" || (Propertys != "" && ADs[0].Ad_IsContain == 1 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && ADs[0].Ad_IsContain == 0 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                adv_id = ADs[0].Ad_ID;
                Freq[0, 0] = adv_id;
                Freq[0, 1] = 0;
            }
        }
        else
        {
            foreach (ADInfo entity in ADs)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    if (entity.Ad_Show_Freq > 0)
                    {
                        Freq[j, 0] = entity.Ad_ID;
                        Freq[j, 1] = entity.Ad_Show_times / entity.Ad_Show_Freq;
                    }
                    else
                    {
                        Freq[j, 0] = 99999999;
                        Freq[j, 1] = 99999999;
                    }
                    //j++;
                }
                else
                {
                    Freq[j, 0] = 99999999;
                    Freq[j, 1] = 99999999;
                }
                j++;
            }
            Min_ID = Freq[0, 0];
            Min_Freq = Freq[0, 1];

            for (j = 1; j < i; j++)
            {
                if (Min_Freq > Freq[j, 1] && Freq[j, 1] >= 0)
                {
                    Min_ID = Freq[j, 0];
                    Min_Freq = Freq[j, 1];
                }
            }
            adv_id = Min_ID;
        }

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_ID == adv_id)
            {
                if (entity.Ad_MediaKind == 2)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                        if (Ad_Width > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                        }
                        if (Ad_Height > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                        }
                        Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                    }
                }
                else if (entity.Ad_MediaKind == 3)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        }

                        Ad_Show_Code = Ad_Show_Code + "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" ";
                        if (Ad_Width > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                        }
                        if (Ad_Height > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                        }
                        Ad_Show_Code = Ad_Show_Code + "><param name=\"movie\" value=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\"><param name=\"quality\" value=\"high\"><param name=\"wmode\" value=\"opaque\"></object>";
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = Ad_Show_Code + "</a>";
                        }
                    }
                }
                else if (entity.Ad_MediaKind == 4)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        }

                        Ad_Show_Code = Ad_Show_Code + entity.Ad_Media;
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = Ad_Show_Code + "</a>";
                        }
                    }
                }
            }
        }
        return Ad_Show_Code;
    }


    public string AD_Cycle_Show_li(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        //按照播放频率在固定位置循环投放
        int i = ADs.Count;

        int[,] Freq = new int[i, 2];
        int adv_id = 0;
        int j, Min_ID, Min_Freq;
        j = 0;
        if (i == 1)
        {
            if (Propertys == "" || (Propertys != "" && ADs[0].Ad_IsContain == 1 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && ADs[0].Ad_IsContain == 0 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                adv_id = ADs[0].Ad_ID;
                Freq[0, 0] = adv_id;
                Freq[0, 1] = 0;
            }
        }
        else
        {
            foreach (ADInfo entity in ADs)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    if (entity.Ad_Show_Freq > 0)
                    {
                        Freq[j, 0] = entity.Ad_ID;
                        Freq[j, 1] = entity.Ad_Show_times / entity.Ad_Show_Freq;
                    }
                    else
                    {
                        Freq[j, 0] = 99999999;
                        Freq[j, 1] = 99999999;
                    }
                    //j++;
                }
                else
                {
                    Freq[j, 0] = 99999999;
                    Freq[j, 1] = 99999999;
                }
                j++;
            }
            Min_ID = Freq[0, 0];
            Min_Freq = Freq[0, 1];

            for (j = 1; j < i; j++)
            {
                if (Min_Freq > Freq[j, 1] && Freq[j, 1] >= 0)
                {
                    Min_ID = Freq[j, 0];
                    Min_Freq = Freq[j, 1];
                }
            }
            adv_id = Min_ID;
        }

        foreach (ADInfo entity in ADs)
        {
            //if (entity.Ad_ID == adv_id)
            //{
                if (entity.Ad_MediaKind == 2)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        Ad_Show_Code += "<li><a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                        if (Ad_Width > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                        }
                        if (Ad_Height > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                        }
                        Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a></li>";
                    }
                }
                else if (entity.Ad_MediaKind == 3)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        }

                        Ad_Show_Code = Ad_Show_Code + "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" ";
                        if (Ad_Width > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                        }
                        if (Ad_Height > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                        }
                        Ad_Show_Code = Ad_Show_Code + "><param name=\"movie\" value=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\"><param name=\"quality\" value=\"high\"><param name=\"wmode\" value=\"opaque\"></object>";
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = Ad_Show_Code + "</a>";
                        }
                    }
                }
                else if (entity.Ad_MediaKind == 4)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        }

                        Ad_Show_Code = Ad_Show_Code + entity.Ad_Media;
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = Ad_Show_Code + "</a>";
                        }
                    }
                }
            //}
        }
        return Ad_Show_Code;
    }

    public string AD_Cycle_ShowText(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        //按照播放频率在固定位置循环投放
        int i = ADs.Count;

        int[,] Freq = new int[i, 2];
        int adv_id = 0;
        int j, Min_ID, Min_Freq;
        j = 0;
        if (i == 1)
        {
            if (Propertys == "" || (Propertys != "" && ADs[0].Ad_IsContain == 1 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && ADs[0].Ad_IsContain == 0 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                adv_id = ADs[0].Ad_ID;
                Freq[0, 0] = adv_id;
                Freq[0, 1] = 0;
            }
        }
        else
        {
            foreach (ADInfo entity in ADs)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    if (entity.Ad_Show_Freq > 0)
                    {
                        Freq[j, 0] = entity.Ad_ID;
                        Freq[j, 1] = entity.Ad_Show_times / entity.Ad_Show_Freq;
                    }
                    else
                    {
                        Freq[j, 0] = 99999999;
                        Freq[j, 1] = 99999999;
                    }
                }
                else
                {
                    Freq[j, 0] = 99999999;
                    Freq[j, 1] = 99999999;
                }
                j++;
            }
            Min_ID = Freq[0, 0];
            Min_Freq = Freq[0, 1];

            for (j = 1; j < i; j++)
            {
                if (Min_Freq > Freq[j, 1] && Freq[j, 1] >= 0)
                {
                    Min_ID = Freq[j, 0];
                    Min_Freq = Freq[j, 1];
                }
            }
            adv_id = Min_ID;
        }

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_ID == adv_id)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + entity.Ad_Title + "</a>";
                }
            }
        }
        return Ad_Show_Code;
    }

    public string AD_Keyword_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        RBACUserInfo UserInfo = pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01");
        string Ad_Show_Code = "";
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                WebAD.Adv_Show_Times_Add(entity.Ad_ID, UserInfo);

                Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                Ad_Show_Code = Ad_Show_Code + entity.Ad_Title + "</a>";
            }

        }
        return Ad_Show_Code;
    }

    /// <summary>
    /// 首页banner
    /// </summary>
    /// <param name="ADs"></param>
    /// <param name="sys_Install_Path"></param>
    /// <param name="Propertys"></param>
    /// <param name="Ad_Width"></param>
    /// <param name="Ad_Height"></param>
    /// <returns></returns>
    public string AD_Scroll2(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";

        int show_time_ad;

        RBACUserInfo userInfo = pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01");
        int i = 0;
        Ad_Show_Code = Ad_Show_Code + "<div id=\"banner_tabs\" class=\"flexslider\">";

        Ad_Show_Code = Ad_Show_Code + "<ul class=\"slides clearfix\">";
        foreach (ADInfo entity in ADs)
        {

            if (entity.Ad_MediaKind == 2)
            {
                i++;
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, userInfo);

                    Ad_Show_Code = Ad_Show_Code + "<li>";
                    Ad_Show_Code = Ad_Show_Code + "<a  href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img width='100%' style=\"background: url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ") no-repeat center;\" src=\"/images/alpha.png\"></a></li>";
                    //src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "
                }
            }
        }
        Ad_Show_Code = Ad_Show_Code + "</ul>";
        Ad_Show_Code = Ad_Show_Code + " <ul class=\"flex-direction-nav\"><li><a class=\"flex-prev\" href=\"javascript:;\">Previous</a></li><li><a class=\"flex-next\" href=\"javascript:;\">Next</a></li></ul>";
        Ad_Show_Code = Ad_Show_Code + "<ol id=\"bannerCtrl\" class=\"flex-control-nav flex-control-paging\">";
        for (int ii = 0; ii < i;)
        {
            ii++;
            if (ii == 1)
            {
                Ad_Show_Code = Ad_Show_Code + "<li><a>1</a></li>";
            }
            else
            {
                Ad_Show_Code = Ad_Show_Code + "<li><a>" + ii + "</a></li>";
            }
        
        }

        Ad_Show_Code = Ad_Show_Code + "</ol>";
        Ad_Show_Code = Ad_Show_Code + "</div>";

        return Ad_Show_Code;
    }

    /// <summary>
    /// 党建专题轮播
    /// </summary>
    /// <param name="ADs"></param>
    /// <param name="sys_Install_Path"></param>
    /// <param name="Propertys"></param>
    /// <param name="Ad_Width"></param>
    /// <param name="Ad_Height"></param>
    /// <returns></returns>
    public string AD_SpecialScroll(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";

        int show_time_ad;

        RBACUserInfo userInfo = pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01");
        int i = 0;
        Ad_Show_Code = Ad_Show_Code + "<div class=\"news-banner\" style=\"width:550px;height:410px;margin-top:0;\">";
        Ad_Show_Code = Ad_Show_Code + "<div class=\"banner-wrap clearfix\" style=\"width: 550px; height: 410px;\">";
        Ad_Show_Code = Ad_Show_Code + "<ul class=\"banner clearfix\">";
        foreach (ADInfo entity in ADs)
        {

            if (entity.Ad_MediaKind == 2)
            {
                i++;
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, userInfo);

                    Ad_Show_Code = Ad_Show_Code + "<li>";
                    Ad_Show_Code = Ad_Show_Code + "<a  href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\"><h4>" + entity.Ad_Title + "</h4></a></li>";

                }
            }
        }
        Ad_Show_Code = Ad_Show_Code + "</ul>";

        Ad_Show_Code = Ad_Show_Code + "<div class=\"new-number\" style=\"bottom:10px;\">";
        for (int ii = 0; ii < i;)
        {
            ii++;
            if (ii == 1)
            {
                Ad_Show_Code = Ad_Show_Code + "<span></span>";
            }
            else
            {
                Ad_Show_Code = Ad_Show_Code + "<span></span>";
            }
        }

        Ad_Show_Code = Ad_Show_Code + "</div>";
        Ad_Show_Code = Ad_Show_Code + "</div>";
        Ad_Show_Code = Ad_Show_Code + "</div>";
        return Ad_Show_Code;
    }
}