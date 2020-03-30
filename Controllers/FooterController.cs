
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RegistryForFinalProject.Controllers
{
    public class FooterController : Controller
    {
        public IActionResult Privacy()
        {
            return View("FooterPrivacy");
        }

        public IActionResult Terms()
        {
            return View("FooterTerms");
        }

        public IActionResult Contact()
        {
            return View("FooterContact");
        }
    }
}
