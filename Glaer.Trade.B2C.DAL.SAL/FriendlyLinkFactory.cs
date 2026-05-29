using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.SAL
{
    public class FriendlyLinkFactory
    {
        public static IFriendlyLinkCate CreateFriendlyLinkCate()
        {
            string path = ConfigurationManager.AppSettings["DALSAL"].ToString();
            string classname = path + ".FriendlyLinkCate";
            return (IFriendlyLinkCate)Assembly.Load(path).CreateInstance(classname);
        }

        public static IFriendlyLink CreateFriendlyLink()
        {
            string path = ConfigurationManager.AppSettings["DALSAL"].ToString();
            string classname = path + ".FriendlyLink";
            return (IFriendlyLink)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
