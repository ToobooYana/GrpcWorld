using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Repository.Repositories;

namespace GrpcWorld.Performance.Server.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
      private readonly ICategoryRepository _repository;

      public CategoryController(ICategoryRepository repository)
      {
        _repository = repository;
      }

    [HttpGet("{count}", Name = "GetCategories")]
    public async Task<IEnumerable<Category>> GetCategories(int count)
        {
            var all = await _repository.FetchAllAsync(count);

            return all;
        }
    }
}
