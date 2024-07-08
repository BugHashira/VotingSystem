using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public class ManifestoService : IManifestoService
    {
        private readonly ApplicationDbContext _context;

        public ManifestoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manifesto>> GetAllManifestosAsync()
        {
            return await _context.Manifestos.ToListAsync();
        }

        public async Task<Manifesto> GetManifestoByIdAsync(int id)
        {
            return await _context.Manifestos.FindAsync(id);
        }

        public async Task AddManifestoAsync(Manifesto manifesto)
        {
            _context.Manifestos.Add(manifesto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateManifestoAsync(Manifesto manifesto)
        {
            _context.Entry(manifesto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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
