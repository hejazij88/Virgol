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
    public class ArticlesController : Controller
    {
        private VirgolContext db = new VirgolContext();

        ArticleService _articleService;
        UserService _userService;
        CategoryService _categoryService;
        public ArticlesController()
        {
            _articleService = new ArticleService(db);
            _userService = new UserService(db);
            _categoryService = new CategoryService(db);
        }

        // GET: Admin/Articles
        public ActionResult Index()
        {
            
            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "PhoneNumber");
            var articles = _articleService.GetAll().ToList();
            var articleViewModel = AutoMapperConfig.mapper.Map<List<Article>,List<ArticleViewModel>>(articles);
            return View(articleViewModel);
        }

        public ActionResult IndexUser()
        {

            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "PhoneNumber");
            var articles = _articleService.GetAll().Where(a=>a.UserId==ViewBag.UserId).ToList();
            var articleViewModel = AutoMapperConfig.mapper.Map<List<Article>, List<ArticleViewModel>>(articles);
            return View(articleViewModel);
        }

        // GET: Admin/Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _articleService.GetEntitiy(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "PhoneNumber");
            var articleViewModel=AutoMapperConfig.mapper.Map<Article,ArticleViewModel>(article);
            return View(articleViewModel);
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "PhoneNumber");
            return View();
        }

        // POST: Admin/Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleId,CtegoryId,Title,Content")] ArticleViewModel articleViewModel,HttpPostedFileBase imageUpload)
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
                    imageUpload.SaveAs(Server.MapPath("~/Images/Article/") + newImageName);
                }
                articleViewModel.ImageName = newImageName;
                articleViewModel.DateRegester = DateTime.Now;
                articleViewModel.IsActive = false;
                articleViewModel.Like = 0;
                articleViewModel.UserId= _userService.GetAdminId(User.Identity.Name);
                var article=AutoMapperConfig.mapper.Map<ArticleViewModel,Article>(articleViewModel);
                _articleService.add(article);
                _articleService.save();
                return RedirectToAction("Index");
            }

            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "PhoneNumber");
            return View(articleViewModel);
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _articleService.GetEntitiy(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CtegoryId = new SelectList(db.Ctegories, "CtegoryId", "Title", article.CtegoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "PhoneNumber", article.UserId);
            var articleViewModel=AutoMapperConfig.mapper.Map<Article,ArticleViewModel>(article);
            return View(articleViewModel);
        }

        // POST: Admin/Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,CtegoryId,Title,Content,ImageName,DateRegester,IsActive,UserId,Like,ComentId")] ArticleViewModel articleViewModel,HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null )
                {
                    if (articleViewModel.ImageName != "noPhoto.png")
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Article/") + articleViewModel.ImageName);
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/Images/Article/") + articleViewModel.ImageName);
                        articleViewModel.ImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    }
                    imageUpload.SaveAs(Server.MapPath("~/Images/Article/" + articleViewModel.ImageName));
                }
                var article= AutoMapperConfig.mapper.Map<ArticleViewModel, Article>(articleViewModel);
                _articleService.Update(article);
                _articleService.save();
                return RedirectToAction("Index");
            }
            ViewBag.CtegoryId = new SelectList(db.Ctegories, "CtegoryId", "Title", articleViewModel.CtegoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "PhoneNumber", articleViewModel.UserId);
            return View(articleViewModel);
        }

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = _articleService.GetEntitiy(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            ViewBag.UserId = new SelectList(_userService.GetAll(), "UserId", "PhoneNumber");
            var articleViewModel = AutoMapperConfig.mapper.Map<Article,ArticleViewModel>(article);
            return View(articleViewModel);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = _articleService.GetEntitiy(id);
            _articleService.remove(article);
            _articleService.save();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
       _articleService.Dispose();
        }
    }
}
