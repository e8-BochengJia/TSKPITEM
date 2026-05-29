using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public class NoticeFactory
    {
        public static INoticeCate CreateNoticeCate()
        {
            string path = ConfigurationManager.AppSettings["DALCMS"];
            string classname = path + ".NoticeCate";
            return (INoticeCate)Assembly.Load(path).CreateInstance(classname);
        }

        public static INotice CreateNotice()
        {
            string path = ConfigurationManager.AppSettings["DALCMS"];
            string classname = path + ".Notice";
            return (INotice)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
