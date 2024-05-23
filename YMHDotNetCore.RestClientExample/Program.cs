// See https://aka.ms/new-console-template for more information
using YMHDotNetCore.ConsoleAppRestClientExample;

Console.WriteLine("Hello, World!");

RestClientExample restClientExample = new RestClientExample();
restClientExample.RunAsync();

Console.ReadLine();