using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VotingSystem.Dto.Candidates;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Route("candidate")]
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly IPositionService _positionService;

        public CandidateController(ICandidateService candidateService, IPositionService positionService)
        {
            _candidateService = candidateService;
            _positionService = positionService;
        }

        [HttpGet("create-candidate/{electionId}")]
        public IActionResult CreateCandidate([FromRoute] Guid electionId)
        {

            var selectCutTypes = _positionService.GetPositionSelectAsync();
            ViewData["SelectPositions"] = new SelectList(selectCutTypes.Result.Data, "Id", "PositionName");
            return View();
        }

        [HttpPost("create-candidate/{electionId}")]
        public async Task<IActionResult> CreateCandidate([FromRoute] Guid electionId, [FromForm] CreateCandidateDto request)
        {
            request.ElectionId = electionId;
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

        [HttpGet("candidates/{electionId}")]
        public async Task<IActionResult> Candidates([FromRoute] Guid electionId)
        {
            var result = await _candidateService.GetAllCandidatesAsync(electionId);

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
