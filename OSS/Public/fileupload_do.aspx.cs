using System;
using System.Web;
using System.Drawing;
using System.IO;

public partial class fileupload_do : System.Web.UI.Page
{
    string app, formname, frmelement, rtvalue, rturl;

    string AllowFileType = ".xlsx|.xls";
    int AllowFileLength = 1024 * 1024 * 3; //上传上限为3M

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.ContentType = "multipart/form-data";
        //Response.Expires = -1;

        app = Request["app"];
        formname = Request["formname"];
        frmelement = Request["frmelement"];
        rtvalue = Request["rtvalue"];
        rturl = Request["rturl"];

        //定义上传路径
        string path_http = "";
        string path = Request.MapPath("/");

        //上传文件信息
        HttpPostedFile PostedFile = Request.Files["File1"];
        if (PostedFile.ContentLength == 0)
        {
            Response.Redirect("fileupload.aspx?" + Request.QueryString.ToString());
        }

        string fileName = MakeFileName();
        string fileExtend = PostedFile.FileName.Substring(PostedFile.FileName.LastIndexOf('.')).ToLower();
        int fileLength = PostedFile.ContentLength;
        string fileFullName = string.Empty;

        if (!FileValidate(fileExtend, AllowFileType))
        {
            Response.Write("<div class=\"errormsg\">error_filetype</div>");
            return;
        }

        if (fileLength > AllowFileLength)
        {
            Response.Write("<div class=\"errormsg\">error_exceedlimit</div>");
            return;
        }
        switch (app)
        {
            case "mobile_batch_number_import":
                #region 手机号码批量导入
                path_http += "/ImportTemplate_TempFile/";
                path += "\\ImportTemplate_TempFile\\";
                fileFullName = fileName + fileExtend;

                PostedFile.SaveAs(path + fileFullName);

                Response.Write("<script type=\"text/javascript\">");
                Response.Write("parent.document." + formname + "." + frmelement + ".value='" + path_http + fileFullName + "';");
                Response.Write("</script>");
                Response.Write("<div class=\"successmsg\">上传成功！</div>");
                break;
                #endregion
            default:
                Response.Write("<div class=\"errormsg\">error_invaildapp</div>");
                break;
        }
    }

    /// <summary>
    /// 生成文件名称
    /// </summary>
    /// <returns></returns>
    private string MakeFileName()
    {
        Random ran = new Random();
        int intstr = (int)ran.Next(9);
        return DateTime.Now.ToString("yyyyMMddHHmmss") + intstr.ToString();
    }

    /// <summary>
    /// 检查文件格式
    /// </summary>
    /// <param name="fileExt">文件格式</param>
    /// <returns></returns>
    public bool FileValidate(string fileExt, string allowType)
    {
        bool FileGood = false;
        string[] Exts = allowType.Split('|');
        foreach (string FileType in Exts)
        {
            if (FileType == fileExt) { FileGood = true; break; }
        }
        return FileGood;
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath"></param>
    private void DelectFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }
}
