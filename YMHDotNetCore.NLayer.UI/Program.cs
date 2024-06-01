// See https://aka.ms/new-console-template for more information
using YMHDotNetCore.RestApiWithNLayer.Features.Blog;

Console.WriteLine("Hello, World!");

BL_Blog bl_Blog = new BL_Blog();
bl_Blog.GetBlogs();