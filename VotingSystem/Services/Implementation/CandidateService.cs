using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Enums;
using VotingSystem.Dto.Candidates;
using VotingSystem.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VotingSystem.Services
{


    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;

        public CandidateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<CandidateDto>>> GetAllCandidatesAsync(Guid electionId)
        {

            try
            {
                var candidates = await _context.Candidates
              .Include(x => x.Election)
              .Include(x => x.Position)
              .Include(x => x.Manifestos)
              .Where(x => x.ElectionId == electionId)
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
                  PositionName = x.Position.PositionName,
                  HasManifesto = x.Manifestos.Count > 0 ? true : false,
                  ManifestoId = x.Manifestos.Count > 0 ? x.Manifestos.Select(x => x.Id).FirstOrDefault() : null,
              }).ToListAsync();


                if (candidates.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<CandidateDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = candidates
                    };
                }

                return new BaseResponseModel<IEnumerable<CandidateDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = candidates
                };
            }
            catch (Exception ex)
            {

                return new BaseResponseModel<IEnumerable<CandidateDto>>()
                {
                    IsSuccessful = false,
                    Message = "CandidateService : GetAllCandidatesAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<CandidateDto>> GetCandidateByIdAsync(Guid id)
        {

            try
            {
                var candidate = await _context.Candidates
                .Include(x => x.Election)
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


                if (candidate != null)
                {
                    return new BaseResponseModel<CandidateDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = candidate
                    };
                }

                return new BaseResponseModel<CandidateDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {

                return new BaseResponseModel<CandidateDto>()
                {
                    IsSuccessful = false,
                    Message = "CandidateService : GetCandidatesAsync : Error Occured:"
                };
            }

        }

        public async Task<BaseResponseModel<bool>> AddCandidateAsync(CreateCandidateDto request)
        {
            try
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

                await _context.Candidates.AddAsync(candidate);

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
                    Message = "CandidateService : AddCandidateAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateCandidateAsync(Guid id, UpdateCandidateDto request)
        {

            try
            {
                var candidateExist = await _context.Candidates.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (candidateExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = true, Message = "No record found", Data = false };

                candidateExist.MatricNumber = request.MatricNumber;
                candidateExist.Level = request.Level;
                candidateExist.PositionId = request.PositionId;
                candidateExist.ElectionId = request.ElectionId;
                candidateExist.CandidateName = request.CandidateName;
                candidateExist.ModifiedDate = DateTime.Now;

                _context.Candidates.Update(candidateExist);

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
                    Message = "Upadte failed",
                    Data = false
                };


            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "CandidateService : UpdateCandidateAsync : Error Occured:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteCandidateAsync(int id)
        {

            try
            {
                var candidate = await _context.Candidates.FindAsync(id);

                if (candidate != null)
                    return new BaseResponseModel<bool>() { IsSuccessful = true, Message = "No record found", Data = false };

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
                    Message = "CandidateService : UpdateCandidateAsync : Error Occured:"
                };
            }

        }

        public async Task<IEnumerable<SelectListItem>> GetCandidateListItem()
        {
            var candidates = await _context.Candidates.ToListAsync();

            var selectListItem = candidates.Select(item => new SelectListItem()
            {
                Value = item.Id.ToString(),
                Text = item.CandidateName
            }).ToList();

            return selectListItem;
        }
    }
}
