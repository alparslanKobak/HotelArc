using HotelArc.Kernel.Entities;
using HotelArc.MVCUI.Models;
using HotelArc.Process.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelArc.MVCUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IRoleService _roleService;

        public AuthController(IAppUserService appUserService, IRoleService roleService)
        {
            _appUserService = appUserService;
            _roleService = roleService;
        }

        // GET: AuthController
        public ActionResult Login()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                AppUser user = await _appUserService.GetAppUserByIncludeAsync(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password);

                user.Role = await _roleService.GetRoleByIncludeAsync(x => x.RoleId == user.RoleId);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role.RoleName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.AppUserId.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Login");

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);


                    if (user.Role.RoleName == "Admin")
                    {
                        return RedirectToAction("Index","Home", new {area="Admin"} );
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "<div class='alert alert-danger'>Email or password is wrong.</div>";
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                return View();
            }
        }



        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        public async Task<ActionResult> AccessDenied()
        {
            return View();
        }


    }
}
