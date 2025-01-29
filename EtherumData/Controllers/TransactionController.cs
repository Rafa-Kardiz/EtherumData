using EtherumData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtherumData.Controllers
{
    public class TransactionController : Controller
    {
        private readonly EthereumDataContext _context;

        public TransactionController(EthereumDataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
            => View(await _context.Transactions.ToListAsync());
    }
}
