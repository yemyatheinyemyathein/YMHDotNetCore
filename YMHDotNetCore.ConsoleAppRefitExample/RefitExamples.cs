using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMHDotNetCore.ConsoleAppRefitExample;

public class RefitExamples
{
    private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7074");
    public async Task RunAsync()
    {
        //await ReadAsync();
        await EditAsync(1);
        await EditAsync(100); 
        await CreateAsync("asdfsomething ff", "asdfAuthor ff ", "asdfContent ff ");
        await UpdateAsync(29, "asdfsom t changes n ", "asdfAuthor sf", "asdfContent sdf");
        await DeleteAsync(27);
    }

    private async Task ReadAsync()
    {
        var lst = await _service.GetBlogs();
        foreach (var item in lst)
        {
            Console.WriteLine($"Creat e Id => {item.BlogId}");
            Console.WriteLine($"Creat e Title => {item.BlogTitle}");
            Console.WriteLine($"Create Author => {item.BlogAuthor}");
            Console.WriteLine($"Create Content => {item.BlogContent}");
            Console.WriteLine("-------------------------------");
        }
    }

    private async Task EditAsync(int id)
    {
        try
        {
            var item = await _service.GetBlog(id);
            Console.WriteLine($"Creat e Id => {item.BlogId}");
            Console.WriteLine($"Creat e Title => {item.BlogTitle}");
            Console.WriteLine($"Create Author => {item.BlogAuthor}");
            Console.WriteLine($"Create Content => {item.BlogContent}");
            Console.WriteLine("-------------------------------");
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex2)
        {
            Console.WriteLine(ex2.Message);
        }
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        var message = await _service.CreateBlog(blog);
        Console.WriteLine(message);
    }
    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        try
        {
            var message = await _service.UpdateBlog(id, blog);
            Console.WriteLine(message);
        }catch (ApiException ex) { 
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Content);
        }catch(Exception ex2)
        {
            Console.WriteLine(ex2.Message);
        }
    }
    private async Task DeleteAsync(int id)
    {
        try
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }catch (ApiException ex) {
            Console.WriteLine(ex.StatusCode.ToString());
            Console.WriteLine(ex.Content);
        }catch(Exception ex2)
        {
            Console.WriteLine(ex2.Message);
        }
    }
}
