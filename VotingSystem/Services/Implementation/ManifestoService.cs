using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto;
using VotingSystem.Dto.Manifestoes;
using System.Linq;

namespace VotingSystem.Services
{

    public class ManifestoService : IManifestoService
    {
        private readonly ApplicationDbContext _context;

        public ManifestoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<ManifestoDto>>> GetAllManifestosAsync()
        {
            try
            {
                var manifestos = await _context.Manifestos
                    .Include(x => x.Candidate)
                    .Select(x => new ManifestoDto
                    {
                        Id = x.Id,
                        CandidateId = x.CandidateId,
                        ManifestoNote = x.ManifestoDocument,
                        CandidateName = x.Candidate.CandidateName
                    }).ToListAsync();

                if (manifestos.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<ManifestoDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = manifestos
                    };
                }

                return new BaseResponseModel<IEnumerable<ManifestoDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = manifestos
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<ManifestoDto>>()
                {
                    IsSuccessful = false,
                    Message = "ManifestoService : GetAllManifestosAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<ManifestoDto>> GetManifestoByIdAsync(Guid id)
        {
            try
            {
                var manifesto = await _context.Manifestos
                    .Include(x => x.Candidate)
                    .Where(x => x.Id.Equals(id))
                    .Select(x => new ManifestoDto
                    {
                        Id = x.Id,
                        CandidateId = x.CandidateId,
                        ManifestoNote = x.ManifestoDocument,
                        CandidateName = x.Candidate.CandidateName
                    }).FirstOrDefaultAsync();

                if (manifesto != null)
                {
                    return new BaseResponseModel<ManifestoDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = manifesto
                    };
                }

                return new BaseResponseModel<ManifestoDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<ManifestoDto>()
                {
                    IsSuccessful = false,
                    Message = "ManifestoService : GetManifestoByIdAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddManifestoAsync(CreateManifestoDto request)
        {
            var candidate = await _context.Candidates
                           .FirstOrDefaultAsync(c => c.Id == request.CandidateId);
            try
            {
                var manifesto = new Manifesto()
                {
                    Id = Guid.NewGuid(),
                    CandidateId = request.CandidateId,
                    Candidate = candidate,
                    ManifestoDocument = request.ManifestoNote,
                    CreatedDate = DateTime.UtcNow,
                    FileExtension = request.FileExtension,
                    FileName = request.FileName
                };

                await _context.Manifestos.AddAsync(manifesto);

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
                    Message = "ManifestoService : AddManifestoAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdateManifestoAsync(Guid id, UpdateManifestoDto request)
        {
            try
            {
                var manifestoExist = await _context.Manifestos.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (manifestoExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                manifestoExist.CandidateId = request.CandidateId;
                manifestoExist.ManifestoDocument = request.ManifestoNote;
                manifestoExist.ModifiedDate = DateTime.Now;

                _context.Manifestos.Update(manifestoExist);

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
                    Message = "ManifestoService : UpdateManifestoAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeleteManifestoAsync(int id)
        {
            try
            {
                var manifesto = await _context.Manifestos.FindAsync(id);

                if (manifesto == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                _context.Manifestos.Remove(manifesto);

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
                    Message = "ManifestoService : DeleteManifestoAsync : Error Occurred:"
                };
            }
        }
    }
}
