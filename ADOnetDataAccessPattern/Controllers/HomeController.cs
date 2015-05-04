using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADO.Data.Access.Conctretes;
using ADO.Data.Access.Domain;

namespace ADOnetDataAccessPattern.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult ProductAdmin()
        {
            var productRepository = new ProductRepository();

            return View(productRepository.GetAll());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProductAdmin(Product product, string insert, string update, string delete)
        {
            var productRepository = new ProductRepository();

            var operation = (insert != null ? insert : update != null ? update : delete);

            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "Insert":
                        productRepository.Add(product);
                        break;
                    case "Update":
                        productRepository.Update(product);
                        break;
                    case "Delete":
                        productRepository.Delete(product.ProductId);
                        break;
                    default:
                        return View(productRepository.GetAll());
                }

            }
            return RedirectToAction("ProductAdmin");
        }
    }
}