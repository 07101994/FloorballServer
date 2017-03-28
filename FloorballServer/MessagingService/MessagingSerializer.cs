using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingService
{
    public interface IJsonSerializer : ISerializer, IDeserializer
    {

    }

    class MessagingSerializer : IJsonSerializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        public MessagingSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType
        {
            get
            {
                return "application/json";
            }

            set
            { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    System.Diagnostics.Debug.WriteLine(stringWriter.ToString());

                    return stringWriter.ToString();
                }
            }
        }

        public static MessagingSerializer Instance
        {
            get
            {
                return new MessagingSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }
}
