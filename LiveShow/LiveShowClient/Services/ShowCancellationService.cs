using LiveShow.Services.Models.Show;
using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class ShowCancellationService : IShowCancellationService
    {
        private readonly IApiService apiService;

        public ShowCancellationService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<ShowDto> CancelShow(Guid id, Guid artistId)
        {
            var path = $"/show/shows/cancel/{id}/{artistId}";
            JsonPatchDocument<ShowDto> jsonPatch = new JsonPatchDocument<ShowDto>();
            var result = await apiService.PatchDataAsync(path, jsonPatch);
            return JsonConvert.DeserializeObject<ShowDto>(result);
        }
    }
}
