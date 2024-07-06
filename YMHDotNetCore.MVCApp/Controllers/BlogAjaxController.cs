using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YMHDotNetCore.MVCApp.Db;
using YMHDotNetCore.MVCApp.Models;

namespace YMHDotNetCore.MVCApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDbContext _db;
        public BlogAjaxController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {

            // select * from Tbl_Blog with (no lock) (if we use As no tracking the query will be like this)
            var lst = await _db.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToListAsync();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogCreate(BlogModel blog)
        { 
            await _db.Blogs.AddAsync(blog);
            int result = await _db.SaveChangesAsync();
            string message = result > 0 ? "Saving Sccessfl." : "Saving Failed";
            BlogMessageResponseModel model = new BlogMessageResponseModel()
            {
                IsSccess = result > 0,
                Message = message
            };
            return Json(model);
            //return View("BlogCreate");
            //return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item == null)
            {
                return Redirect("/Blog");
            }

            return View("BlogEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogModel blog)
        {
            var item = await _db.Blogs
                //.AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id);
            if (item == null)
            {
                return Redirect("/Blog");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            // if we use asnotracking()
            //_db.Entry(item).State = EntryState.Modified;

            await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item == null)
            {
                return Redirect("/Blog");
            }

            _db.Blogs.Remove(item);
            await _db.SaveChangesAsync();

            return Redirect("/Blog");
        }
    }
}
