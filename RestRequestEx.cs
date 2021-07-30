using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace L.Util
{
    internal class NullDeserializer : IDeserializer
    {
        public T Deserialize<T>(IRestResponse response)
        {
            return default(T);
        }
    }
    public class LanceJsonSerializer : ISerializer,IDeserializer
    {
        public JsonSerializerSettings JsonSerializerSettings { get; }
        public LanceJsonSerializer(JsonSerializerSettings jsonSerializerSettings=null)
        {
            JsonSerializerSettings = jsonSerializerSettings;
            this.ContentType = "application/json";
        }

        public string Serialize(object obj)
        {
            return obj.ToJsonString(JsonSerializerSettings);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return response.Content.ToEntityFromJson<T>(response.Request.JsonSerializer.AsType<LanceJsonSerializer>().JsonSerializerSettings);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }
    }
    public class RestRequestEx : RestRequest
    {
        public RestRequestEx(string resource, Method method = Method.GET, JsonSerializerSettings jsonSerializerSettings = null) : base(resource, method)
        {
            this.JsonSerializer = new LanceJsonSerializer(jsonSerializerSettings);
            ReadWriteTimeout = Timeout = 3000;
        }
    }
}