using LiveShowClient.Interfaces;
using LiveShowClient.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Controllers
{
    public class ShowController : Controller
    {
        private readonly IShowGettingService gettingService;
        private readonly IGettingCurrentUser currentUser;
        private readonly IGenreService genreService;
        private readonly IShowCreationService creationService;
        private readonly IShowDeletionService deletionService;
        private readonly IShowCancellationService cancellationService;
        private readonly IShowUpdatingService updatingService;
        private readonly IHttpContextAccessor httpContext;

        public ShowController(IShowGettingService showService, IGettingCurrentUser currentUser, IGenreService genreService, IHttpContextAccessor httpContext,
            IShowCreationService showCreation, IShowDeletionService showDeletion, IShowCancellationService showCancellation, IShowUpdatingService showUpdating)
        {
            this.gettingService = showService;
            this.currentUser = currentUser;
            this.genreService = genreService;
            this.httpContext = httpContext;
            this.creationService = showCreation;
            this.updatingService = showUpdating;
            this.cancellationService = showCancellation;
            this.deletionService = showDeletion;
        }

        [Authorize(Roles ="Artist")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genres = await genreService.GetMusicGenres();
            ViewData["Genre"] = new SelectList(genres, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShowVM show)
        {
            show.ArtistId = currentUser.GetCurrentUserId(httpContext);
            var result = await creationService.CreateShow(show);
            return RedirectToAction("ShowsForArtist", "Show", new { @id = result.ArtistId });
        }

        [HttpGet]
        public async Task<IActionResult> AllShows()
        {
            var shows = await gettingService.GetShows();
            return View(shows);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var show = await gettingService.GetShow(id);
            return View(show);
        }

        [Authorize(Roles = "Artist")]
        [HttpGet]
        public async Task<IActionResult> ShowsForArtist()
        {
            var shows = await gettingService.GetShowsForArtist(currentUser.GetCurrentUserId(httpContext));
            return View(shows);
        }

        [HttpGet]
        public async Task<IActionResult> ShowsToBeAttended()
        {
            var show = await gettingService.GetShowsToBeAttended(currentUser.GetCurrentUserId(httpContext));
            return View(show);
        }

        [HttpGet]
        public async Task<IActionResult> ShowsThatCanBeAttended()
        {
            var shows = await gettingService.ShowsThatCanBeAttended(currentUser.GetCurrentUserId(httpContext));
            return View(shows);
        }

        [Authorize(Roles = "Artist")]
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View(await gettingService.GetShow(id));
        }

        public async Task<IActionResult> Update(Guid id, ShowUpdatingVM show)
        {
            await updatingService.UpdateShow(id, currentUser.GetCurrentUserId(httpContext), show);
            return RedirectToAction("ShowsForArtist", "Show");
        }

        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await cancellationService.CancelShow(id, currentUser.GetCurrentUserId(httpContext));
            return RedirectToAction("ShowsForArtist", "Show");
        }

        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await deletionService.DeleteShow(id, currentUser.GetCurrentUserId(httpContext));
            return RedirectToAction("ShowsForArtist", "Show");
        }
    }
}