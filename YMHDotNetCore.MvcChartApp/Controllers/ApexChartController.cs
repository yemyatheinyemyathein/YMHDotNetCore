using Microsoft.AspNetCore.Mvc;
using YMHDotNetCore.MvcChartApp.Models;
using System.Collections.Generic;

namespace YMHDotNetCore.MvcChartApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult CandleStickChart()
        {
            var model = new CandleStickChartModel
            {
                Data = new List<CandleStickDataPoint>
                {
                    new CandleStickDataPoint { x = "Jan 2015", y = new List<int> { 54, 66, 69, 75, 88 } },
                    new CandleStickDataPoint { x = "Jan 2016", y = new List<int> { 43, 65, 69, 76, 81 } },
                    new CandleStickDataPoint { x = "Jan 2017", y = new List<int> { 31, 39, 45, 51, 59 } },
                    new CandleStickDataPoint { x = "Jan 2018", y = new List<int> { 39, 46, 55, 65, 71 } },
                    new CandleStickDataPoint { x = "Jan 2019", y = new List<int> { 29, 31, 35, 39, 44 } },
                    new CandleStickDataPoint { x = "Jan 2020", y = new List<int> { 41, 49, 58, 61, 67 } },
                    new CandleStickDataPoint { x = "Jan 2021", y = new List<int> { 54, 59, 66, 71, 88 } }
                }
            };

            return View(model);
        }
    }
}
