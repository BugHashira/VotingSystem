using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Services.Interface;

namespace VotingSystem.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext _context;

        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vote>> GetAllVotesAsync()
        {
            return await _context.Votes.ToListAsync();
        }

        public async Task<Vote> GetVoteByIdAsync(int id)
        {
            return await _context.Votes.FindAsync(id);
        }

        public async Task AddVoteAsync(Vote vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVoteAsync(Vote vote)
        {
            _context.Entry(vote).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
