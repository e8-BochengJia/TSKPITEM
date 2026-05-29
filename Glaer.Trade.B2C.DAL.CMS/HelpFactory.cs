using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public class HelpFactory
    {
        public static IHelpCate CreateHelpCate()
        { 
            string path = ConfigurationManager.AppSettings["DALCMS"].ToString();
            string classname = path + ".HelpCate";
            return (IHelpCate)Assembly.Load(path).CreateInstance(classname);
        }

        public static IHelp CreateHelp()
        {
            string path = ConfigurationManager.AppSettings["DALCMS"].ToString();
            string classname = path + ".Help";
            return (IHelp)Assembly.Load(path).CreateInstance(classname);
        }
    }
}
