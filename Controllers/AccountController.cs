using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BlogCore.Application.Dtos;
using BlogCore.Common;
using BlogCore.Core.ViewModels;
using BlogCore.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BlogCore.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(Blog_L2Context blogContext, IMapper mapper, IConfiguration configuration, IMemoryCache memoryCache) : base(blogContext, mapper, configuration, memoryCache)
        {
        }
        public IActionResult Index(string returnUrl="")
        {
            //if (!string.IsNullOrWhiteSpace(returnUrl))
            //{
            //    ViewData["ErrorMessage"] = "账号已强制登出";
            //}
            if (TempData["ErrorMessage"] != null) ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            return View("login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogonDto input)
        {
            try
            {
                string passwordHash = Encryption.EncryptToSHA1($"{input.PassWord}LQH");
                var queryUser = await _db.User.Where(u => u.UserName == input.UserName && u.IsActive == true).SingleOrDefaultAsync();
                if (queryUser != null)
                {
                    if (queryUser.LockoutEnabled == true && queryUser.LockoutEndDateUtc > DateTime.Now)
                    {
                        TempData["ErrorMessage"] = "账号被锁定,请稍后在尝试.";
                    }
                    else
                    {
                        if (queryUser.PasswordHash.Equals(passwordHash))
                        {
                            var user = new ClaimsPrincipal(
                            new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name, input.UserName),
                            },
                            CookieAuthenticationDefaults.AuthenticationScheme));
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(5)) // 有效时间
                            });
                            queryUser.AccessFailedCount = 0;
                            queryUser.LockoutEnabled = false;
                            _db.User.Attach(queryUser);
                            _db.Entry(queryUser).State = EntityState.Modified;
                            await _db.SaveChangesAsync();
                            return Redirect("/gadmin");
                        }
                        else
                        {
                            queryUser.AccessFailedCount += 1;
                            TempData["ErrorMessage"] = $"密码错误,请稍后在尝试(剩余错误次数:{3 - queryUser.AccessFailedCount}).";
                            if (queryUser.AccessFailedCount >= 3)
                            {
                                queryUser.LockoutEnabled = true;
                                queryUser.LockoutEndDateUtc = DateTime.Now.AddMinutes(30);
                                TempData["ErrorMessage"] = $"账号被锁定,请稍后在尝试.";
                            }
                            _db.User.Attach(queryUser);
                            _db.Entry(queryUser).State = EntityState.Modified;
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = $"账号无效.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{ex.Message}";
            }
            return Redirect("/account");
        }

        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Register(LogonViewModel input)
        //{
        //    return View();
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/account");
        }
  
    }
}