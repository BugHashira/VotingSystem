using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.Manifesto;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.Candidates;
using VotingSystem.Data.Dto.Manifestoes;

namespace VotingSystem.Services
{
    public class ManifestoService : IManifestoService
    {
        private readonly ApplicationDbContext _context;

        public ManifestoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ManifestoDto>> GetAllManifestosAsync()
        {
            return await _context.Manifestos
                .Include(x => x.Candidate)
                .Select(x => new ManifestoDto
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    ManifestoNote = x.ManifestoNote,
                    CandidateName = x.Candidate.CandidateName
                }).ToListAsync();
        }

        public async Task<ManifestoDto> GetManifestoByIdAsync(Guid id)
        {
            return await _context.Manifestos
                .Include(x => x.Candidate)
                .Where(x => x.Id.Equals(id))
                .Select(x => new ManifestoDto
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    ManifestoNote = x.ManifestoNote,
                    CandidateName = x.Candidate.CandidateName
                }).FirstOrDefaultAsync();
        }

        public async Task AddManifestoAsync(CreateManifestoDto request)
        {
            var manifesto = new Manifesto
            {
                Id = Guid.NewGuid(),
                CandidateId = request.CandidateId,
                ManifestoNote = request.ManifestoNote,
                CreatedDate = DateTime.UtcNow
            };

            _context.Manifestos.Add(manifesto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateManifestoAsync(Guid id, UpdateManifestoDto request)
        {
            var manifestoExist = await _context.Manifestos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (manifestoExist != null)
            {
                manifestoExist.CandidateId = request.CandidateId;
                manifestoExist.ManifestoNote = request.ManifestoNote;
                manifestoExist.ModifiedDate = DateTime.Now;

                _context.Manifestos.Update(manifestoExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteManifestoAsync(int id)
        {
            var manifesto = await _context.Manifestos.FindAsync(id);
            if (manifesto != null)
            {
                _context.Manifestos.Remove(manifesto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
