using FriendZone.Models;
using FriendZone.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FriendZone.Controllers
{
    public class AccountController : Controller
    {
        private FriendZoneEntities context = new FriendZoneEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }


        //POST: /Account/Login
       [HttpPost]
        public async Task<ActionResult> Login(UserViewModel model)
        {
            var result = context.Users
                        .Where(u => u.UserName == model.UserName && u.Password == model.Password)
                        .FirstOrDefault();
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (result != null)
            {
                //ViewBag.Message = "Success";
                Session["UserName"] = model.UserName;
                Session["UserId"] = result.Id;

                SessionVar.UserId = result.Id.ToString();
                SessionVar.UserName = model.UserName;

                TempData["Message"] = "Success";
                TempData["LoggedUserName"] = model.UserName;
                //ViewBag.UserName = model.UserName;
            }
            else
            {
                TempData["Message"] = "Failed";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}