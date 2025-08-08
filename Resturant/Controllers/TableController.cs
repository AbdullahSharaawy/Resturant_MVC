using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _TS;
        private readonly IBranchService _BS;
        public TableController(ITableService tS, IBranchService bS)
        {
            _TS = tS;
            _BS = bS;
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
            
            if (!ModelState.IsValid)
            {
               
                _UpdateTable.Branches=await _BS.GetList();
               
                return View("Update", _UpdateTable);
            }
            else
            {
               if( await _TS.Update(_UpdateTable.tableDTO) == null)
                {
                    return View("Update", _UpdateTable);
                }
                    
                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Tables", await _TS.GetList());
        }

        public async Task<IActionResult> SaveNew(UpdateTableDTO _CreateTable)
        {

            if (!ModelState.IsValid)
            {

                _CreateTable.Branches = await _BS.GetList();

                return View("Update", _CreateTable);
            }
            else
            {
                if (await _TS.Create(_CreateTable.tableDTO) == null)
                {
                    return View("Update", _CreateTable);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Tables", await _TS.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _TS.Delete(id) )
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