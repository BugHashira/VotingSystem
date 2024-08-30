using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.Manifestoes;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Route("manifesto")]
    public class ManifestoController : Controller
    {
        private readonly IManifestoService _manifestoService;
        private readonly ICandidateService _candidateService;

        public ManifestoController(IManifestoService manifestoService, ICandidateService candidateService)
        {
            _manifestoService = manifestoService;
            _candidateService   = candidateService;
        }

        [HttpGet("create-manifesto/{candidateId}")]
        public async Task<IActionResult> CreateManifesto([FromRoute] Guid candidateId)
        {
            var candidate = await _candidateService.GetCandidateListItem();

            var viewModel = new CreateManifestoDto
            {
                Candidates = candidate.ToList()
            };
            return View(viewModel);
        }

        [HttpPost("create-manifesto/{candidateId}")]
        public async Task<IActionResult> CreateManifesto([FromRoute] Guid candidateId,[FromForm] CreateManifestoDto request)
        {
            var result = await _manifestoService.AddManifestoAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Manifestoes");
            }
            return RedirectToAction("CreateManifesto");
        }

        [HttpGet("edit-manifesto/{id}")]
        public async Task<IActionResult> EditManifesto(Guid id)
        {
            var result = await _manifestoService.GetManifestoByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Manifestoes");
        }

        [HttpPost("edit-manifesto/{id}")]
        public async Task<IActionResult> EditManifesto(UpdateManifestoDto request, Guid id)
        {
            var result = await _manifestoService.UpdateManifestoAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Manifestoes");
            }
            return RedirectToAction("EditManifesto", new { id = id });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> ManifestoDetail(Guid id)
        {
            var result = await _manifestoService.GetManifestoByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Manifestoes");
        }

        [HttpGet("manifestoes")]
        public async Task<IActionResult> Manifestoes()
        {
            var result = await _manifestoService.GetAllManifestosAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteManifesto(int id)
        {
            var result = await _manifestoService.DeleteManifestoAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Manifestoes");
            }

            return RedirectToAction("Manifestoes");
        }
    }
}
