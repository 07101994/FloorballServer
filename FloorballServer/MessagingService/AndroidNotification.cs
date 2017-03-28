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
        [JsonProperty("notification")]
        public Notification Notification { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }

    }

    public class Notification
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }

}
