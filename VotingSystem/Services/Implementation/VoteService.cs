using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.Vote;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.Votes;

namespace VotingSystem.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext _context;

        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VoteDto>> GetAllVotesAsync()
        {
            return await _context.Votes
                .Include(x => x.User)
                .Include(x => x.Candidate)
                .Select(x => new VoteDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CandidateId = x.CandidateId,
                    VoteTime = x.VoteTime,
                    UserName = x.User.UserName,
                    CandidateName = x.Candidate.CandidateName
                }).ToListAsync();
        }

        public async Task<VoteDto> GetVoteByIdAsync(Guid id)
        {
            return await _context.Votes
                .Include(x => x.User)
                .Include(x => x.Candidate)
                .Where(x => x.Id.Equals(id))
                .Select(x => new VoteDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CandidateId = x.CandidateId,
                    VoteTime = x.VoteTime,
                    UserName = x.User.UserName,
                    CandidateName = x.Candidate.CandidateName
                }).FirstOrDefaultAsync();
        }

        public async Task AddVoteAsync(CreateVoteDto request)
        {
            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId.ToString(),
                CandidateId = request.CandidateId,
                VoteTime = request.VoteTime,
                CreatedDate = DateTime.UtcNow
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVoteAsync(Guid id, UpdateVoteDto request)
        {
            var voteExist = await _context.Votes.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (voteExist != null)
            {
                voteExist.UserId = request.UserId.ToString();
                voteExist.CandidateId = request.CandidateId;
                voteExist.VoteTime = request.VoteTime;
                voteExist.ModifiedDate = DateTime.Now;

                _context.Votes.Update(voteExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteVoteAsync(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
                await _context.SaveChangesAsync();
            }
        }
    }
}
