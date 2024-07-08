using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;

namespace VotingSystem.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly ApplicationDbContext _context;

        public CollegeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<College>> GetAllCollegesAsync()
        {
            return await _context.Colleges.ToListAsync();
        }

        public async Task<College> GetCollegeByIdAsync(int id)
        {
            return await _context.Colleges.FindAsync(id);
        }

        public async Task AddCollegeAsync(College college)
        {
            _context.Colleges.Add(college);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCollegeAsync(College college)
        {
            _context.Entry(college).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCollegeAsync(int id)
        {
            var college = await _context.Colleges.FindAsync(id);
            if (college != null)
            {
                _context.Colleges.Remove(college);
                await _context.SaveChangesAsync();
            }
        }
    }
}
