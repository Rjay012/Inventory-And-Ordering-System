using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InventoryAndOrderingSystem.Models.LoginModels;
using InventoryAndOrderingSystem.Services.LoginServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAndOrderingSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            //prevent user from accessing login page if their cookie is not yet expired
            if(User.Identity.IsAuthenticated)  
            {
                string role = GetClaim().ElementAt(2).Value;
                if(role == "Manager")
                {
                    return RedirectToAction("Index", "Order");
                }
                else if(role == "Customer")
                {
                    return RedirectToAction("Selection", "Product");
                }
            }
            return View();
        }

        public IActionResult LoginCard()
        {
            return PartialView("Partials/Cards/_LoginForm");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind]LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                LoginModel loginCredentials = _loginService.Login(loginModel)
                                                           .Select(l => new LoginModel
                                                           {
                                                               UserID = l.UserID,
                                                               Username = l.Username,
                                                               Role = l.Role.RoleName
                                                           })
                                                           .FirstOrDefault();

                if (loginCredentials == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                await SetLoginCredentials(loginCredentials);

                switch (loginCredentials.Role)
                {
                    case "Manager":
                        return RedirectToAction("Index", "Order");
                    case "Customer":
                        return RedirectToAction("Selection", "Product");
                }
            }

            return View(nameof(Index));
        }

        private async Task<IActionResult> SetLoginCredentials([Bind("UserID, Username, Role")] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                List<Claim> loginCredentials = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, loginModel.UserID.ToString()),
                        new Claim(ClaimTypes.Name, loginModel.Username),
                        new Claim(ClaimTypes.Role, loginModel.Role)
                    };
                var userIdentity = new ClaimsIdentity(loginCredentials, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);

                return Ok();
            }

            return NoContent();
        }

        [Authorize(Roles = "Manager, Customer")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        private IEnumerable<Claim> GetClaim()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            return claims;
        }
    }
}