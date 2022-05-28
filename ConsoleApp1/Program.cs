using AuthenticateService;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new UserServiceTest().GenerateJwtToken(0));
            Console.WriteLine(new UserServiceTest().GenerateJwtToken(1));
            Console.ReadKey();
        }
    }
}
