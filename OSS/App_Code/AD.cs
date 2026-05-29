using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

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
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IAD MyAD;
    private IAD_Position_Channel Mychannel;
    private IADPosition Myposition;
   

    public AD()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        tools = ToolsFactory.CreateTools();
        MyAD = ADFactory.CreateAD();
        Mychannel = AD_Position_ChannelFactory.CreateAD_Position_Channel();
        Myposition = ADPositionFactory.CreateADPosition();
    }

    //添加广告频道
    public void AddAD_Position_Channel()
    {
        int AD_Position_Channel_ID = tools.CheckInt(Request.Form["AD_Position_Channel_ID"]);
        string AD_Position_Channel_Name = tools.CheckStr(Request.Form["AD_Position_Channel_Name"]);
        string AD_Position_Channel_Note = tools.CheckStr(Request.Form["AD_Position_Channel_Note"]);
        string AD_Position_Channel_Site = Public.GetCurrentSite();

        if (AD_Position_Channel_Name == "") {
            Public.Msg("error", "错误信息", "请填写频道名称", false, "{back}");
        }

        ADPositionChannelInfo entity = new ADPositionChannelInfo();
        entity.AD_Position_Channel_ID = AD_Position_Channel_ID;
        entity.AD_Position_Channel_Name = AD_Position_Channel_Name;
        entity.AD_Position_Channel_Note = AD_Position_Channel_Note;
        entity.AD_Position_Channel_Site = AD_Position_Channel_Site;

        if (Mychannel.AddAD_Position_Channel(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(41, "", "广告频道添加", AD_Position_Channel_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_Position_Channel_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(41, "", "广告频道添加", AD_Position_Channel_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改广告频道
    public void EditAD_Position_Channel()
    {

        int AD_Position_Channel_ID = tools.CheckInt(Request.Form["AD_Position_Channel_ID"]);
        string AD_Position_Channel_Name = tools.CheckStr(Request.Form["AD_Position_Channel_Name"]);
        string AD_Position_Channel_Note = tools.CheckStr(Request.Form["AD_Position_Channel_Note"]);
        string AD_Position_Channel_Site = Public.GetCurrentSite();

        if (AD_Position_Channel_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写频道名称", false, "{back}");
        }

        ADPositionChannelInfo entity = GetAD_Position_ChannelByID(AD_Position_Channel_ID);
        if (entity != null)
        {
            entity.AD_Position_Channel_ID = AD_Position_Channel_ID;
            entity.AD_Position_Channel_Name = AD_Position_Channel_Name;
            entity.AD_Position_Channel_Note = AD_Position_Channel_Note;
            entity.AD_Position_Channel_Site = AD_Position_Channel_Site;


            if (Mychannel.EditAD_Position_Channel(entity, Public.GetUserPrivilege()))
            {
                Public.AddRBACUserLog(41, entity.AD_Position_Channel_ID.ToString(), "广告频道修改", entity.AD_Position_Channel_Name, 1);
                Public.Msg("positive", "操作成功", "操作成功", true, "AD_Position_Channel.aspx");
            }
            else
            {
                Public.AddRBACUserLog(41, entity.AD_Position_Channel_ID.ToString(), "广告频道修改", entity.AD_Position_Channel_Name, 0);
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除广告频道
    public void DelAD_Position_Channel()
    {
        int cate_id = tools.CheckInt(Request.QueryString["channel_id"]);
        if (Mychannel.DelAD_Position_Channel(cate_id, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(41, cate_id.ToString(), "广告频道删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_Position_Channel.aspx");
        }
        else
        {
            Public.AddRBACUserLog(41, cate_id.ToString(), "广告频道删除", "", 1);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取频道
    public ADPositionChannelInfo GetAD_Position_ChannelByID(int channel_id)
    {
        return Mychannel.GetAD_Position_ChannelByID(channel_id, Public.GetUserPrivilege());
    }

    //选择位置频道
    public void Select_Position_Channel(string obj_name,int channel_id)
    {
        Response.Write("<select name=\"" + obj_name + "\">");
        Response.Write("<option value=\"0\">选择频道</option>");
        
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionChannelInfo.AD_Position_Channel_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionChannelInfo.AD_Position_Channel_ID", "Desc"));
        IList<ADPositionChannelInfo> Channels = Mychannel.GetAD_Position_Channels(Query, Public.GetUserPrivilege());
        if (Channels != null)
        {
            
            foreach (ADPositionChannelInfo entity in Channels)
            {
                if(entity.AD_Position_Channel_ID==channel_id)
                {
                    Response.Write("<option value=\""+entity.AD_Position_Channel_ID+"\" selected>"+entity.AD_Position_Channel_Name+"</option>");
                }
                else
                {
                    Response.Write("<option value=\""+entity.AD_Position_Channel_ID+"\">"+entity.AD_Position_Channel_Name+"</option>");
                }
            }
            
        }
        Response.Write("</select>");
    }

    //获取广告频道
    public string GetAdPositionChannels()
    {


        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionChannelInfo.AD_Position_Channel_Site", "=", Public.GetCurrentSite()));
        string keyword = tools.CheckStr(Request["keyword"]);

        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionChannelInfo.AD_Position_Channel_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Mychannel.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADPositionChannelInfo> Channels = Mychannel.GetAD_Position_Channels(Query, Public.GetUserPrivilege());
        if (Channels != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ADPositionChannelInfo entity in Channels)
            {
                jsonBuilder.Append("{\"ADPositionChannelInfo.AD_Position_Channel_ID\":" + entity.AD_Position_Channel_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.AD_Position_Channel_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.AD_Position_Channel_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("c6dba721-72aa-4ca4-86fe-2306566e17eb"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_position_channel_edit.aspx?channel_id=" + entity.AD_Position_Channel_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("9adc558d-446c-41cc-a092-bd1313d855e8"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_position_channel_do.aspx?action=move&channel_id=" + entity.AD_Position_Channel_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else
        {
            return null;
        }
    }

    //添加广告位置
    public void AddAD_Position()
    {
        int Ad_Position_ID = tools.CheckInt(Request.Form["Ad_Position_ID"]);
        string Ad_Position_Name = tools.CheckStr(Request.Form["Ad_Position_Name"]);
        int Ad_Position_ChannelID = tools.CheckInt(Request.Form["Ad_Position_ChannelID"]);
        string Ad_Position_Value = tools.CheckStr(Request.Form["Ad_Position_Value"]);
        int Ad_Position_Width = tools.CheckInt(Request.Form["Ad_Position_Width"]);
        int Ad_Position_Height = tools.CheckInt(Request.Form["Ad_Position_Height"]);
        int Ad_Position_IsActive = tools.CheckInt(Request.Form["Ad_Position_IsActive"]);
        string Ad_Position_Site = Public.GetCurrentSite();

        if (Ad_Position_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写位置名称", false, "{back}");
        }
        if (Ad_Position_Value == "")
        {
            Public.Msg("error", "错误信息", "请填写位置代号", false, "{back}");
        }

        ADPositionInfo entity = new ADPositionInfo();
        entity.Ad_Position_ID = Ad_Position_ID;
        entity.Ad_Position_Name = Ad_Position_Name;
        entity.Ad_Position_ChannelID = Ad_Position_ChannelID;
        entity.Ad_Position_Value = Ad_Position_Value;
        entity.Ad_Position_Width = Ad_Position_Width;
        entity.Ad_Position_Height = Ad_Position_Height;
        entity.Ad_Position_IsActive = Ad_Position_IsActive;
        entity.Ad_Position_Site = Ad_Position_Site;

        if (Myposition.AddAD_Position(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(41, "", "广告位置添加", Ad_Position_Name, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "ad_position_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(41, "", "广告位置添加", Ad_Position_Name, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改广告位置
    public void EditAD_Position()
    {

        int Ad_Position_ID = tools.CheckInt(Request.Form["Ad_Position_ID"]);
        string Ad_Position_Name = tools.CheckStr(Request.Form["Ad_Position_Name"]);
        int Ad_Position_ChannelID = tools.CheckInt(Request.Form["Ad_Position_ChannelID"]);
        string Ad_Position_Value = tools.CheckStr(Request.Form["Ad_Position_Value"]);
        int Ad_Position_Width = tools.CheckInt(Request.Form["Ad_Position_Width"]);
        int Ad_Position_Height = tools.CheckInt(Request.Form["Ad_Position_Height"]);
        int Ad_Position_IsActive = tools.CheckInt(Request.Form["Ad_Position_IsActive"]);
        string Ad_Position_Site = Public.GetCurrentSite();

        if (Ad_Position_Name == "")
        {
            Public.Msg("error", "错误信息", "请填写位置名称", false, "{back}");
        }
        if (Ad_Position_Value == "")
        {
            Public.Msg("error", "错误信息", "请填写位置代号", false, "{back}");
        }

        ADPositionInfo entity = GetAD_PositionByID(Ad_Position_ID);
        if (entity != null)
        {
            entity.Ad_Position_ID = Ad_Position_ID;
            entity.Ad_Position_Name = Ad_Position_Name;
            entity.Ad_Position_ChannelID = Ad_Position_ChannelID;
            entity.Ad_Position_Value = Ad_Position_Value;
            entity.Ad_Position_Width = Ad_Position_Width;
            entity.Ad_Position_Height = Ad_Position_Height;
            entity.Ad_Position_IsActive = Ad_Position_IsActive;
            entity.Ad_Position_Site = Ad_Position_Site;


            if (Myposition.EditAD_Position(entity, Public.GetUserPrivilege()))
            {
                Public.AddRBACUserLog(41, entity.Ad_Position_ID.ToString(), "广告位置修改", entity.Ad_Position_Name, 1);
                Public.Msg("positive", "操作成功", "操作成功", true, "ad_position.aspx");
            }
            else
            {
                Public.AddRBACUserLog(41, entity.Ad_Position_ID.ToString(), "广告位置修改", entity.Ad_Position_Name, 0);
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除广告位置
    public void DelAD_Position()
    {
        int position_id = tools.CheckInt(Request.QueryString["position_id"]);
        if (Myposition.DelAD_Position(position_id, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(41, position_id.ToString(), "广告位置删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "ad_position.aspx");
        }
        else
        {
            Public.AddRBACUserLog(41, position_id.ToString(), "广告位置删除", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取广告位置
    public ADPositionInfo GetAD_PositionByID(int cate_id)
    {
        return Myposition.GetAD_PositionByID(cate_id, Public.GetUserPrivilege());
    }

    //获取广告位置
    public string GetAdPositions()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int Ad_Channel = tools.CheckInt(Request["Ad_Channel"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        if (Ad_Channel > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADPositionInfo.Ad_Position_ChannelID", "=", Ad_Channel.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ADPositionInfo.Ad_Position_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ADPositionInfo.Ad_Position_Value", "like", keyword));
        }
        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Myposition.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ADPositionInfo entity in Positions)
            {
                jsonBuilder.Append("{\"ADPositionInfo.AD_Position_ID\":" + entity.Ad_Position_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Position_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Position_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Position_Value));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ADPositionChannelInfo channel = Mychannel.GetAD_Position_ChannelByID(entity.Ad_Position_ChannelID, Public.GetUserPrivilege());
                if (channel != null)
                {
                    jsonBuilder.Append(channel.AD_Position_Channel_Name);
                }
                else
                {
                    jsonBuilder.Append("&nbsp;");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Position_Width);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Position_Height);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("afbc3245-62b5-4eb3-aefb-c6c8f3e2b02d"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_position_edit.aspx?position_id=" + entity.Ad_Position_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("67c30881-650c-4f84-aa81-08e2e379798c"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_position_do.aspx?action=move&position_id=" + entity.Ad_Position_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else
        {
            return null;
        }
    }

    //根据位置代码获取广告频道
    public int GetAdPositionByKind(string kind)
    {
        int Ad_Position_ChannelID = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Value", "=", kind));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));

        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            foreach (ADPositionInfo entity in Positions)
            {
                Ad_Position_ChannelID = entity.Ad_Position_ChannelID;
            }
        }
        return Ad_Position_ChannelID;
    }

    //选择广告位置
    public void Select_AD_Position(string obj_name, string  position_value)
    {
        Response.Write("<select name=\"" + obj_name + "\">");
        Response.Write("<option value=\"\">选择位置</option>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));
        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {

            foreach (ADPositionInfo entity in Positions)
            {
                if (entity.Ad_Position_Value == position_value)
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\" selected>" + entity.Ad_Position_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\">" + entity.Ad_Position_Name + "</option>");
                }
            }

        }
        Response.Write("</select>");
    }

    //广告添加
    public void AddAD()
    {
        int Ad_ID = tools.CheckInt(Request.Form["Ad_ID"]);
        string Ad_Title = tools.CheckStr(Request.Form["Ad_Title"]);
        string Ad_Kind = tools.CheckStr(Request.Form["Ad_Kind"]);
        int Ad_MediaKind = tools.CheckInt(Request.Form["Ad_MediaKind"]);
        string Ad_Media = tools.CheckStr(Request.Form["Ad_Media"]);
        string Ad_Mediacode = Request.Form["Ad_Mediacode"];
        string Ad_Link = Request.Form["Ad_Link"];
        int Ad_Show_Freq = tools.CheckInt(Request.Form["Ad_Show_Freq"]);
        int Ad_Show_times = 0;
        int Ad_Hits = 0;
        DateTime Ad_StartDate = tools.NullDate(Request.Form["Ad_StartDate"]);
        DateTime Ad_EndDate = tools.NullDate(Request.Form["Ad_EndDate"]);
        int Ad_IsContain = tools.CheckInt(Request.Form["Ad_IsContain"]);
        string Ad_Propertys = tools.CheckStr(Request.Form["Ad_Propertys"]);
        int Ad_Sort = tools.CheckInt(Request.Form["Ad_Sort"]);
        int Ad_IsActive = tools.CheckInt(Request.Form["Ad_IsActive"]);
        string Ad_Site = Public.GetCurrentSite();

        if(Ad_MediaKind == 4)
        {
            Ad_Media=Ad_Mediacode;
        }
        if (Ad_MediaKind == 1)
        {
            Ad_Media = "";
        }

        if (Ad_Title == "")
        {
            Public.Msg("error", "错误信息", "请填写广告名称", false, "{back}");
        }
        if (Ad_Kind == "")
        {
            Public.Msg("error", "错误信息", "请选择广告位置", false, "{back}");
        }
        if (Request.Form["Ad_EndDate"] == "" || Request.Form["Ad_EndDate"] == "")
        {
            Public.Msg("error", "错误信息", "请选择广告起止时间", false, "{back}");
        }
        if ((Ad_MediaKind >1 ) && Ad_Media=="")
        {
            Public.Msg("error", "错误信息", "请设置广告媒体", false, "{back}");
        }

        Ad_Propertys = "|" + Ad_Propertys + "|";
        

        ADInfo entity = new ADInfo();
        entity.Ad_ID = Ad_ID;
        entity.Ad_Title = Ad_Title;
        entity.Ad_Kind = Ad_Kind;
        entity.Ad_MediaKind = Ad_MediaKind;
        entity.Ad_Media = Ad_Media;
        entity.Ad_Link = Ad_Link;
        entity.Ad_Show_Freq = Ad_Show_Freq;
        entity.Ad_Show_times = Ad_Show_times;
        entity.Ad_Hits = Ad_Hits;
        entity.Ad_StartDate = Ad_StartDate;
        entity.Ad_EndDate = Ad_EndDate;
        entity.Ad_IsContain = Ad_IsContain;
        entity.Ad_Propertys = Ad_Propertys;
        entity.Ad_Sort = Ad_Sort;
        entity.Ad_IsActive = Ad_IsActive;
        entity.Ad_Site = Ad_Site;

        if (MyAD.AddAD(entity, Public.GetUserPrivilege()))
        {
            Public.AddRBACUserLog(41, "", "广告添加", Ad_Title, 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "AD_add.aspx");
        }
        else
        {
            Public.AddRBACUserLog(41, "", "广告修改", Ad_Title, 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //广告修改
    public void EditAD()
    {

        int Ad_ID = tools.CheckInt(Request.Form["Ad_ID"]);
        string Ad_Title = tools.CheckStr(Request.Form["Ad_Title"]);
        string Ad_Kind = tools.CheckStr(Request.Form["Ad_Kind"]);
        int Ad_MediaKind = tools.CheckInt(Request.Form["Ad_MediaKind"]);
        string Ad_Media = tools.CheckStr(Request.Form["Ad_Media"]);
        string Ad_Mediacode = Request.Form["Ad_Mediacode"];
        string Ad_Link = Request.Form["Ad_Link"];
        int Ad_Show_Freq = tools.CheckInt(Request.Form["Ad_Show_Freq"]);
        int Ad_Show_times = tools.CheckInt(Request.Form["Ad_Show_times"]);
        int Ad_Hits = tools.CheckInt(Request.Form["Ad_Hits"]);
        DateTime Ad_StartDate = tools.NullDate(Request.Form["Ad_StartDate"]);
        DateTime Ad_EndDate = tools.NullDate(Request.Form["Ad_EndDate"]);
        int Ad_IsContain = tools.CheckInt(Request.Form["Ad_IsContain"]);
        string Ad_Propertys = tools.CheckStr(Request.Form["Ad_Propertys"]);
        int Ad_Sort = tools.CheckInt(Request.Form["Ad_Sort"]);
        int Ad_IsActive = tools.CheckInt(Request.Form["Ad_IsActive"]);
        string Ad_Site = Public.GetCurrentSite();

        if (Ad_MediaKind == 4)
        {
            Ad_Media = Ad_Mediacode;
        }
        if (Ad_MediaKind == 1)
        {
            Ad_Media = "";
        }

        if (Ad_Title == "")
        {
            Public.Msg("error", "错误信息", "请填写广告名称", false, "{back}");
        }
        if (Ad_Kind == "")
        {
            Public.Msg("error", "错误信息", "请选择广告位置", false, "{back}");
        }
        if (Request.Form["Ad_EndDate"] == "" || Request.Form["Ad_EndDate"] == "")
        {
            Public.Msg("error", "错误信息", "请选择广告起止时间", false, "{back}");
        }
        if ((Ad_MediaKind > 1) && Ad_Media == "")
        {
            Public.Msg("error", "错误信息", "请设置广告媒体", false, "{back}");
        }
        Ad_Propertys = "|" + Ad_Propertys + "|";

        ADInfo entity = GetADByID(Ad_ID);
        if (entity != null)
        {
            entity.Ad_ID = Ad_ID;
            entity.Ad_Title = Ad_Title;
            entity.Ad_Kind = Ad_Kind;
            entity.Ad_MediaKind = Ad_MediaKind;
            entity.Ad_Media = Ad_Media;
            entity.Ad_Link = Ad_Link;
            entity.Ad_Show_Freq = Ad_Show_Freq;
            entity.Ad_StartDate = Ad_StartDate;
            entity.Ad_EndDate = Ad_EndDate;
            entity.Ad_IsContain = Ad_IsContain;
            entity.Ad_Propertys = Ad_Propertys;
            entity.Ad_Sort = Ad_Sort;
            entity.Ad_IsActive = Ad_IsActive;
            entity.Ad_Site = Ad_Site;


            if (MyAD.EditAD(entity, Public.GetUserPrivilege()))
            {
                Public.AddRBACUserLog(41, entity.Ad_ID.ToString(), "广告修改", entity.Ad_Title, 1);
                Public.Msg("positive", "操作成功", "操作成功", true, "AD.aspx");
            }
            else
            {
                Public.AddRBACUserLog(41, entity.Ad_ID.ToString(), "广告修改", entity.Ad_Title, 0);
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //广告删除
    public void DelAD()
    {
        int AD_ID = tools.CheckInt(Request.QueryString["AD_ID"]);
        if (MyAD.DelAD(AD_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.AddRBACUserLog(41, AD_ID.ToString(), "广告删除", "", 1);
            Public.Msg("positive", "操作成功", "操作成功", true, "AD.aspx");
        }
        else
        {
            Public.AddRBACUserLog(41, AD_ID.ToString(), "广告删除", "", 0);
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 启用状态
    /// </summary>
    /// <param name="Active"></param>
    /// <returns></returns>
    public string GetADIsActive(int Active)
    {
        string Name = "--";
        switch(Active)
        {
            case 0:
                Name = "不启用";
                break;
            case 1:
                Name = "启用";
                break;
        }
        return Name;
    }

    //根据编号获取广告
    public ADInfo GetADByID(int cate_id)
    {
        return MyAD.GetADByID(cate_id, Public.GetUserPrivilege());
    }

    //获取广告
    public string GetAds()
    {
        ADPositionInfo positioninfo=null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        string Ad_Kind = tools.CheckStr(Request["Ad_Kind"]);
        if (Ad_Kind.Length >0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "=", Ad_Kind));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Title", "like", keyword));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyAD.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ADInfo> Ads = MyAD.GetADs(Query, Public.GetUserPrivilege());
        if (Ads != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ADInfo entity in Ads)
            {
                jsonBuilder.Append("{\"id\":" + entity.Ad_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_ID);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Ad_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                positioninfo= Myposition.GetAD_PositionByValue(entity.Ad_Kind, Public.GetUserPrivilege());
                if (positioninfo != null)
                {
                    jsonBuilder.Append(positioninfo.Ad_Position_Name);
                }
                else
                {
                    jsonBuilder.Append(entity.Ad_Kind);
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Ad_MediaKind == 1)
                {
                    jsonBuilder.Append("文字");
                }
                else if(entity.Ad_MediaKind==2)
                {
                jsonBuilder.Append("图片");
                }
                else if (entity.Ad_MediaKind == 3)
                {
                    jsonBuilder.Append("Flash");
                }
                else
                {
                    jsonBuilder.Append("富媒体");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Show_Freq);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Show_times);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_Hits);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Ad_StartDate.ToShortDateString() + " - " + entity.Ad_EndDate.ToShortDateString());
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetADIsActive(entity.Ad_IsActive));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("c47f67fa-1142-459d-b466-e3216848ff9c"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"ad_edit.aspx?ad_id=" + entity.Ad_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("6087aa59-bd66-4eb5-8fb0-f72da294b1ae"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('ad_do.aspx?action=move&ad_id=" + entity.Ad_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else
        {
            return null;
        }
    }

    //批量审批
    public void GetADsActive(int Active)
    {
        string ad_id = tools.CheckStr(Request["ad_id"]);
        if (ad_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的广告", false, "{back}");
            return;
        }

        if (tools.Left(ad_id, 1) == ",") { ad_id = ad_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.Ad_ID", "in", ad_id));
        Query.OrderInfos.Add(new OrderInfo("ADInfo.Ad_ID", "DESC"));

        IList<ADInfo> Ads = MyAD.GetADs(Query, Public.GetUserPrivilege());
        if(Ads!=null)
        {
            foreach(ADInfo entity in Ads)
            {
                entity.Ad_IsActive = Active;

                if (MyAD.EditAD(entity, Public.GetUserPrivilege()))
                {
                    if(Active==1)
                    {
                        Public.AddRBACUserLog(41, entity.Ad_ID.ToString(), "广告启用", entity.Ad_Title, 1);
                    }
                    else
                    {
                        Public.AddRBACUserLog(41, entity.Ad_ID.ToString(), "广告不启用", entity.Ad_Title, 1);
                    }
                    

                }
                else
                {
                    if (Active == 1)
                    {
                        Public.AddRBACUserLog(41, entity.Ad_ID.ToString(), "广告启用", entity.Ad_Title, 0);
                    }
                    else
                    {
                        Public.AddRBACUserLog(41, entity.Ad_ID.ToString(), "广告不启用", entity.Ad_Title, 0);
                    }
                }

            }
        }

        Response.Redirect("/ad/ad.aspx");
    }

    //根据频道获取广告位置代码
    public string GetADKinds(int id)
    {
        string ad_kinds = "'0'";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_ChannelID", "=", id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));

        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {
            foreach (ADPositionInfo entity in Positions)
            {
                ad_kinds += "," + "'" + entity.Ad_Position_Value + "'";
            }
        }
        return ad_kinds;

    }

    //添广告频道再添位置
    public string AD_Position_Select(int value)
    {
        string select_list = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        IList<ADPositionChannelInfo> adpositionchannelinfos = Mychannel.GetAD_Position_Channels(Query, Public.GetUserPrivilege());
        if (adpositionchannelinfos != null)
        {
            select_list += "<select name=\"adpositionchannel\" id=\"adpositionchannel\" onchange=\"getChannelID(this.options[this.selectedIndex].value);\">";
            select_list += "<option value=\"0\">选择频道</option>";
            foreach (ADPositionChannelInfo entity in adpositionchannelinfos)
            {
                if (value == entity.AD_Position_Channel_ID)
                {
                    select_list += "<option value=\"" + entity.AD_Position_Channel_ID + "\" selected>" + entity.AD_Position_Channel_Name + "</option>";
                }
                else
                {
                    select_list += "<option value=\"" + entity.AD_Position_Channel_ID + "\">" + entity.AD_Position_Channel_Name + "</option>";
                }
            }
            select_list += "</select>";
        }
        return select_list;
    }

    public void Select_AD_Position1(string obj_name, string position_value, int channel_id)
    {
        Response.Write("<select name=\"" + obj_name + "\">");
        Response.Write("<option value=\"\">选择位置</option>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_ChannelID", "=", channel_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "Desc"));
        IList<ADPositionInfo> Positions = Myposition.GetADPositions(Query, Public.GetUserPrivilege());
        if (Positions != null)
        {

            foreach (ADPositionInfo entity in Positions)
            {
                if (entity.Ad_Position_Value == position_value)
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\" selected>" + entity.Ad_Position_Name + "(" + entity.Ad_Position_Width + "*" + entity.Ad_Position_Height + ")</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Ad_Position_Value + "\">" + entity.Ad_Position_Name + "(" + entity.Ad_Position_Width + "*" + entity.Ad_Position_Height + ")</option>");
                }
            }

        }
        Response.Write("</select>");
    }

    public string AD_Show(string AD_Position, string Propertys, string DisplayStyle, int Col_Num)
    {
        string sys_Install_Path = "/AD/ad.aspx";
        string Ad_String = "";
        ADPositionInfo Positioninfo = Myposition.GetAD_PositionByValue(AD_Position, Public.GetUserPrivilege());
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
                IList<ADInfo> ADs = MyAD.GetADs(Query, Public.GetUserPrivilege());
                if (ADs != null)
                {
                    switch (DisplayStyle)
                    {
                        

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

        //RBACUserInfo userInfo = pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01");
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
                    //show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, userInfo);

                    Ad_Show_Code = Ad_Show_Code + "<li>";
                    Ad_Show_Code = Ad_Show_Code + "<a  href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + Public.FormatImgURL(entity.Ad_Media, "fullpath") + "\"><h4>" + entity.Ad_Title + "</h4></a></li>";

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
