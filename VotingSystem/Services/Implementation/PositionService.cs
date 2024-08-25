using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.Positions;
using VotingSystem.Dto;

namespace VotingSystem.Services
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext _context;

        public PositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<PositionDto>>> GetAllPositionsAsync()
        {
            try
            {
                var positions = await _context.Positions
                    .Select(x => new PositionDto
                    {
                        Id = x.Id,
                        Price = x.Price,
                        PositionDescription = x.PositionDescription,
                        PositionName = x.PositionName
                    }).ToListAsync();

                if (positions.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<PositionDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = positions
                    };
                }

                return new BaseResponseModel<IEnumerable<PositionDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = positions
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<PositionDto>>()
                {
                    IsSuccessful = false,
                    Message = "PositionService : GetAllPositionsAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<PositionDto>> GetPositionByIdAsync(Guid id)
        {
            try
            {
                var position = await _context.Positions
                    .Where(x => x.Id.Equals(id))
                    .Select(x => new PositionDto
                    {
                        Id = x.Id,
                        Price = x.Price,
                        PositionDescription = x.PositionDescription,
                        PositionName = x.PositionName
                    }).FirstOrDefaultAsync();

                if (position != null)
                {
                    return new BaseResponseModel<PositionDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = position
                    };
                }

                return new BaseResponseModel<PositionDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<PositionDto>()
                {
                    IsSuccessful = false,
                    Message = "PositionService : GetPositionByIdAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddPositionAsync(CreatePositionDto request)
        {
            try
            {

                var checkExistingPosition = await _context.Positions.FirstOrDefaultAsync(x =>x.PositionName.Equals( request.PositionName.ToLower()));

                if (checkExistingPosition != null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "Position name already exist", Data = true };


                var position = new Position()
                {
                    Id = Guid.NewGuid(),
                    Price = request.Price,
                    PositionName = request.PositionName,
                    PositionDescription = request.PositionDescription,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.Positions.AddAsync(position);

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
                    Message = "PositionService : AddPositionAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdatePositionAsync(Guid id, UpdatePositionDto request)
        {
            try
            {
                var positionExist = await _context.Positions.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (positionExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                positionExist.Price = request.Price;
                positionExist.PositionDescription = request.PositionDescription;
                positionExist.PositionName = request.PositionName;
                positionExist.ModifiedDate = DateTime.Now;

                _context.Positions.Update(positionExist);

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
                    Message = "PositionService : UpdatePositionAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeletePositionAsync(Guid id)
        {
            try
            {
                var position = await _context.Positions.FindAsync(id);

                if (position == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                _context.Positions.Remove(position);

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
                    Message = "PositionService : DeletePositionAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<IEnumerable<SelectPositionDto>>> GetPositionSelectAsync()
        {
            try
            {
                var positions = await _context.Positions
                    .Select(x => new SelectPositionDto
                    {
                        Id = x.Id,
                        PositionName = x.PositionName
                    }).ToListAsync();

                if (positions.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<SelectPositionDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = positions
                    };
                }

                return new BaseResponseModel<IEnumerable<SelectPositionDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = positions
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<SelectPositionDto>>()
                {
                    IsSuccessful = false,
                    Message = "PositionService : GetAllPositionsAsync : Error Occurred:"
                };
            }
        }
    }
}
