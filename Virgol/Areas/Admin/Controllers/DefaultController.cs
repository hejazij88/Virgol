using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Virgol.Service;
using Virgol_Model.Context;

namespace Virgol.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
		private VirgolContext db = new VirgolContext();
		CategoryService _CategoryService;
		ArticleService _ArticleService;
		UserService _UserService;

		public DefaultController()
		{
			_CategoryService = new CategoryService(db);
			_ArticleService = new ArticleService(db);
			_UserService = new UserService(db);
		}
		public ActionResult Index()
        {
			ViewBag.Category=_CategoryService.GetAll().Count();
			ViewBag.Article = _ArticleService.GetAll().Count();
			ViewBag.User=_UserService.GetAll().Count();
			ViewBag.UserActive = _UserService.GetAll().Where(t=>t.IsActive==true).Count();
			ViewBag.ArticleActive = _ArticleService.GetAll().Where(t=>t.IsActive==true).Count();





			return View();
        }
    }
}