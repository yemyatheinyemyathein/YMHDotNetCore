// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YMHDotNetCore.ConsoleAppHttpClientExample;

Console.WriteLine("Hello, World!");

// See https://aka.ms/new-console-template for more information
//HttpClient client = new HttpClient();

//var response = await client.GetAsync("https://localhost:7079/api/blog");

////response.RunSynchronously(); // if there is no await
//if (response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr);
//    foreach(var blog in lst)
//    {
//        Console.WriteLine(JsonConvert.SerializeObject(blog));
//        Console.WriteLine($"Title => {blog.BlogTitle}");
//        Console.WriteLine($"Author => {blog.BlogAuthor}");
//        Console.WriteLine($"Content => {blog.BlogContent}");
//    }
//}

HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();

Console.ReadLine();