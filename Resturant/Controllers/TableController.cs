using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
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
        public IActionResult Update(int id)
        {
            TableDTO table = _TS.GetById(id);
            if (table == null)
            {
                return NotFound();
            }
            return View("Update",table);
        }
       
    }
}
