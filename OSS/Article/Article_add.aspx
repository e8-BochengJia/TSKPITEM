<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<script runat="server">

    private ArticleCate myAppC;
    private ArticleSubject mySubject;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Public.CheckLogin("870e6332-ab75-41cc-98c3-17e8af7827d3");
        mySubject = new ArticleSubject();
        myAppC = new ArticleCate();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Public/u/ueditor.config.js?v=2" type="text/javascript"></script>
    <script src="/Public/u/ueditor.all.min.js?v=2" type="text/javascript"></script>
    <script src="/Public/u/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
        <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
        <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
    <script type="text/javascript">


        function editoradd(value, id) {
            UE.getEditor('Article_Content').execCommand('insertHtml', value)
        }        function Dowritefrom(value)        {
            $("#Article_Source").val(value);        }</script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    添加文章
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    
                    <form id="formadd" name="formadd" method="post" action="Article_do.aspx" onsubmit="MM_findObj('Article_CateIDs').value = tree.getAllChecked();">
                    <div class="opt_content">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table"
                            id="frm_optitem_1">
                            <tr>
                                <td class="cell_title">
                                    文章标题
                                </td>
                                <td class="cell_content">
                                    <input name="Article_Title" type="text" id="Article_Title" size="50" maxlength="50" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    文章类别
                                </td>
                                <td class="cell_content">
                                    <span id="main_cate">
                                        <%=myAppC.Article_Category_Select(0, "main_cate")%></span>
                                </td>
                            </tr>
                              <tr>
                                        <td class="cell_title">
                                            附加类别
                                        </td>
                                        <td class="cell_content">
                                            <div id="treeboxbox_tree">
                                            </div>
                                        </td>
                                        <script type="text/javascript">
                                            tree = new dhtmlXTreeObject("treeboxbox_tree", "100%", "100%", 0);
                                            tree.setSkin('dhx_skyblue');
                                            tree.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                                            tree.enableCheckBoxes(1);
                                            tree.enableThreeStateCheckboxes(true);
                                            tree.loadXML("treedata.aspx");
                                        </script>
                                        <span id="div_Article_CateIDs"></span>
                                    </tr>
                                 <tr>
                                        <td class="cell_title" valign="top">
                                            所属专题
                                        </td>
                                        <td class="cell_content">
                                            <%=mySubject.GetArticleSubjectSelect(0)%>
                                            <span class="tip">&nbsp;&nbsp;为文章选择专题</span>
                                        </td>
                                    </tr>
                            <tr>
          <td class="cell_title">文章来源</td>
          <td class="cell_content"><input name="Article_Source" type="text" id="Article_Source" size="50" maxlength="50" />
            
              <select name="select3" onchange="Dowritefrom(this.options[this.selectedIndex].value)">
          <option value="" selected=""></option>
          <option value="新华网">新华网</option><option value="新浪网">新浪网</option><option value="唐山科普在线">唐山科普在线</option><option value="普及部">普及部</option><option value="普及部">普及部</option><option value="39健康网">39健康网</option><option value="科技日报">科技日报</option><option value="河北科技新闻网">河北科技新闻网</option><option value="唐山市大学生科普志愿者管理办公室">唐山市大学生科普志愿者管理办公室</option><option value="果壳网">果壳网</option><option value="科普中国">科普中国</option>
        </select>

          </td>
        </tr>
        <tr>
          <td class="cell_title">文章作者</td>
          <td class="cell_content"><input name="Article_Author" type="text" id="Article_Author" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">文章摘要</td>
          <td class="cell_content"><textarea name="Article_Intro" id="Article_Intro" cols="50" rows="5"></textarea></td>
        </tr>
                            <tr>
                                <td class="cell_title">
                                    预览图片
                                </td>
                                <td class="cell_content">
                                    <iframe id="iframe2" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=article&formname=formadd&frmelement=Article_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                        width="100%" height="35" frameborder="0" scrolling="no"></iframe>
                                    <span class="tip">建议上传宽：高比例=1.5:1</span>
                                </td>
                            </tr>
                            <tr id="tr_Article_Img" style="display: none">
                                <td class="cell_title">
                                </td>
                                <td class="cell_content">
                                    <img src="" id="img_Article_Img" /><input name="Article_Img" type="hidden" id="Article_Img" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    上传图片
                                </td>
                                <td class="cell_content">
                                    <iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=content&formname=formadd&frmelement=Article_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                        width="100%" height="35" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>
                                <tr>
                                <td class="cell_title">
                                    上传附件
                                </td>
                                <td class="cell_content">
                                    <iframe id="iframe3" src="<% =Application["upload_server_url"]%>/public/FileUpload.aspx?App=contentfile&formname=formadd&frmelement=Article_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>"
                                        width="100%" height="35" frameborder="0" scrolling="no"></iframe>  <span class="tip">支持格式： .jpg|.gif|.png|.swf|.rar|.zip|.pdf|.xls|.jpeg|.xlsx|.doc|.docx|.txt</span>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">
                                    文章内容
                                </td>
                                <td class="cell_content" id="Article00">
                                    <textarea cols="80" id="Article_Content" name="Article_Content" rows="16" style="width:100%" ></textarea>
                                    <script type="text/javascript">
                                        var ue = UE.getEditor('Article_Content', {
                                            allowDivTransToP: false
                                        });
                                    </script>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    TITLE<br />
                                    (页面标题)
                                </td>
                                <td class="cell_content">
                                    <input name="Article_SEO_Title" type="text" id="Article_SEO_Title" size="50" maxlength="200"  />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    META_KEYWORDS<br />
                                    (页面关键词)
                                </td>
                                <td class="cell_content">
                                    <input name="Article_SEO_Keyword" type="text" id="Article_SEO_Keyword" size="50" maxlength="200" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    META_DESCRIPTION<br />
                                    (页面描述)
                                </td>
                                <td class="cell_content">
                                    <textarea name="Article_SEO_Description" cols="50" rows="5" id="Article_SEO_Description"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    排序
                                </td>
                                <td class="cell_content">
                                    <input name="Article_Sort" type="text" id="Article_Sort" value="1" size="10" maxlength="10" />
                                    <span class="tip">数字越小越靠前</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">
                                    是否推荐阅读
                                </td>
                                <td class="cell_content">
                                    <input name="Article_IsRecommend" type="radio" id="Article_IsRecommend" value="1" />是
                                    <input type="radio" name="Article_IsRecommend" id="Article_IsRecommend1" value="0"
                                        checked="checked" />否
                                </td>
                            </tr>
                              <tr>
                                <td class="cell_title">
                                    是否列表顶部
                                </td>
                                <td class="cell_content">
                                    <input name="Artide_IsTop" type="radio" id="Artide_IsTop" value="1" />是
                                    <input type="radio" name="Artide_IsTop" id="Artide_IsTop1" value="0"
                                        checked="checked" />否
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div class="foot_gapdiv">
                    </div>
                    <div class="float_option_div" id="float_option_div">
                        <input type="hidden" id="action" name="action" value="new" />
                         <input type="hidden" name="Article_CateIDs" id="Article_CateIDs" value="" />
                        <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                        <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                            onmouseout="this.className='bt_grey';" onclick="location='Article_list.aspx';" />
                    </div>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
