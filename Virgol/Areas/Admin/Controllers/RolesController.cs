using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;
using Virgol.App_Start;
using Virgol.Service;
using Virgol.Views.ViewModel;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Areas.Admin.Controllers
{
    public class RolesController : Controller
    {
        private VirgolContext db = new VirgolContext();
        RoleServices _roleService;

        public RolesController()
        {
                _roleService = new RoleServices(db);
        }

        // GET: Admin/Roles
        public ActionResult Index()
        {
            var role=_roleService.GetAll().ToList();
            var roleViewModel = AutoMapperConfig.mapper.Map<List<Role>, List<RoleViewModel>>(role);
            return View(roleViewModel);
        }


        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,RoleTitle")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = AutoMapperConfig.mapper.Map<RoleViewModel, Role>(roleViewModel);

                _roleService.add(role);
                _roleService.save();
                return RedirectToAction("Index");
            }

            return View(roleViewModel);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _roleService.GetEntitiy(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleViewModel=AutoMapperConfig.mapper.Map<Role,RoleViewModel>(role);
            return View(roleViewModel);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,RoleTitle")] RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = AutoMapperConfig.mapper.Map<RoleViewModel, Role> (roleViewModel);

                _roleService.Update(role);          
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roleViewModel);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _roleService.GetEntitiy(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleViewModel = AutoMapperConfig.mapper.Map<Role, RoleViewModel>(role);
            ViewBag.RoleDelete = roleViewModel.RoleTitle;

            return View(roleViewModel);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = _roleService.GetEntitiy(id);
            _roleService.remove(role);
            _roleService.save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _roleService.Dispose();
        }
    }
}
