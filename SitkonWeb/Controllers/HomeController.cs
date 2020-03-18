using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitkonWeb.Models;
using System.Net.Http;

namespace SitkonWeb.Controllers
{
    public class HomeController : Controller
    {
        

        

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Validate(CustomerModel model)
        {
            var client = new HttpClient();
            CustomerModel CustomerVM = new CustomerModel();
            if (model != null)
            {
                if (model.ValidaIdentidad == false)
                {
                    return RedirectToAction("Score", "Home", model);
                }
                string URLApi = System.Web.Configuration.WebConfigurationManager.AppSettings["URL_PROD"];
                var response = client.GetAsync(URLApi + "Customer/GetByCustomer/" + model.Cuit + "/" + model.Sexo).Result;
                CustomerVM = response.Content.ReadAsAsync<CustomerModel>().Result;

                
            }
            return View(CustomerVM);
        }
        public ActionResult Score(CustomerModel model)
        {
            var client = new HttpClient();
            CustomerModel CustomerVM = new CustomerModel();
            if (model != null)
            {
                string URLApi = System.Web.Configuration.WebConfigurationManager.AppSettings["URL_PROD"];
                model.Sexo = (string.IsNullOrEmpty(model.Sexo) ? "empresa" : model.Sexo);
                var response = client.GetAsync(URLApi + "Customer/GetByCustomer/" + model.Cuit + "/" + model.Sexo).Result;
                CustomerVM = response.Content.ReadAsAsync<CustomerModel>().Result;
            }
            return View(CustomerVM);
        }
        

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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
