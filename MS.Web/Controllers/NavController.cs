using MS.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository productRepository;

        public NavController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Nav
        public ActionResult Menu()
        {
            var categories = this.productRepository.Categories;

            return PartialView(categories);
        }
    }
}