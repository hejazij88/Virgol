using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class CtegoriesController : Controller
    {
        private VirgolContext db = new VirgolContext();
        CategoryService _CategoryService;

        public CtegoriesController()
        {
            _CategoryService = new CategoryService(db);
        }

        // GET: Admin/Ctegories
        public ActionResult Index()
        {
            var category = _CategoryService.GetAll().ToList();
            var categoryViewModel=AutoMapperConfig.mapper.Map<List<Ctegory>,List<CategoryViewModel>>(category);
            return View(categoryViewModel);
        }

        // GET: Admin/Ctegories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ctegory ctegory = _CategoryService.GetEntitiy(id.Value);
            if (ctegory == null)
            {
                return HttpNotFound();
            }
            var categoryViewModel = AutoMapperConfig.mapper.Map<Ctegory, CategoryViewModel>(ctegory);
            return View(categoryViewModel);
        }

        // GET: Admin/Ctegories/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CtegoryId,Title")] CategoryViewModel ctegoryView, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                string newImageName = "noPhoto.png";
                if (imageUpload != null)
                {
                    if (imageUpload.ContentType != "image/jpeg" && imageUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("imageUpload", "تصویر شما فقط باید با فرمت png یا jpg یا jpeg باشد!");
                        return View();
                    }
                    if (imageUpload.ContentLength > 300000)
                    {
                        ModelState.AddModelError(" imageUpload ", "سایز تصویر شما نباید بیشتر از 300 کیلوبایت باشد!");
                        return View();
                    }

                    newImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("~/Images/Category/") + newImageName);
                }
                ctegoryView.ImageName = newImageName;
                var ctegory=AutoMapperConfig.mapper.Map<CategoryViewModel,Ctegory>(ctegoryView);
                _CategoryService.add(ctegory);
                _CategoryService.save();
                return RedirectToAction("Index");
            }

            return View(ctegoryView);
        }

        // GET: Admin/Ctegories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ctegory ctegory = _CategoryService.GetEntitiy(id.Value);
            if (ctegory == null)
            {
                return HttpNotFound();
            }
            var ctegoryView = AutoMapperConfig.mapper.Map<Ctegory, CategoryViewModel>(ctegory);

            return View(ctegoryView);
        }

        // POST: Admin/Ctegories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CtegoryId,Title,ImageName")] CategoryViewModel ctegoryView,HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if(imageUpload!=null)
                {
                    if(ctegoryView.ImageName!="noPhoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Category/") + ctegoryView.ImageName);
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Category/" )+ ctegoryView.ImageName);
                        ctegoryView.ImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    }
                    imageUpload.SaveAs(Server.MapPath("~/Images/Category/" + ctegoryView.ImageName));
                }
                var ctegory=AutoMapperConfig.mapper.Map<CategoryViewModel,Ctegory>(ctegoryView);
               _CategoryService.Update(ctegory);
                _CategoryService.save();
                return RedirectToAction("Index");
            }
            return View(ctegoryView);
        }

        // GET: Admin/Ctegories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ctegory ctegory = _CategoryService.GetEntitiy(id.Value);
            if (ctegory == null)
            {
                return HttpNotFound();
            }
            var ctegoryView = AutoMapperConfig.mapper.Map<Ctegory, CategoryViewModel>(ctegory);
            ViewBag.TitleCategory=ctegoryView.Title;
            return View(ctegoryView);
        }

        // POST: Admin/Ctegories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ctegory ctegory = _CategoryService.GetEntitiy(id);
            if (ctegory != null)
            {
                _CategoryService.remove(ctegory);
                _CategoryService.save();
                return RedirectToAction("Index");
            }
            return Content("کاربر یافت نشد!");
        }


        protected override void Dispose(bool disposing)
        {
            _CategoryService.Dispose();
        }
    }
}
