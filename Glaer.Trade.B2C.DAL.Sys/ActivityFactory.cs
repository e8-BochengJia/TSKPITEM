using System.Configuration;
using System.Reflection;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class ActivityUserFactory
    {
        public static IActivityUser CreateActivityUser()
        {
            string path = ConfigurationManager.AppSettings["DALActivity"];
            string classname = path + ".ActivityUser";
            return (IActivityUser)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
