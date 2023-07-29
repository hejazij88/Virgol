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
    public class CategoryController : Controller
    {
        VirgolContext db=new VirgolContext();
        CategoryService _categoryService;
        ArticleService _articleService;

        public CategoryController()
        {
            _categoryService=new CategoryService(db);
            _articleService=new ArticleService(db);
        }
        public ActionResult Index()
        {
            var categorys=_categoryService.GetAll().ToList();
            var categoryView = AutoMapperConfig.mapper.Map<List<Ctegory>, List<CategoryViewModel>>(categorys);
            return View(categoryView);
        }


        public ActionResult Filter(int? Id)
        {
            if(Id==null)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article=_articleService.GetAll().Where(a=>a.CtegoryId==Id).ToList();
            if(article==null)
            {
                return HttpNotFound();
            }
            var articleView=AutoMapperConfig.mapper.Map<List<Article>, List<ArticleViewModel>>(article);
            return View(articleView);
        }
    }
}