using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.CMS
{
   
    public class QuestionFactory
    {
        public static IQuestion CreateQuestion()
        {
            string path = ConfigurationManager.AppSettings["DALCMS"];
            string classname = path + ".Question";
            return (IQuestion)Assembly.Load(path).CreateInstance(classname);
        }

    }
   
}
