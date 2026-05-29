using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.BLL.MEM
{
    public class Vote : IVote
    {
        protected DAL.MEM.IVote MyDAL;
        protected IRBAC RBAC;

        public Vote()
        {
            MyDAL = DAL.MEM.VoteFactory.CreateVote();
            RBAC = RBACFactory.CreateRBAC();
        }

        public virtual bool AddVote(VoteInfo entity)
        {
            return MyDAL.AddVote(entity);
        }

        public virtual bool EditVote(VoteInfo entity)
        {
            return MyDAL.EditVote(entity);
        }

        public virtual int DelVote(int ID)
        {
            return MyDAL.DelVote(ID);
        }

        public virtual int UpdateVoteNumber(int ID)
        {
            return MyDAL.UpdateVoteNumber(ID);
        }

        public virtual VoteInfo GetVoteByID(int ID)
        {
            return MyDAL.GetVoteByID(ID);
        }

        public virtual VoteInfo GetVoteBySN(string SN)
        {
            return MyDAL.GetVoteBySN(SN);
        }

        public virtual IList<VoteInfo> GetVotes(QueryInfo Query)
        {
            return MyDAL.GetVotes(Query);
        }

        public virtual PageInfo GetVotePageInfo(QueryInfo Query)
        {
            return MyDAL.GetVotePageInfo(Query);
        }

        public virtual bool AddVoteSelect(VoteSelectInfo entity)
        {
            return MyDAL.AddVoteSelect(entity);
        }

        public virtual bool EditVoteSelect(VoteSelectInfo entity)
        {
            return MyDAL.EditVoteSelect(entity);
        }

        public virtual int DelVoteSelect(int ID)
        {
            return MyDAL.DelVoteSelect(ID);
        }

        public virtual VoteSelectInfo GetVoteSelectByID(int ID)
        {
            return MyDAL.GetVoteSelectByID(ID);
        }

        public virtual IList<VoteSelectInfo> GetVoteSelects(QueryInfo Query)
        {
            return MyDAL.GetVoteSelects(Query);
        }

        public virtual IList<VoteSelectInfo> GetVoteSelectsByVoteID(int ID)
        {
            return MyDAL.GetVoteSelectsByVoteID(ID);
        }

        public virtual bool AddVoteMember(VoteMemberInfo entity)
        {
            return MyDAL.AddVoteMember(entity);
        }

        public virtual bool EditVoteMember(VoteMemberInfo entity)
        {
            return MyDAL.EditVoteMember(entity);
        }

        public virtual int DelVoteMember(int ID)
        {
            return MyDAL.DelVoteMember(ID);
        }

        public virtual int UpdateVoteSelectNumber(int ID)
        {
            return MyDAL.UpdateVoteSelectNumber(ID);
        }
        public virtual VoteMemberInfo GetVoteMemberByID(int ID)
        {
            return MyDAL.GetVoteMemberByID(ID);
        }

        public virtual IList<VoteMemberInfo> GetVoteMembers(QueryInfo Query)
        {
            return MyDAL.GetVoteMembers(Query);
        }

        public virtual PageInfo GetVoteMemberPageInfo(QueryInfo Query)
        {
            return MyDAL.GetVoteMemberPageInfo(Query);
        }
    }
}
