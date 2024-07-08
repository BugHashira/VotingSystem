using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Services.Interface;

namespace VotingSystem.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;

        public CandidateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            return await _context.Candidates.FindAsync(id);
        }

        public async Task AddCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidateAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
