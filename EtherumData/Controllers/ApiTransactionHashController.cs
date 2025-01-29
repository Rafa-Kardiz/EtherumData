using EtherumData.Models;
using EtherumData.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtherumData.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class ApiTransactionHashController : ControllerBase
    {
        private readonly EthereumService _ethereumService;
        public ApiTransactionHashController(EthereumService ethereumService)
        {
            _ethereumService = ethereumService;
        }

        [HttpGet("{hash}")]
        public async Task<ActionResult<Transaction>> GetTransactionHash(string hash)
        {
            var (transaction, errorMessage) = await _ethereumService.GetTransactionHash(hash);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return BadRequest(errorMessage ?? "Error desconocido.");

        }
    }
}
