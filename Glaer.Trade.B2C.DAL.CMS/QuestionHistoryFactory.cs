using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.CMS
{
   
   
    public class QuestionHistoryFactory
    {
        public static IQuestionHistory CreateQuestionHistory()
        {
            string path = ConfigurationManager.AppSettings["DALCMS"];
            string classname = path + ".QuestionHistory";
            return (IQuestionHistory)Assembly.Load(path).CreateInstance(classname);
        }

    }
   
}
