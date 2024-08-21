using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.Candidates;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Route("candidate")]
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("create-candidate")]
        public IActionResult CreateCandidate()
        {
            return View();
        }

        [HttpPost("create-candidate")]
        public async Task<IActionResult> CreateCandidate([FromForm] CreateCandidateDto request)
        {
            var result = await _candidateService.AddCandidateAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Candidates");
            }
            return RedirectToAction("CreateCandidate");
        }

        [HttpGet("edit-candidate/{id}")]
        public async Task<IActionResult> EditCandidate(Guid id)
        {
            var result = await _candidateService.GetCandidateByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Candidates");
        }

        [HttpPost("edit-candidate/{id}")]
        public async Task<IActionResult> EditCandidate(UpdateCandidateDto request, Guid id)
        {
            var result = await _candidateService.UpdateCandidateAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Candidates");
            }
            return RedirectToAction("EditCandidate", new { id = id });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> CandidateDetail(Guid id)
        {
            var result = await _candidateService.GetCandidateByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Candidates");
        }

        [HttpGet("candidates")]
        public async Task<IActionResult> Candidates()
        {
            var result = await _candidateService.GetAllCandidatesAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var result = await _candidateService.DeleteCandidateAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Candidates");
            }

            return RedirectToAction("Candidates");
        }
    }
}
