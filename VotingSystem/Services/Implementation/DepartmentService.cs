using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Departments;
using VotingSystem.Dto;

namespace VotingSystem.Services
{

    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _context.Departments
                    .Select(x => new DepartmentDto
                    {
                        Id = x.Id,
                        DepartmentName = x.DepartmentName
                    }).ToListAsync();

                if (departments.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<DepartmentDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = departments
                    };
                }

                return new BaseResponseModel<IEnumerable<DepartmentDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = departments
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<DepartmentDto>>()
                {
                    IsSuccessful = false,
                    Message = "DepartmentService : GetAllDepartmentsAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<DepartmentDto>> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var department = await _context.Departments
                    .Where(x => x.Id.Equals(id))
                    .Select(x => new DepartmentDto
                    {
                        Id = x.Id,
                        DepartmentName = x.DepartmentName
                    }).FirstOrDefaultAsync();

                if (department != null)
                {
                    return new BaseResponseModel<DepartmentDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = department
                    };
                }

                return new BaseResponseModel<DepartmentDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<DepartmentDto>()
                {
                    IsSuccessful = false,
                    Message = "DepartmentService : GetDepartmentByIdAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddDepartmentAsync(CreateDepartmentDto request)
        {
            try
            {
                var department = new Department()
                {
                    Id = Guid.NewGuid(),
                    DepartmentName = request.DepartmentName,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.Departments.AddAsync(department);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data Created successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Create failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "DepartmentService : AddDepartmentAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto request)
        {
            try
            {
                var departmentExist = await _context.Departments.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (departmentExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = true, Message = "No record found", Data = false };

                departmentExist.DepartmentName = request.DepartmentName;
                departmentExist.ModifiedDate = DateTime.Now;

                _context.Departments.Update(departmentExist);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data Updated successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Update failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "DepartmentService : UpdateDepartmentAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteDepartmentAsync(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);

                if (department == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = true, Message = "No record found", Data = false };

                _context.Departments.Remove(department);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data Deleted successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Delete failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "DepartmentService : DeleteDepartmentAsync : Error Occured:"
                };
            }
        }

        public Task<BaseResponseModel<bool>> DeleteDepartmentAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
