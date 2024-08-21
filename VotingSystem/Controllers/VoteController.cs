using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.Votes;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Route("vote")]
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpGet("create-vote")]
        public IActionResult CreateVote()
        {
            return View();
        }

        [HttpPost("create-vote")]
        public async Task<IActionResult> CreateVote([FromForm] CreateVoteDto request)
        {
            var result = await _voteService.AddVoteAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Votes");
            }
            return RedirectToAction("CreateVote");
        }

        [HttpGet("edit-vote/{id}")]
        public async Task<IActionResult> EditVote(string id)
        {
            var result = await _voteService.GetVoteByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Votes");
        }

        [HttpPost("edit-vote/{id}")]
        public async Task<IActionResult> EditVote(UpdateVoteDto request, Guid id)
        {
            var result = await _voteService.UpdateVoteAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Votes");
            }
            return RedirectToAction("EditVote", new { id = id });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> VoteDetail(string id)
        {
            var result = await _voteService.GetVoteByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Votes");
        }

        [HttpGet("votes")]
        public async Task<IActionResult> Votes()
        {
            var result = await _voteService.GetAllVotesAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeleteVote(int id)
        {
            var result = await _voteService.DeleteVoteAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Votes");
            }

            return RedirectToAction("Votes");
        }
    }
}
