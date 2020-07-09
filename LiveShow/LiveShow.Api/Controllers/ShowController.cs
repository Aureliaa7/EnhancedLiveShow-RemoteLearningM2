using LiveShow.Api.Filters;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Show;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class ShowController : LiveShowApiControllerBase
    {
        private readonly IShowGettingService showService;
        private readonly IShowCreationService showCreationService;
        private readonly IShowUpdatingService showUpdatingService;
        private readonly IShowCancellationService showCancellationService;
        private readonly IShowDeletionService showDeletionService;

        public ShowController(IShowGettingService showService, IShowCreationService showCreation, IShowCancellationService showCancellation, 
            IShowUpdatingService showUpdating, IShowDeletionService showDeletion)
        {
            this.showService = showService;
            this.showCreationService = showCreation;
            this.showUpdatingService = showUpdating;
            this.showCancellationService = showCancellation;
            this.showDeletionService = showDeletion;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var show = await showService.GetShow(id);
            return Ok(show);
        }

        [HttpPost("shows")]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateShow([FromBody]ShowDtoWithoutId show)
        {
            var createdShow = await showCreationService.CreateShow(show);
            return Created(Url.Action("CreateShow"), createdShow);
        }

        [HttpPatch("shows/update/{id}/{artistId}")]
        public async Task<IActionResult> UpdateShow(Guid id, Guid artistId, [FromBody] JsonPatchDocument<ShowDto> showDto)
        {
            return Ok(await showUpdatingService.UpdateShow(id, artistId, showDto));
        }

        [HttpPatch("shows/cancel/{id}/{artistId}")]
        public async Task<IActionResult> CancelShow(Guid id, Guid artistId)
        {
            var canceledShow = await showCancellationService.CancelShow(id, artistId);
            return Ok(canceledShow);
        }

        [HttpGet("shows")]
        public async Task<IActionResult> GetAllShows()
        {
            var shows = await showService.GetAllShows();
            return Ok(shows);
        }

        [HttpGet("shows/artist/{artistId}")]
        public async Task<IActionResult> GetShowsForArtist(Guid artistId)
        {
            var shows = await showService.GetShowsForArtist(artistId);
            return Ok(shows);
        }

        [HttpGet("shows/user/{attendeeId}")]
        public async Task<IActionResult> GetShowsToBeAttended(Guid attendeeId)
        {
            var shows = await showService.GetShowsToBeAttended(attendeeId);
            return Ok(shows);
        }

        [HttpGet("find/{attendeeId}")]
        public async Task<IActionResult> FindShows(Guid attendeeId)
        {
            var shows = await showService.FindShows(attendeeId);
            return Ok(shows);
        }

        [HttpDelete("delete/{id}/{artistId}")]
        public async Task<IActionResult> DeleteShow(Guid id, Guid artistId)
        {
            await showDeletionService.DeleteShow(id, artistId);
            return NoContent();
        }
    }
}