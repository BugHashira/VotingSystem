using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dto.Departments;
using VotingSystem.Dto;


namespace VotingSystem.Services.Interface
{
    public interface IDepartmentService
    {
        Task<BaseResponseModel<bool>> AddDepartmentAsync(CreateDepartmentDto request);
        Task<BaseResponseModel<bool>> DeleteDepartmentAsync(int id);
        Task<BaseResponseModel<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync();
        Task<BaseResponseModel<DepartmentDto>> GetDepartmentByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto request);
    }
}
