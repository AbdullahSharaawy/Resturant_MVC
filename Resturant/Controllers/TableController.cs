﻿using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels;
using Resturant_BLL.Services;
using Resturant_DAL.Entities;
using Resturant_BLL.Mapperly;
namespace Resturant_PL.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _TS;
        private readonly IBranchService _BS;

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
           
            return View("Update", _TS.GetUpdateTableInfo(id));
        }
        public IActionResult Create()
        {
            return View("Create",_TS.GetCreateTableDTOInfo());
        }
        public IActionResult SaveEdit(UpdateTableDTO _UpdateTable)
        {
            
            if(_TS.Update(_UpdateTable.tableDTO)==null)
            {
                RedirectToAction("Update",_UpdateTable);
                TempData["ErrorMessage"] = "Failed to update the record.";
            }
            else
            {
                TempData["SuccessMessage"] = "Record updated successfully!";
                
            }
                return View("Tables", _TS.GetList());
        }
        public IActionResult SaveNew(UpdateTableDTO _CreateTable)
        {

            if (_TS.Create(_CreateTable.tableDTO) == null)
            {
                RedirectToAction("Create", _CreateTable);

                TempData["ErrorMessage"] = "Failed to create a new record.";

            }
            else
            {
                TempData["SuccessMessage"] = "New Record is created successfully!";
            }
            return View("Tables", _TS.GetList());
        }
        public IActionResult Delete(int id)
        {
            if (_TS.Delete(id))
            {
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            // If deletion fails, you might want to show an error message
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the record.";
            }
            return RedirectToAction("Index");
        }
    }
}
