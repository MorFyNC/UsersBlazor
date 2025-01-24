using System.Text.Json.Serialization;

namespace TestBlazor3K.ApiRequest.Model
{
    public class UserDataShort
    {
        public int id_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isAdmin { get; set; }
    }

    public class UserData
    {
       public bool status { get; set; }
       public UserDataContainer data { get; set; }
    }

    public class UserDataContainer
    {
        public List<UserDataShort> users { get; set; }
    }

    public class ReqDataUser
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UserAddData
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    
    public class ReqUserLogin
    {
        [JsonPropertyName("Login")]
        public string Login { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        [JsonPropertyName("data")]
        public LoginResponseDataModel Data { get; set; }
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

    }
    public class LoginResponseDataModel
    {
        [JsonPropertyName("jwt")]
        public string JWT { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
