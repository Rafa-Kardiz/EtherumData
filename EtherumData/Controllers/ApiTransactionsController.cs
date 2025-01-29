using EtherumData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtherumData.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class ApiTransactionsController : ControllerBase
    {
        private readonly EthereumDataContext _context;

        public ApiTransactionsController(EthereumDataContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GET()
            => await _context.Transactions.ToListAsync();
    }
}
