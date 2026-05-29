using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public class AboutFactory
    {
        public static IAbout CreateAbout()
        {
            string path = ConfigurationManager.AppSettings["DALCMS"].ToString();
            string classname = path + ".About";
            return (IAbout)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
