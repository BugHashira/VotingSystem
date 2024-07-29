using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dto.Departments;


namespace VotingSystem.Services.Interface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(Guid id);
        Task AddDepartmentAsync(CreateDepartmentDto request);
        Task UpdateDepartmentAsync(Guid id, UpdateDepartmentDto request);
        Task DeleteDepartmentAsync(int id);
    }
}
