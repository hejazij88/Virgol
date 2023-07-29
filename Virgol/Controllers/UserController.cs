using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Virgol_Model.Context;
using System.Web.Mvc;
using Virgol.Service;
using System.Web.Security;
using Virgol.Views.ViewModel;
using Virgol.App_Start;
using Virgol_Model;

namespace Virgol.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        VirgolContext db = new VirgolContext();
        private UserService _userService;
        private RoleServices _roleService;
        public UserController()
        {
            _userService = new UserService(db);
            _roleService= new RoleServices(db);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = "/")
        {
            UserLogInViewModel model = new UserLogInViewModel() { ReturnUrl = returnUrl };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "PhoneNumber,Password,returnUrl,SavePassword")] UserLogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pass = HashPass.GetSha256(model.Password);

                var admin = _userService.GetAll().FirstOrDefault(t => t.PhoneNumber == model.PhoneNumber && t.Password == pass);

                if (admin != null)
                {
                    if (model.ReturnUrl != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.PhoneNumber, model.SavePassword);
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.PhoneNumber, model.SavePassword);
                        return Redirect("/Admin/Default/Index");
                    }
                }
                ModelState.AddModelError("PhoneNumber", "شماره موبایل یا رمز عبور شما اشتباه است!");
                return View(model);
            }
            return View(model);
        }

        public ActionResult SingOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult State()
        {
            ViewBag.State = false;
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.State = true;
            }
            return PartialView();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp([Bind(Include = "UserId,PhoneNumber,Password,Name,Family")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                userViewModel.RegesterDate = DateTime.Now;
                userViewModel.IsActive = true;
                userViewModel.RoleId = 2;
                userViewModel.Password = HashPass.GetSha256(userViewModel.Password);
                var user = AutoMapperConfig.mapper.Map<UserViewModel, User>(userViewModel);

                _userService.add(user);
                _userService.save();
                return Redirect("/Home/Index");
            }

            ViewBag.RoleId = new SelectList(_roleService.GetAll(), "RoleID", "RoleTitle", userViewModel.RoleId);
            return View("/");
        }
    }
}