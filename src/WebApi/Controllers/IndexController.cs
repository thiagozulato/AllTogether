using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public CacheController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string valorCache = await _cache.GetStringAsync("ValorCache");

            if(string.IsNullOrEmpty(valorCache))
            {
                return BadRequest(new {
                    erro = 1,
                    message = "Valor não definido ou expirado"
                });
            }

            return Ok (new {
               valorCache 
            });
        }

        [HttpPost("{valor}")]
        public async Task<IActionResult> Post(string valor)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

            await _cache.SetStringAsync("ValorCache", valor, options);

            return Ok(new {
                message = "Cache definido com sucesso"
            });
        }
    }
}
