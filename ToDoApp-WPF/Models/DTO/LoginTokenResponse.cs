using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp_WPF.Models.DTO
{
    class LoginTokenResponse
    {
        public override string ToString()
        {
            return AccessToken;
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string Token_Type { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string Expires_In { get; set; }
    }
}
