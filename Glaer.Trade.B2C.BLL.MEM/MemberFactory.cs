using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class MemberFactory
    {
        public static IMember CreateMember()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"].ToString();
            string classname = path + ".Member";
            return (IMember)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class MemberLogFactory
    {
        public static IMemberLog CreateMemberLog()
        {
            string path = ConfigurationManager.AppSettings["BLLMEM"];
            string classname = path + ".MemberLog";
            return (IMemberLog)Assembly.Load(path).CreateInstance(classname);
        }

    }

}
