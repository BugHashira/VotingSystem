using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.Position;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.Positions;

namespace VotingSystem.Services
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext _context;

        public PositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PositionDto>> GetAllPositionsAsync()
        {
            return await _context.Positions
                .Select(x => new PositionDto
                {
                    Id = x.Id,
                    Price = x.Price,
                    PositionDescription = x.PositionDescription
                }).ToListAsync();
        }

        public async Task<PositionDto> GetPositionByIdAsync(Guid id)
        {
            return await _context.Positions
                .Where(x => x.Id.Equals(id))
                .Select(x => new PositionDto
                {
                    Id = x.Id,
                    Price = x.Price,
                    PositionDescription = x.PositionDescription
                }).FirstOrDefaultAsync();
        }

        public async Task AddPositionAsync(CreatePositionDto request)
        {
            var position = new Position
            {
                Id = Guid.NewGuid(),
                Price = request.Price,
                PositionDescription = request.PositionDescription,
                CreatedDate = DateTime.UtcNow
            };

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePositionAsync(Guid id, UpdatePositionDto request)
        {
            var positionExist = await _context.Positions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (positionExist != null)
            {
                positionExist.Price = request.Price;
                positionExist.PositionDescription = request.PositionDescription;
                positionExist.ModifiedDate = DateTime.Now;

                _context.Positions.Update(positionExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePositionAsync(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
                await _context.SaveChangesAsync();
            }
        }
    }
}
