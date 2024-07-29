using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.Election;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.Elections;

namespace VotingSystem.Services
{
    public class ElectionService : IElectionService
    {
        private readonly ApplicationDbContext _context;

        public ElectionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ElectionDto>> GetAllElectionsAsync()
        {
            return await _context.Elections
                .Select(x => new ElectionDto
                {
                    Id = x.Id,
                    ElectionName = x.ElectionName,
                    DescriptionName = x.DescriptionName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToListAsync();
        }

        public async Task<ElectionDto> GetElectionByIdAsync(Guid id)
        {
            return await _context.Elections
                .Where(x => x.Id.Equals(id))
                .Select(x => new ElectionDto
                {
                    Id = x.Id,
                    ElectionName = x.ElectionName,
                    DescriptionName = x.DescriptionName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).FirstOrDefaultAsync();
        }

        public async Task AddElectionAsync(CreateElectionDto request)
        {
            var election = new Election
            {
                Id = Guid.NewGuid(),
                ElectionName = request.ElectionName,
                DescriptionName = request.DescriptionName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow
            };

            _context.Elections.Add(election);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateElectionAsync(Guid id, UpdateElectionDto request)
        {
            var electionExist = await _context.Elections.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (electionExist != null)
            {
                electionExist.ElectionName = request.ElectionName;
                electionExist.DescriptionName = request.DescriptionName;
                electionExist.StartDate = request.StartDate;
                electionExist.EndDate = request.EndDate;
                electionExist.ModifiedDate = DateTime.Now;

                _context.Elections.Update(electionExist);
                await _context.SaveChangesAsync();
            }
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
