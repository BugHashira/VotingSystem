using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Colleges;
using VotingSystem.Dto;

namespace VotingSystem.Services
{

    public class CollegeService : ICollegeService
    {
        private readonly ApplicationDbContext _context;

        public CollegeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<CollegeDto>>> GetAllCollegesAsync()
        {
            try
            {
                var colleges = await _context.Colleges
                    .Select(x => new CollegeDto
                    {
                        Id = x.Id,
                        CollegeName = x.CollegeName
                    }).ToListAsync();

                if (colleges.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<CollegeDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = colleges
                    };
                }

                return new BaseResponseModel<IEnumerable<CollegeDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = colleges
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<CollegeDto>>()
                {
                    IsSuccessful = false,
                    Message = "CollegeService : GetAllCollegesAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<CollegeDto>> GetCollegeByIdAsync(Guid id)
        {
            try
            {
                var college = await _context.Colleges
                    .Where(x => x.Id.Equals(id))
                    .Select(x => new CollegeDto
                    {
                        Id = x.Id,
                        CollegeName = x.CollegeName
                    }).FirstOrDefaultAsync();

                if (college != null)
                {
                    return new BaseResponseModel<CollegeDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = college
                    };
                }

                return new BaseResponseModel<CollegeDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<CollegeDto>()
                {
                    IsSuccessful = false,
                    Message = "CollegeService : GetCollegeByIdAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddCollegeAsync(CreateCollegeDto request)
        {
            try
            {
                var college = new College()
                {
                    Id = Guid.NewGuid(),
                    CollegeName = request.CollegeName,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.Colleges.AddAsync(college);

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
                    Message = "CollegeService : AddCollegeAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateCollegeAsync(Guid id, UpdateCollegeDto request)
        {
            try
            {
                var collegeExist = await _context.Colleges.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (collegeExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = true, Message = "No record found", Data = false };

                collegeExist.CollegeName = request.CollegeName;
                collegeExist.ModifiedDate = DateTime.Now;

                _context.Colleges.Update(collegeExist);

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
                    Message = "CollegeService : UpdateCollegeAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteCollegeAsync(int id)
        {
            try
            {
                var college = await _context.Colleges.FindAsync(id);

                if (college == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = true, Message = "No record found", Data = false };

                _context.Colleges.Remove(college);

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
                    Message = "CollegeService : DeleteCollegeAsync : Error Occured:"
                };
            }
        }
    }
}
