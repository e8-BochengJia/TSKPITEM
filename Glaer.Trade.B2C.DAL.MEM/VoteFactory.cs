using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class VoteFactory
    {
        public static IVote CreateVote()
        {
            string path = ConfigurationManager.AppSettings["DALMEM"];
            string classname = path + ".Vote";
            return (IVote)Assembly.Load(path).CreateInstance(classname);
        }

    }
}
