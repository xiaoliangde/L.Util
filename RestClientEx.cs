using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace L.Util
{
    public class RestClientEx: RestClient
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        public RestClientEx() : base()
        {
            ClearHandlers();
            this.AddHandler("application/json", ()=>new LanceJsonSerializer(JsonSerializerSettings));
            this.AddHandler("application/xml", () => new XmlDeserializer());
            this.AddHandler("text/json", () => new LanceJsonSerializer(JsonSerializerSettings));
            this.AddHandler("text/x-json", () => new LanceJsonSerializer(JsonSerializerSettings));
            this.AddHandler("text/javascript", () => new LanceJsonSerializer(JsonSerializerSettings));
            this.AddHandler("text/xml", () => new XmlDeserializer());
            this.AddHandler("*+json", () => new LanceJsonSerializer(JsonSerializerSettings));
            this.AddHandler("*+xml", () => new XmlDeserializer());

            this.AddHandler("*", () => new NullDeserializer());
            this.AddHandler("text/plain", ()=>new NullDeserializer());

            this.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            Encoding = Encoding.UTF8;
            //this.AddDefaultHeader("Content-Type", "text/html;charset=utf8");
        }

        public RestClientEx(Uri baseUrl)
            : this()
        {
            this.BaseUrl = baseUrl;
        }

        public RestClientEx(string baseUrl)
            : this()
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception(nameof(baseUrl));
            this.BaseUrl = new Uri(baseUrl);
        }

        public Action<IRestResponse> WhenExecutedRq { get; set; }
        public override IRestResponse Execute(IRestRequest request)
        {
            var response = base.Execute(request);
            if(response.StatusCode == HttpStatusCode.Conflict)
                WhenExecutedRq?.Invoke(response);
            return response;
        }
    }
}
