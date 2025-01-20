using TestBlazor3K.ApiRequest.Model;

namespace TestBlazor3K.UserContext
{
    public class UserContext
    {
        public UserDataShort CurrentUser { get; set; }
        public bool IsCurrentUserAdmin { get => CurrentUser.isAdmin; }
    }
}
