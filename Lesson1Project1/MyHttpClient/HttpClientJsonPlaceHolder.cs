using Lesson1Project1.Models.Dto;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace Lesson1Project1.MyHttpClient
{
    public class HttpClientJsonPlaceHolder : IHttpClientJsonPlaceHolder
    {
        private HttpClientJsonPlaceHolder() { }

        async Task<PostDto> IHttpClientJsonPlaceHolder.GetPost(int id, CancellationToken cancellationToken)
        {
            using HttpClient httpClient = new HttpClient();
            //Task.Delay(500).Wait();
            using HttpResponseMessage httpResponseMessage = await new HttpClient()
                .GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}", cancellationToken);
            //await Task.Delay(500);
            return httpResponseMessage.IsSuccessStatusCode ?
                await JsonSerializer.DeserializeAsync<PostDto>(await httpResponseMessage.Content.ReadAsStreamAsync(), 
                    new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken) :
                new PostDto();
        }

        public static async Task<PostDto> GetPost(int id, CancellationToken cancellationToken) =>
            await ((IHttpClientJsonPlaceHolder)new HttpClientJsonPlaceHolder()).GetPost(id, cancellationToken);
    }
}
