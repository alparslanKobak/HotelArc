using HotelArc.Kernel.Entities;
using HotelArc.Process.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace HotelArc.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUsersController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IRoleService _roleService;

        public AppUsersController(IAppUserService appUserService, IRoleService roleService)
        {
            _appUserService = appUserService;
            _roleService = roleService;
        }



        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            IEnumerable<AppUser> appUsers = await _appUserService.GetAppUsersByIncludeAsync();
            return View(appUsers);
        }



        // GET: AppUsersController/Create
        public async Task<ActionResult> Create()
        {
            IEnumerable<Role> roles = await _roleService.GetAllRolesByIncludeAsync();
            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppUser collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _appUserService.AddAsync(collection);
                }

                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        TempData["Message"] += $"<div class='alert alert-danger'>{error.ErrorMessage}</div>";
                    }
                    // İlgili işlemleri yapın
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                IEnumerable<Role> roles = await _roleService.GetAllRolesByIncludeAsync();
                ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");

            }
            return RedirectToAction(nameof(Create), "AppUsers");
        }

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            IEnumerable<Role> roles = await _roleService.GetAllRolesByIncludeAsync();
            ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            return View(await _appUserService.GetAppUserByIncludeAsync(id));

        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, AppUser collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _appUserService.UpdateAsync(collection, id);
                }

                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        TempData["Message"] += $"<div class='alert alert-danger'>{error.ErrorMessage}</div>";
                    }
                    // İlgili işlemleri yapın
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                IEnumerable<Role> roles = await _roleService.GetAllRolesByIncludeAsync();
                ViewBag.RoleId = new SelectList(roles, "RoleId", "RoleName");
            }
            return RedirectToAction(nameof(Edit), "AppUsers", id);
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            AppUser model = await _appUserService.FindAsync(id);
            return View(model);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, AppUser collection)
        {
            try
            {
                await _appUserService.SoftDeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
