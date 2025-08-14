using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{

    public class ReservedTablesController : Controller
    {
        private readonly IReservedTableService _RTS;

        public ReservedTablesController(IReservedTableService rTS)
        {
            _RTS = rTS;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View("Index",await _RTS.GetList());
        }
    }
}
