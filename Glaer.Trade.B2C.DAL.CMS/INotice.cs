using System;
using System.Collections.Generic;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;

namespace Glaer.Trade.B2C.DAL.CMS
{
    public interface INoticeCate
    {
        bool AddNoticeCate(NoticeCateInfo entity);

        bool EditNoticeCate(NoticeCateInfo entity);

        int DelNoticeCate(int ID);

        NoticeCateInfo GetNoticeCateByID(int ID);

        IList<NoticeCateInfo> GetNoticeCates(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }

    public interface INotice
    {
        bool AddNotice(NoticeInfo entity);

        bool EditNotice(NoticeInfo entity);

        int DelNotice(int ID);

        NoticeInfo GetNoticeByID(int ID);

        IList<NoticeInfo> GetNotices(QueryInfo Query);

        IList<NoticeInfo> GetNoticeList(QueryInfo Query);

        PageInfo GetPageInfo(QueryInfo Query);
    }
}
