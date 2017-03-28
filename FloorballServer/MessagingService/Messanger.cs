using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService
{
    public class Messanger
    {

        private static Messanger instance;

        private string ApiKey = "AAAA6N58lRo:APA91bGnVftytpcm4kXhMiTHMH0e4xIKRzUuJ5ib_a-B36EmlS1NcjiSCvmWqFPU2QL0kuTNDX78aETMkaB8PZTlkpQp7cJSrTqhahU5cHzZAha7HXaVptJ2ynAf6F056Nl_RWEEfE51";
        private string Url = "https://fcm.googleapis.com/fcm/send";
        private Dictionary<string, string> headers;

        private Messanger()
        {
            headers = new Dictionary<string, string>();
            headers.Add("Authorization", "key=" + ApiKey);
            headers.Add("Content-type", "application/json");
        }

        public static Messanger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Messanger();
                }
                return instance;
            }
        }

        public async Task SendNotification(BaseNotification notification)
        {
            var client = new MessagingRestClient(Url);
            await client.ExecuteRequest("", Method.POST, null, null, notification, headers);

        }



    }
}
