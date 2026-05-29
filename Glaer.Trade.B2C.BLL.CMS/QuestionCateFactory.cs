using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.CMS
{
   
    public class QuestionCateFactory
    {
        public static IQuestionCate CreateQuestionCate()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".QuestionCate";
            return (IQuestionCate)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
