using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV20T1020570.BusinessLayers;
using SV20T1020570.DomainModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SV20T1020570.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Adminstrator},{WebUserRoles.Employee}")]

    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult AccessDenined()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username = "", string password = "")
        {
            ViewBag.UserName = username;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("Error", "Nhập tên đăng nhập và mật khẩu!!!");
                return View();
            }
            var userAccount = UserAccountService.Authorize(username, password);
            if (userAccount == null)
            {
                ModelState.AddModelError("Error", "Đăng nhập thất bại!!!");
                return View();
            }
            // Đăng nhập thành công, tạo dữ liệu để lưu thông tin 
            var userData = new WebUserData()
            {
                UserId = userAccount.UserID,
                UserName = userAccount.UserName,
                DisplayName = userAccount.FullName,
                Email = userAccount.Email,
                Photo = userAccount.Photo,
                ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                SessionId = HttpContext.Session.Id,
                AdditionalData = "",
                Roles = userAccount.RoleNames.Split(',').ToList(),
            };
            await HttpContext.SignInAsync(userData.CreatePrincipal());
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login"); 
        }
        public IActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (HttpContext.Request.Method == "POST")
            {
                if (!UserAccountService.CheckPassword(userName, oldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Mật khẩu cũ không chính xác.");
                    return View();
                }
                if (string.IsNullOrEmpty(newPassword))
                {
                    ViewBag.OldPassword = oldPassword;
                    ModelState.AddModelError("NewPassword", "Vui lòng nhập mật khẩu mới.");
                    return View();
                }
                if (UserAccountService.CheckPassword(userName, newPassword))
                {
                    ViewBag.OldPassword = oldPassword;
                    ModelState.AddModelError("NewPassword", "Mật khẩu mới không được trùng với mật khẩu cũ");
                    return View();
                }
                UserAccountService.ChangePassword(userName, oldPassword, newPassword);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
