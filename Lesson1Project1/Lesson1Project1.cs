using Lesson1Project1.Models.Dto;
using Lesson1Project1.MyHttpClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson1Project1
{
    class Lesson1Project1
    {
        static async Task Main(string[] args)
        {
            
            using StreamWriter writer = new StreamWriter(new FileStream("result.txt", FileMode.Create, FileAccess.Write));

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(8));
            
            IEnumerable<PostDto> postsDto = await Task.WhenAll(Enumerable.Range(4, 10)
                        .Select(async i => await HttpClientJsonPlaceHolder.GetPost(i, cancellationTokenSource.Token)));
            // сначала думал что тоже нужно вызывать ToList
            // но оказалось он работатет хитрее видимо внутри себя он уже вызывает подобный метод который проходит по всей коллекции
            // в этом можно убедиться если раскомментировать 20 строчку в HttpClientJsonPlaceHolder.cs если бы он использовал что то вроде этого
            // List<PostDto> result = new List<PostDto>();
            // foreach(Task<PostDto> taskPostDto in Enumerable.Range(4, 10).Select(async i => await HttpClientJsonPlaceHolder.GetPost(i)))
            // {
            //     result.Add(await taskPostDto);
            // }
            // то тогда пока не выполнился бы предыдущий запрос новый бы не начался.
            // при условии что таски не были заранее запущены допустим ToList() или ToArray() и т.п. методами


            IEnumerable<string> stringPostsDto = postsDto
                .OrderBy(dto => dto.Id)
                .Select(dto => $"{dto.UserId}\n{dto.Id}\n{dto.Title}\n{dto.Body}");

            await writer.WriteAsync(string.Join(new string('\n', 2), stringPostsDto));

            Console.WriteLine("completed.");

            /*
            await writer.WriteAsync(
                string.Join(new string('\n', 2), Enumerable.Range(4, 10)
                    .Select(async i => await HttpClientJsonPlaceHolder.GetPost(i))
                    //.ToList() // нужно чтобы сначала запустились все таски если убрать то всё будет выполняться последовательно 
                    // или если есть сортировка то это не обязательно т.к. перед сортировкой будет полный проход по всем элементам.
                    // в этом можно убедиться если раскомментировать 20 строчку в HttpClientJsonPlaceHolder.cs и если закоментировать 
                    // ToList() и OrderBy() то тогда время выполнения будет как минимум 5с + время на запросы т.к. в данном случае пока не выполнится
                    // предыдущий запрос новый не начнётся если откоментировать ToList() или OrderBy() то время выполнения будет примерно
                    // 500 мс + самый долгий запрос при закоментированном ToList() и OrderBy() программа выполняется более 5 секунд из за
                    // особенности Linq если идут последоватьльно Selectы и между ними допустим нету сортировки то первый вычисленный элемент
                    // будет сразу пробрасываться на следующий Select а т.к. во втором Select у нас идёт неявное ожидание вызовом свойства Result
                    // объекта Task то следующий элемент на вычисление не пойдёт пока не вычислится текущий если есть сортитровка то она сначала
                    // проходит по всем элементам а уже после начинает сортировку почти тоже самое и с ToList() он заставляет колекцию сформироваться
                    // а уже тольк после прохода по всем элементам будет вызвам следующий Select точнее след. Select вызывается в самом методе
                    // string.Join т.к. после него нет метода который заставляет колекцию свормироваться ведь методы Linq только добавляют правила
                    // для формирования коллекции но не формируют её пока ему это не скажут допустим перебором в foreach или вызовом метода для 
                    // формирования коллекции.
                    .OrderBy(result => result.Result.Id)
                    .Select(result => $"{result.Result.UserId}\n{result.Result.Id}\n{result.Result.Title}\n{result.Result.Body}")));
            */
        }
    }
}
