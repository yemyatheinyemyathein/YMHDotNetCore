using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YMHDotNetCore.PizzaApi.Db;
using YMHDotNetCore.PizzaApi.Features.Queries;
using YMHDotNetCore.Shared;

namespace YMHDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperService;
        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }
         
        [HttpGet("Extra")]
        public async Task<IActionResult> GetExtraAsync()
        {
            var lst = await _appDbContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }

        //[HttpGet("Order/{invoiceNo}")]
        //public async Task<IActionResult> GetOrder(string invoiceNo)
        //{
        //    var item = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
        //    var lst = await _appDbContext.PizzaOrderDetails.Where(x => x.PizzaOrderInvoiceNo == invoiceNo).ToListAsync();
        //    return Ok(new
        //    {
        //        Order = item,
        //        OrderDetail =  lst
        //    });
        //}

        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModal>
                (
                    PizzaQuery.PizzaOrderQuery,
                    new
                    {
                        PizzaOrderInvoiceNo = invoiceNo
                    }
                );

            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModal>
                (
                    PizzaQuery.PizzaOrderDetailQuery,
                    new
                    {
                        PizzaOrderInvoiceNo = invoiceNo
                    }
                );

            var model = new PizzaOrderInvoiceResponse()
            {
                Order = item,
                OrderDetail = lst
            };

            return Ok(model);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var itemPizza = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = itemPizza.Price;
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (orderRequest.Extras.Length > 0)
            {
                // select * from Tbl_PizzaExtra where PizzaExtraId in (1,2,3,4);
                //foreach(var item in orderRequest.Extras)
                //{}
                var lstExtra = await _appDbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }
            PizzaOrderModal pizzaOrderModal = new PizzaOrderModal()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };
            List<PizzaOrderDetailModal> pizzaExtraModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModal()
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNo
            }).ToList();
            await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModal);
            await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _appDbContext.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total,
            };
            return Ok(response);
        }
    }
}
 