using Microsoft.AspNetCore.Mvc;

namespace YMHDotNetCore.MvcApp2.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ExampleChart()
        {
            return View();
        }
        public IActionResult BorderRadiuBarChart()
        {
            return View();
        }
    }
}
