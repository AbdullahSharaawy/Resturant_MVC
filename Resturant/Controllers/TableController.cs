using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.Services;

namespace Resturant_PL.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _TS;

        public TableController(ITableService tS)
        {
            _TS = tS;
        }

        public IActionResult Index()
        {
            return View("Tables",_TS.GetList());
        }
    }
}
