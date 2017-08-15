using PayrollAppSample.DDD.Domain.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PayrollAppSample.DDD.MVC.Controllers
{
    public class HumanResourceController : Controller {

        private readonly IHumanResourceService _hrService;
        public HumanResourceController(IHumanResourceService hrService) {
            _hrService = hrService;
        }

        // GET: HumanResource
        public ActionResult Index()
        {
            var modelList = _hrService.GetListOfEmployees();
            return View(modelList);
        }

        // GET: HumanResource/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HumanResource/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HumanResource/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HumanResource/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HumanResource/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: HumanResource/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HumanResource/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
