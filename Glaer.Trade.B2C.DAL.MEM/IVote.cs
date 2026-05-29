using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public interface IVote
    {
        bool AddVote(VoteInfo entity);

        bool EditVote(VoteInfo entity);

        int DelVote(int ID);

        VoteInfo GetVoteByID(int ID);

        VoteInfo GetVoteBySN(string SN);

        int UpdateVoteNumber(int ID);

        IList<VoteInfo> GetVotes(QueryInfo Query);


        PageInfo GetVotePageInfo(QueryInfo Query);


        bool AddVoteSelect(VoteSelectInfo entity);

        bool EditVoteSelect(VoteSelectInfo entity);

        int DelVoteSelect(int ID);

        VoteSelectInfo GetVoteSelectByID(int ID);

        IList<VoteSelectInfo> GetVoteSelects(QueryInfo Query);

        IList<VoteSelectInfo> GetVoteSelectsByVoteID(int ID);

        bool AddVoteMember(VoteMemberInfo entity);

        bool EditVoteMember(VoteMemberInfo entity);

        int DelVoteMember(int ID);

        int UpdateVoteSelectNumber(int ID);

        VoteMemberInfo GetVoteMemberByID(int ID);

        IList<VoteMemberInfo> GetVoteMembers(QueryInfo Query);

        PageInfo GetVoteMemberPageInfo(QueryInfo Query);
    }
}
