using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Virgol.App_Start;
using Virgol.Service;
using Virgol.Views.ViewModel;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        CategoryService _categoryService;
        VirgolContext db=new VirgolContext();

        public ArticleController()
        {
            _articleService = new ArticleService(db);
            _categoryService= new CategoryService(db);
        }
        // GET: Article
        public ActionResult Index(int? Id)
        {
            if(Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CtegoryId = new SelectList(_categoryService.GetAll(), "CtegoryId", "Title");
            var article = _articleService.GetEntitiy(Id.Value);
            if (article == null || !article.IsActive)
            {
                return HttpNotFound();
            }
            var arricleView = AutoMapperConfig.mapper.Map<Article, ArticleViewModel>(article);
            return View(arricleView);
        }

        public ActionResult TopNewArticle()
        {
            var article = _articleService.GetAll().OrderByDescending(t => t.ArticleId).Where(t => t.IsActive == true).Take(20).ToList();
            var articleView = AutoMapperConfig.mapper.Map<List<Article>, List<ArticleViewModel>>(article);
            return View(articleView);
        }
    }
}