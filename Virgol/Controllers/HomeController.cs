using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Virgol.App_Start;
using Virgol.Service;
using Virgol.Views.ViewModel;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Controllers
{
    public class HomeController : Controller
    {
		VirgolContext db=new VirgolContext();

		private SliderService _sliderService;
		private ArticleService _articleService;
		private CategoryService _categoryService;


		public HomeController()
        {
            _sliderService = new SliderService(db);
			_articleService=new ArticleService(db);
			_categoryService = new CategoryService(db);

		}
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Slider()
		{
			var slider = _sliderService.GetAll().ToList();
			var sliderView = AutoMapperConfig.mapper.Map<List<Slider>, List<SliderViewModel>>(slider);
			return PartialView(sliderView);
		}

		public ActionResult NewArticle()
		{
			var article = _articleService.GetAll().OrderByDescending(t=>t.ArticleId).Where(t=>t.IsActive==true).Take(6).ToList();
			var articleView = AutoMapperConfig.mapper.Map<List<Article>, List<ArticleViewModel>>(article);
			return PartialView(articleView);
		}
		public ActionResult AboutUs()
		{
			return View();
		}
	}
}