using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Virgol.App_Start;
using Virgol.Service;
using Virgol.Views.ViewModel;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private VirgolContext db = new VirgolContext();

        UserService _userService;
        RoleServices _role;
        public UsersController()
        {
            _userService = new UserService(db);
            _role = new RoleServices(db);
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            ViewBag.RoleId = new SelectList(_role.GetAll(), "RoleID", "RoleTitle");
            var users= _userService.GetAll().ToList();
            var usweViewModel = AutoMapperConfig.mapper.Map<List<User>, List<UserViewModel>>(users);
            return View(usweViewModel);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetEntitiy(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(_role.GetAll(), "RoleID", "RoleTitle");
            var usweViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);

            return View(usweViewModel);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_role.GetAll(), "RoleID", "RoleTitle");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,PhoneNumber,Password,Name,Family")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                userViewModel.RegesterDate = DateTime.Now;
                userViewModel.IsActive = true;
                userViewModel.RoleId = 2;
                userViewModel.Password = HashPass.GetSha256(userViewModel.Password);
                var user = AutoMapperConfig.mapper.Map<UserViewModel , User> (userViewModel);

               _userService.add(user);
                _userService.save();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(_role.GetAll(), "RoleID", "RoleTitle", userViewModel.RoleId);
            return View(userViewModel);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetEntitiy(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);
            ViewBag.RoleId = new SelectList(_role.GetAll(), "RoleID", "RoleTitle", userViewModel.RoleId);


            return View(userViewModel);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,PhoneNumber,Password,RoleId,IsActive,RegesterDate,Name,Family")] UserViewModel userViewModel)
        {
            userViewModel.Password = HashPass.GetSha256(userViewModel.Password);
            var user = AutoMapperConfig.mapper.Map<UserViewModel, User>(userViewModel);
            if (ModelState.IsValid)
            {

                _userService.Update(user);
                _userService.save();
                return RedirectToAction("Index");
            }
            
            return View(userViewModel);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.RoleId = new SelectList(_role.GetAll(), "RoleID", "RoleTitle");
            User user = _userService.GetEntitiy(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userViewModel = AutoMapperConfig.mapper.Map<User, UserViewModel>(user);
            ViewBag.Delete = userViewModel.Name;
            return View(userViewModel);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = _userService.GetEntitiy(id);
            _userService.remove(user);
            _userService.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
        }
    }
}
