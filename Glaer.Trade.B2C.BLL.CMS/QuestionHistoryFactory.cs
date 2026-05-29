using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.CMS
{
  
    public class QuestionHistoryFactory
    {
        public static IQuestionHistory CreateQuestionHistory()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".QuestionHistory";
            return (IQuestionHistory)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
