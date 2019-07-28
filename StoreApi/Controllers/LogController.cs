using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Interfaces;
using Store.Core.Product;

namespace StoreApi.Controllers
{
    [Route("logs")]
    public class LogController : BaseController
    {
        private readonly IRepository<PriceUpdateLogEntity> _priceUpdateLogRepository;
        private readonly IRepository<PurchaseLogEntity> _purchaseLogRepository;

        public LogController(IRepository<PurchaseLogEntity> purchaseLogRepository, IRepository<PriceUpdateLogEntity> priceUpdateLogRepository)
        {
            _priceUpdateLogRepository = priceUpdateLogRepository;
            _purchaseLogRepository = purchaseLogRepository;
        }

        [HttpGet]
        [Route("price")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPriceUpdateLogs()
        {
            return Ok(_priceUpdateLogRepository.FindAll());
        }

        [HttpGet]
        [Route("purchase")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPurchaseLogs()
        {
            return Ok(_purchaseLogRepository.FindAll());
        }
    }
}
