using Blazor.Extensions.Storage;
using TestBlazor3K.ApiRequest.Model;

namespace TestBlazor3K.ApiRequest.Services
{
    public class UserService
    {
        public static LoginResponseDataModel CurrentUserData { get; set; }
        private ApiRequestService _apiRequest;
        public UserService(ApiRequestService apiRequestService)
        {
            _apiRequest = apiRequestService;
        }
    }
}
