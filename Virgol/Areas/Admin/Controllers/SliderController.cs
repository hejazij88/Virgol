using System;
using System.Collections.Generic;
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
    public class SliderController : Controller
    {
        VirgolContext db=new VirgolContext();
        private SliderService _sliderService;

        public SliderController()
        {
            _sliderService = new SliderService(db);
        }
        // GET: Admin/Slider
        public ActionResult Index()
        {
            var slider=_sliderService.GetAll().ToList();
            var viewSlider=AutoMapperConfig.mapper.Map<List<Slider>,List<SliderViewModel>>(slider);
            return View(viewSlider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slider/Create
        [HttpPost]
        public ActionResult Create([Bind(Include ="Id,Title")] SliderViewModel sliderViewModel,HttpPostedFileBase imageUpload)
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
					if (imageUpload.ContentLength > 300000000)
					{
						ModelState.AddModelError(" imageUpload ", "سایز تصویر شما نباید بیشتر از 300 کیلوبایت باشد!");
						return View();
					}

					newImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
					imageUpload.SaveAs(Server.MapPath("~/Images/SlideShow/") + newImageName);
				}

				sliderViewModel.ImageName = newImageName;
				var slider = AutoMapperConfig.mapper.Map<SliderViewModel, Slider>(sliderViewModel);
				_sliderService.add(slider);
				_sliderService.save();
				return RedirectToAction("Index");
			}

			return View(sliderViewModel);
		}

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Slider slider = _sliderService.GetEntitiy(id.Value);
			if (slider == null)
			{
				return HttpNotFound();
			}
			var sliderViewModel = AutoMapperConfig.mapper.Map<Slider, SliderViewModel>(slider);

			return View(sliderViewModel);
		}

        // POST: Admin/Slider/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Title,ImageName")] SliderViewModel sliderView, HttpPostedFileBase imageUpload)
		{
			if (ModelState.IsValid)
			{
				if (imageUpload != null)
				{
					if (sliderView.ImageName != "noPhoto.png")
					{
						System.IO.File.Delete(Server.MapPath("~/Images/SlideShow/") + sliderView.ImageName);
					}
					else
					{
						System.IO.File.Delete(Server.MapPath("~/Images/SlideShow/") + sliderView.ImageName);
						sliderView.ImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
					}
					imageUpload.SaveAs(Server.MapPath("~/Images/SlideShow/" + sliderView.ImageName));
				}
				var slide = AutoMapperConfig.mapper.Map<SliderViewModel, Slider>(sliderView);
				_sliderService.Update(slide);
				_sliderService.save();
				return RedirectToAction("Index");
			}
			return View(sliderView);
		}

        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Slider slider = _sliderService.GetEntitiy(id.Value);
			if (slider == null)
			{
				return HttpNotFound();
			}
			var sliderView = AutoMapperConfig.mapper.Map<Slider, SliderViewModel>(slider);
			ViewBag.TitleSlider = sliderView.Title;
			return View(sliderView);
		}

        // POST: Admin/Slider/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
			Slider slider = _sliderService.GetEntitiy(id);
			if (slider != null)
			{
				_sliderService.remove(slider);
				_sliderService.save();
				return RedirectToAction("Index");
			}
			return Content("کاربر یافت نشد!");
		}

		protected override void Dispose(bool disposing)
		{
			_sliderService.Dispose();
		}

	}
}
