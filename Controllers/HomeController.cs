using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vega.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return Content("asdf");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

