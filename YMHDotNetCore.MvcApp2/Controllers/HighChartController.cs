using Microsoft.AspNetCore.Mvc;

namespace YMHDotNetCore.MvcApp2.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
