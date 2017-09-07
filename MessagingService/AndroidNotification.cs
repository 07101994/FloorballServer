using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService
{
    public class AndroidNotification : BaseNotification
    {
        //[JsonProperty("notification")]
        //public Notification Notification { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }

    }

    public class Notification
    {
        [JsonProperty("title_loc_key")]
        public string TitleLocKey { get; set; }
        [JsonProperty("title_loc_args")]
        public string TitleLocArgs { get; set; }
        [JsonProperty("sound")]
        public string Sound { get; set; }
        [JsonProperty("click_action")]
        public string Action { get; set; }
        [JsonProperty("body_loc_key")]
        public string BodyLocKey { get; set; }
        [JsonProperty("body_loc_args")]
        public string BodyLocArgs { get; set; }
    }

    public class Data
    {
        [JsonProperty("messageType")]
        public string MessageType { get; set; }
        [JsonProperty("entity")]
        public string Entity { get; set; }
        [JsonProperty("titleArgs")]
        public List<string> TitleArgs { get; set; }
        [JsonProperty("bodyArgs")]
        public List<string> BodyArgs { get; set; }

    }

}
