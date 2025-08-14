using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Resturant_BLL.DTOModels.ChifDTOS;
using Resturant_BLL.Services;
using System.Threading.Tasks;

namespace Resturant_PL.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Authorize]
    public class ChiefController : Controller
    {
        private readonly IChiefService _CS;
        private readonly IBranchService _BS;

        public ChiefController(IChiefService chiefService, IBranchService bS)
        {
            _CS = chiefService;
            _BS = bS;
        }

        public async Task<IActionResult> Index()
        {
            return View("Chiefs", await _CS.GetList());
        }
        [AllowAnonymous]
        public async Task<IActionResult> ChiefsPartialView()
        {
            ChiefsWithSystemBranchesDTO chiefWithSystemBranchesDTO = new ChiefsWithSystemBranchesDTO();
            chiefWithSystemBranchesDTO.Branches=await _BS.GetList();
            return PartialView("_Chiefs", chiefWithSystemBranchesDTO);
        }
        [AllowAnonymous]
        public async Task<IActionResult> selectChiefsOfBranch(int branchId)
        {
            ChiefsWithSystemBranchesDTO chiefWithSystemBranchesDTO = new ChiefsWithSystemBranchesDTO();
            chiefWithSystemBranchesDTO.Branches = await _BS.GetList();
            chiefWithSystemBranchesDTO.chiefsDTO=await _CS.GetList(c=>c.BranchID==branchId && !c.IsDeleted);
            return PartialView("_Chiefs", chiefWithSystemBranchesDTO);
        }
        public async Task<IActionResult> Update(int id)
        {
            return View("Update", await _CS.GetUpdateChiefInfo(id));
        }

        public async Task<IActionResult> Create()
        {
            return View("Create", await _CS.GetCreateChiefInfo());
        }

        public async Task<IActionResult> SaveEdit(ManageChiefDTO _UpdateChief)
        {

            if (!ModelState.IsValid)
            {

                _UpdateChief.Branches = await _BS.GetList();

                return View("Update", _UpdateChief);
            }
            else
            {
                if (await _CS.Update(_UpdateChief.chiefDTO) == null)
                {
                    return View("Update", _UpdateChief);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Chiefs", await _CS.GetList());
        }

        public async Task<IActionResult> SaveNew(ManageChiefDTO _CreateChief)
        {
            if (!ModelState.IsValid)
            {

                _CreateChief.Branches = await _BS.GetList();

                return View("Update", _CreateChief);
            }
            else
            {
                if (await _CS.Create(_CreateChief.chiefDTO) == null)
                {
                    return View("Update", _CreateChief);
                }

                else
                    TempData["SuccessMessage"] = "Record updated successfully!";
            }
            return View("Chiefs", await _CS.GetList());
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _CS.Delete(id) )
            {
                TempData["SuccessMessage"] = "Record deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the record.";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }


    }
}