using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Services.Interface;

namespace VotingSystem.Services
{
    public class ElectionService : IElectionService
    {
        private readonly ApplicationDbContext _context;

        public ElectionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Election>> GetAllElectionsAsync()
        {
            return await _context.Elections.ToListAsync();
        }

        public async Task<Election> GetElectionByIdAsync(int id)
        {
            return await _context.Elections.FindAsync(id);
        }

        public async Task AddElectionAsync(Election election)
        {
            _context.Elections.Add(election);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateElectionAsync(Election election)
        {
            _context.Entry(election).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteElectionAsync(int id)
        {
            var election = await _context.Elections.FindAsync(id);
            if (election != null)
            {
                _context.Elections.Remove(election);
                await _context.SaveChangesAsync();
            }
        }
    }
}
