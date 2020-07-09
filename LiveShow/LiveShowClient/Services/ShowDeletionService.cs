using LiveShowClient.Interfaces;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class ShowDeletionService : IShowDeletionService
    {
        private readonly IApiService apiService;

        public ShowDeletionService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task DeleteShow(Guid id, Guid artistId)
        {
            await apiService.DeleteDataAsync($"/show/delete/{id}/{artistId}");
        }
    }
}
