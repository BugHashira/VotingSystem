using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.College;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.Colleges;


namespace VotingSystem.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly ApplicationDbContext _context;

        public CollegeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CollegeDto>> GetAllCollegesAsync()
        {
            return await _context.Colleges
                .Select(x => new CollegeDto
                {
                    Id = x.Id,
                    CollegeName = x.CollegeName
                }).ToListAsync();
        }

        public async Task<CollegeDto> GetCollegeByIdAsync(Guid id)
        {
            return await _context.Colleges
                .Where(x => x.Id.Equals(id))
                .Select(x => new CollegeDto
                {
                    Id = x.Id,
                    CollegeName = x.CollegeName
                }).FirstOrDefaultAsync();
        }

        public async Task AddCollegeAsync(CreateCollegeDto request)
        {
            var college = new College
            {
                Id = Guid.NewGuid(),
                CollegeName = request.CollegeName,
                CreatedDate = DateTime.UtcNow
            };

            _context.Colleges.Add(college);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCollegeAsync(Guid id, UpdateCollegeDto request)
        {
            var collegeExist = await _context.Colleges.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (collegeExist != null)
            {
                collegeExist.CollegeName = request.CollegeName;
                collegeExist.ModifiedDate = DateTime.Now;

                _context.Colleges.Update(collegeExist);
                await _context.SaveChangesAsync();
            }
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
