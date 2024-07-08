using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.Candidates;
using VotingSystem.Data.Enums;

namespace VotingSystem.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;

        public CandidateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CandidateDto>> GetAllCandidatesAsync()
        {
            return await _context.Candidates
                .Include(x => x.Election)
                .Include(x => x.Position)
                .Select(x => new CandidateDto
                {
                    Id = x.Id,
                    ApprovalStatus = x.ApprovalStatus,
                    CandidateName = x.CandidateName,
                    ElectionId = x.ElectionId,
                    ElectionName = x.Election.ElectionName,
                    Level = x.Level,
                    MatricNumber = x.MatricNumber,
                    PositionId = x.PositionId,
                    PositionName = x.Position.PositionName

                }).ToListAsync();
        }

        public async Task<CandidateDto> GetCandidateByIdAsync(Guid id)
        {
            return await _context.Candidates.Include(x => x.Election)
                .Include(x => x.Position)
                .Where(x => x.Id.Equals(id))
                .Select(x => new CandidateDto
                {
                    Id = x.Id,
                    ApprovalStatus = x.ApprovalStatus,
                    CandidateName = x.CandidateName,
                    ElectionId = x.ElectionId,
                    ElectionName = x.Election.ElectionName,
                    Level = x.Level,
                    MatricNumber = x.MatricNumber,
                    PositionId = x.PositionId,
                    PositionName = x.Position.PositionName
                }).FirstOrDefaultAsync();
        }

        public async Task AddCandidateAsync(CreateCandidateDto request)
        {
            var candidate = new Candidate()
            {
                Id = Guid.NewGuid(),
                ElectionId = request.ElectionId,
                ApprovalStatus = ApprovalStatus.Pending,
                CandidateName = request.CandidateName,
                CreatedDate = DateTime.UtcNow,
                MatricNumber = request.MatricNumber,
                PositionId = request.PositionId,
                Level = request.Level
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCandidateAsync(Guid id, UpdateCandidateDto request)
        {

            var candidateExist = await _context.Candidates.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (candidateExist != null)
            {
                candidateExist.MatricNumber = request.MatricNumber;
                candidateExist.Level = request.Level;
                candidateExist.PositionId = request.PositionId;
                candidateExist.ElectionId = request.ElectionId;
                candidateExist.CandidateName = request.CandidateName;
                candidateExist.ModifiedDate = DateTime.Now;

                _context.Candidates.Update(candidateExist);
                await _context.SaveChangesAsync();
            }
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
