using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Elections;
using VotingSystem.Dto;

namespace VotingSystem.Services
{
    public class ElectionService : IElectionService
    {
        private readonly ApplicationDbContext _context;

        public ElectionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<ElectionDto>>> GetAllElectionsAsync()
        {
            try
            {
                var elections = await _context.Elections
                    .Select(x => new ElectionDto
                    {
                        Id = x.Id,
                        ElectionName = x.ElectionName,
                        DescriptionName = x.DescriptionName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    }).ToListAsync();

                if (elections.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<ElectionDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = elections
                    };
                }

                return new BaseResponseModel<IEnumerable<ElectionDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = elections
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<ElectionDto>>()
                {
                    IsSuccessful = false,
                    Message = "ElectionService : GetAllElectionsAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<ElectionDto>> GetElectionByIdAsync(Guid id)
        {
            try
            {
                var election = await _context.Elections
                    .Where(x => x.Id.Equals(id))
                    .Select(x => new ElectionDto
                    {
                        Id = x.Id,
                        ElectionName = x.ElectionName,
                        DescriptionName = x.DescriptionName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    }).FirstOrDefaultAsync();

                if (election != null)
                {
                    return new BaseResponseModel<ElectionDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = election
                    };
                }

                return new BaseResponseModel<ElectionDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<ElectionDto>()
                {
                    IsSuccessful = false,
                    Message = "ElectionService : GetElectionByIdAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddElectionAsync(CreateElectionDto request)
        {
            try
            {
                var election = new Election()
                {
                    Id = Guid.NewGuid(),
                    ElectionName = request.ElectionName,
                    DescriptionName = request.DescriptionName,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.Elections.AddAsync(election);

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
            catch (Exception)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "ElectionService : AddElectionAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateElectionAsync(Guid id, UpdateElectionDto request)
        {
            try
            {
                var electionExist = await _context.Elections.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (electionExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                electionExist.ElectionName = request.ElectionName;
                electionExist.DescriptionName = request.DescriptionName;
                electionExist.StartDate = request.StartDate;
                electionExist.EndDate = request.EndDate;
                electionExist.ModifiedDate = DateTime.Now;

                _context.Elections.Update(electionExist);

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
                    Message = "ElectionService : UpdateElectionAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteElectionAsync(int id)
        {
            try
            {
                var election = await _context.Elections.FindAsync(id);

                if (election == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                _context.Elections.Remove(election);

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
                    Message = "ElectionService : DeleteElectionAsync : Error Occurred:"
                };
            }
        }
    }
}
