using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcWorld.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Repository.Repositories;

namespace GrpcWorld.Performance.Server.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public OrderController(
            IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet("{count}", Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts(int count)
        {
            var all = await _productsRepository.FetchAllAsync(count);

            return all;
        }

        [HttpGet("{id}", Name = "GetOne")]
        public async Task<Product> GetOne(int id)
        {
            var one = await _productsRepository.GetByIdAsync(id);

            return one;
        }

        [HttpGet("/ping", Name = "Ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
