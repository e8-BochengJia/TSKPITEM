using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberFactory
    {
        public static IMember CreateMember()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"].ToString();
            string classname = path + ".Member";
            return (IMember)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class MemberLogFactory
    {
        public static IMemberLog CreateMemberLog()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".MemberLog";
            return (IMemberLog)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
