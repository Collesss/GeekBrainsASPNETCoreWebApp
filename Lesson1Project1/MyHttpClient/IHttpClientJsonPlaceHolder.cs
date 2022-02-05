using Lesson1Project1.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Project1.MyHttpClient
{
    interface IHttpClientJsonPlaceHolder
    {
        /// <summary>
        /// func get post from https://jsonplaceholder.typicode.com/ by id
        /// </summary>
        /// <param name="id">id post</param>
        /// <returns>return PostDto</returns>
        public Task<PostDto> GetPost(int id);
    }
}
