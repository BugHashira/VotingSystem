using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Votes;
using VotingSystem.Dto;

namespace VotingSystem.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext _context;

        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<VoteDto>>> GetAllVotesAsync()
        {
            try
            {
                var votes = await _context.Votes
                    .Include(x => x.User)
                    .Include(x => x.Candidate)
                    .Select(x => new VoteDto
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        CandidateId = x.CandidateId,
                        VoteTime = x.VoteTime,
                        UserName = x.User.UserName,
                        CandidateName = x.Candidate.CandidateName
                    }).ToListAsync();

                if (votes.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<VoteDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = votes
                    };
                }

                return new BaseResponseModel<IEnumerable<VoteDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = votes
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<VoteDto>>()
                {
                    IsSuccessful = false,
                    Message = "VoteService : GetAllVotesAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<VoteDto>> GetVoteByIdAsync(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var voteId))
                {
                    return new BaseResponseModel<VoteDto>()
                    {
                        IsSuccessful = false,
                        Message = "Invalid ID format"
                    };
                }

                var vote = await _context.Votes
                    .Include(x => x.User)
                    .Include(x => x.Candidate)
                    .Where(x => x.Id.Equals(voteId))
                    .Select(x => new VoteDto
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        CandidateId = x.CandidateId,
                        VoteTime = x.VoteTime,
                        UserName = x.User.UserName,
                        CandidateName = x.Candidate.CandidateName
                    }).FirstOrDefaultAsync();

                if (vote != null)
                {
                    return new BaseResponseModel<VoteDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = vote
                    };
                }

                return new BaseResponseModel<VoteDto>()
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<VoteDto>()
                {
                    IsSuccessful = false,
                    Message = "VoteService : GetVoteByIdAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddVoteAsync(CreateVoteDto request)
        {
            try
            {
                var vote = new Vote
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId.ToString(),
                    CandidateId = request.CandidateId,
                    VoteTime = request.VoteTime,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.Votes.AddAsync(vote);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data created successfully",
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
                    Message = "VoteService : AddVoteAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateVoteAsync(Guid id, UpdateVoteDto request)
        {
            try
            {
                var voteExist = await _context.Votes.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (voteExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                voteExist.UserId = request.UserId.ToString();
                voteExist.CandidateId = request.CandidateId;
                voteExist.VoteTime = request.VoteTime;
                voteExist.ModifiedDate = DateTime.Now;

                _context.Votes.Update(voteExist);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data updated successfully",
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
                    Message = "VoteService : UpdateVoteAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteVoteAsync(int id)
        {
            try
            {
                var vote = await _context.Votes.FindAsync(id);

                if (vote == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                _context.Votes.Remove(vote);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data deleted successfully",
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
                    Message = "VoteService : DeleteVoteAsync : Error Occurred:"
                };
            }
        }
    }
}
