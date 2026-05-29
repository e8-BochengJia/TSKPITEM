using System;
using System.Reflection;
using System.Configuration;

namespace Glaer.Trade.B2C.BLL.CMS
{
    public class ArticleFactory
    {
        public static IArticleCate CreateArticleCate()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".ArticleCate";
            return (IArticleCate)Assembly.Load(path).CreateInstance(classname);
        }

        public static IArticle CreateArticle()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".Article";
            return (IArticle)Assembly.Load(path).CreateInstance(classname);
        }
        public static IArticleSubject CreateArticleSubject()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".ArticleSubject";
            return (IArticleSubject)Assembly.Load(path).CreateInstance(classname);
        }


    }


    public class SensitiveWordsFactory
    {
        public static ISensitiveWords CreateSensitiveWords()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".SensitiveWords";
            return (ISensitiveWords)Assembly.Load(path).CreateInstance(classname);
        }

    }

    public class SpecialFactory
    {
        public static ISpecial CreateSpecial()
        {
            string path = ConfigurationManager.AppSettings["BLLCMS"];
            string classname = path + ".Special";
            return (ISpecial)Assembly.Load(path).CreateInstance(classname);
        }

    }
   

}
