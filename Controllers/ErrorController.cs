using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFoundError()
        {

            return View("NotFoundError");
        }
    }
}
