using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _TS;

        public TableController(ITableService tS)
        {
            _TS = tS;
        }

        public async Task<IActionResult> Index()
        {
            return View("Tables", await _TS.GetList());
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _TS.GetUpdateTableInfo(id));
        }

        public async Task<IActionResult> Create()
        {
            return View("Create", await _TS.GetCreateTableDTOInfo());
        }

        public async Task<IActionResult> SaveEdit(UpdateTableDTO _UpdateTable)
        {
            if (await _TS.Update(_UpdateTable.tableDTO) == null)
            {
                RedirectToAction("Update", _UpdateTable);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Tables", await _TS.GetList());
        }

        public async Task<IActionResult> SaveNew(UpdateTableDTO _CreateTable)
        {
            if (await _TS.Create(_CreateTable.tableDTO) == null)
            {
                RedirectToAction("Create", _CreateTable);
                TempData["ErrorMessage"] = "Failed to create a new record.";
            }
            else
            {
                TempData["SuccessMessage"] = "New Record is created successfully!";
            }
            return View("Tables", await _TS.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _TS.Delete(id))
            {
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the record.";
            }
            return RedirectToAction("Index");
        }
    }
}