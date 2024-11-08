using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingSystem.Dto.Positions;
using VotingSystem.Services.Interface;

namespace VotingSystem.Controllers
{
    [Authorize]
    [Route("position")]
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet("create-position")]
        public IActionResult CreatePosition()
        {
            return View();
        }

        [Authorize(Roles = ("Admin"))]
        [HttpPost("create-position")]
        public async Task<IActionResult> CreatePosition([FromForm] CreatePositionDto request)
        {
            var result = await _positionService.AddPositionAsync(request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Positions");
            }
            return RedirectToAction("CreatePosition");
        }

        [HttpGet("edit-position/{id}")]
        public async Task<IActionResult> EditPosition(Guid id)
        {
            var result = await _positionService.GetPositionByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Positions");
        }

        [HttpPost("edit-position/{id}")]
        public async Task<IActionResult> EditPosition(UpdatePositionDto request, Guid id)
        {
            var result = await _positionService.UpdatePositionAsync(id, request);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Positions");
            }
            return RedirectToAction("EditPosition", new { id = id });
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> PositionDetail(Guid id)
        {
            var result = await _positionService.GetPositionByIdAsync(id);

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return RedirectToAction("Positions");
        }

        [HttpGet("positions")]
        public async Task<IActionResult> Positions()
        {
            var result = await _positionService.GetAllPositionsAsync();

            if (result.IsSuccessful)
            {
                return View(result.Data);
            }

            return View(result.Data);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> DeletePosition(Guid id)
        {
            var result = await _positionService.DeletePositionAsync(id);

            if (result.IsSuccessful)
            {
                return RedirectToAction("Positions");
            }

            return RedirectToAction("Positions");
        }
    }
}
