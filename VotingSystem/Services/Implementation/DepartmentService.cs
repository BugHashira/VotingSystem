using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Dto.Departments;

namespace VotingSystem.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Select(x => new DepartmentDto
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                }).ToListAsync();
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid id)
        {
            return await _context.Departments
                .Where(x => x.Id.Equals(id))
                .Select(x => new DepartmentDto
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                }).FirstOrDefaultAsync();
        }

        public async Task AddDepartmentAsync(CreateDepartmentDto request)
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                DepartmentName = request.DepartmentName,
                CreatedDate = DateTime.UtcNow
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Guid id, UpdateDepartmentDto request)
        {
            var departmentExist = await _context.Departments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (departmentExist != null)
            {
                departmentExist.DepartmentName = request.DepartmentName;
                departmentExist.ModifiedDate = DateTime.Now;

                _context.Departments.Update(departmentExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
