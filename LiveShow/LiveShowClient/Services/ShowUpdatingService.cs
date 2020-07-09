using LiveShow.Services.Models.Show;
using LiveShowClient.Interfaces;
using LiveShowClient.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class ShowUpdatingService : IShowUpdatingService
    {
        private readonly IApiService apiService;

        public ShowUpdatingService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<ShowDto> UpdateShow(Guid id, Guid artistId, ShowUpdatingVM show)
        {
            var showDto = await apiService.GetContentFromHttpAsync<ShowDto>($"/show/{id}");
            JsonPatchDocument<ShowDto> jsonPatch = new JsonPatchDocument<ShowDto>();
            jsonPatch.Replace(s => s.Venue, show.Venue);
            jsonPatch.Replace(s => s.DateTime, show.DateTime);
            var result = await apiService.PatchDataAsync($"/show/shows/update/{id}/{artistId}", jsonPatch);
            return JsonConvert.DeserializeObject<ShowDto>(result);
        }
    }
}
