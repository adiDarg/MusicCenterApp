using System.Net.Http.Formatting;

namespace WebApiClient
{
    public class WebClient<T> : IWebClient<T>
    {
        UriBuilder uriBuilder;
        HttpRequestMessage request;
        HttpResponseMessage response;
        public string Schema
        {
            set { this.uriBuilder.Scheme = value; }
        }
        public string Host
        {
            set { this.uriBuilder.Host = value; }
        }
        public int port
        {
            set { this.uriBuilder.Port = value; }
        }
        public string Path
        {
            set { this.uriBuilder.Path = value; }
        }
        public string controller
        {
            set { this.controller = value; }
            get { return this.controller; }
        }
        public WebClient()
        {
            this.uriBuilder = new UriBuilder();
            this.uriBuilder.Query = string.Empty;
            this.request = new HttpRequestMessage();
        }
        public void AddParams(string name,string value)
        {
            if (this.uriBuilder.Query.Equals(string.Empty))
            {
                this.uriBuilder.Query = "?";
            }
            else
            {
                this.uriBuilder.Query += "&";
            }
            this.uriBuilder.Query += $"{name}={value}";
        }
        public async Task<T> GetAsync()
        {
            this.request.Method = HttpMethod.Get;
            this.request.RequestUri = this.uriBuilder.Uri;
            using (HttpClient client = new HttpClient())
            {
                this.response = await client.SendAsync(this.request);
                if (this.response.IsSuccessStatusCode == true)
                {
                    return await this.response.Content.ReadAsAsync<T>();
                }
            }
            return default;
        }

        public async Task<bool> PostAsync(T model)
        {
            this.request.Method= HttpMethod.Post;
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            this.request.Content = objectContent;
            request.RequestUri = this.uriBuilder.Uri;
            using (HttpClient client = new HttpClient())
            {
                this.response = await client.SendAsync(this.request);
                if (this.response.IsSuccessStatusCode == true)
                {
                    return await this.response.Content.ReadAsAsync<bool>();
                }
            }
            return false;
        }

        public async Task<bool> PostAsync(T model, Stream file)
        {
            this.request.Method = HttpMethod.Post;
            this.request.RequestUri = new Uri(this.uriBuilder.ToString());
            MultipartFormDataContent content = new MultipartFormDataContent();
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            StreamContent streamContent = new StreamContent(file);
            content.Add(objectContent);
            content.Add(streamContent);
            this.request.Content = content;
            using (HttpClient client = new HttpClient())
            {
                this.response = await client.SendAsync(this.request);
                if (this.response.IsSuccessStatusCode == true)
                {
                    return await this.response.Content.ReadAsAsync<bool>();
                }
            }
            return false;
        }

        public async Task<bool> PostAsync(T model, List<Stream> files)
        {
            this.request.Method = HttpMethod.Post;
            this.request.RequestUri = new Uri(this.uriBuilder.ToString());
            MultipartFormDataContent content = new MultipartFormDataContent();
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            content.Add(objectContent, "model");
            foreach (Stream file in files)
            {
                StreamContent streamContent = new StreamContent(file);
                content.Add(streamContent,"file");
            }
            this.request.Content = content;
            using (HttpClient client = new HttpClient())
            {
                this.response = await client.SendAsync(this.request);
                if (this.response.IsSuccessStatusCode == true)
                {
                    return await this.response.Content.ReadAsAsync<bool>();
                }
            }
            return false;
        }
    }
}
