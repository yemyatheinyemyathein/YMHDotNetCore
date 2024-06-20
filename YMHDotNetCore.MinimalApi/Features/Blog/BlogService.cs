using Microsoft.EntityFrameworkCore;
using YMHDotNetCore.MinimalApi.Db;
using YMHDotNetCore.MinimalApi.Models;

namespace YMHDotNetCore.MinimalApi.Features.Blog
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder MapBlog(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Blog", async (AppDbContext db) =>
            {
                var lst = await db.Blogs.AsNoTracking().ToListAsync();
                return Results.Ok(lst);
            });

            app.MapPost("/api/Blog", async (AppDbContext db, BlogModel blog) =>
            {
                await db.Blogs.AddAsync(blog);
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Posting Successful " : "Posting Failed";
                return Results.Ok(message);
            });

            app.MapPut("/api/Blog/{id}", async (AppDbContext db, BlogModel blog, int id) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No Data Found");
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                var result = await db.SaveChangesAsync();

                string message = result > 0 ? "Updating Successful " : "Updating Failed";
                return Results.Ok(message);
            });

            app.MapPut("/api/Blog/{id}", async (AppDbContext db, BlogModel blog, int id) =>
            {
                var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.NotFound("No Data Found");
                }

                db.Blogs.Remove(item);
                var result = db.SaveChanges();

                string message = result > 0 ? "Deleting Successful " : "Deleting Failed";
                return Results.Ok(message);
            });

            // if we return we can add by more then of it. from calling of this one 
            return app;
        }
    }
}
