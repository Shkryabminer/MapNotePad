using MapNotePad.Models.FBProfile;
using Newtonsoft.Json;

namespace MapNotePad.Models
{
    public class FaceBookProfile : User
    {
        [JsonProperty("Picture")]        
        public Picture Picture { get; set; }

        public string Locale { get; set; }

        public string Link { get; set; }

        public Cover Cover { get; set; }

        [JsonProperty("age_range")]
        public AgeRange AgeRange { get; set; }

        public Device[] Devices { get; set; }

        [JsonProperty("first_name")]
        public override string FirstName { get; set; }

        [JsonProperty("last_name")]
        public override string LastName { get; set; }

        public string Gender { get; set; }

        public bool IsVerified { get; set; }

        public string Id { get; set; }

        [JsonProperty("email")]
        public override string Email { get; set; }
    }
}

