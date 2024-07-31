using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.Elections;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Route("election")]
    public class ElectionController : Controller
    {
        private readonly IElectionService _electionService;

        public ElectionController(IElectionService bookService)
        {
            _electionService = bookService;
        }

        [HttpGet("create-election")]
        public IActionResult CreateElection()
        {
            return View();
        }

        [HttpPost("create-election")]
        public async Task<IActionResult> CreateElection([FromForm] CreateElectionDto request)
        {
            var result = await _electionService.AddElectionAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Elections");
            }
            return RedirectToAction("CreateElection");
        }

        [HttpGet("edit-election/{id}")]
        public async Task<IActionResult> EditElection(Guid id)
        {
            var result = await _electionService.GetElectionByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Elections");
        }

        [HttpPost("edit-election/{id}")]
        public async Task<IActionResult> EditElection(UpdateElectionDto request, Guid id)
        {
            var result = await _electionService.UpdateElectionAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Elections");
            }
            return RedirectToAction("EditElection", new { id = id });
        }



        [HttpGet("detail/{id}")]
        public async Task<IActionResult> ElectionDetail(Guid id)
        {
            var result = await _electionService.GetElectionByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Elections");
        }


        [HttpGet("elections")]
        public async Task<IActionResult> Elections()
        {
            var result = await _electionService.GetAllElectionsAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteElection(Guid id)
        {
            var result = await _electionService.DeleteElectionAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Elections");
            }

            return RedirectToAction("Elections");
        }
    }
}
