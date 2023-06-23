using Microsoft.AspNetCore.Mvc;
using MvcApiSeriesPersonajesCedex.Models;
using MvcApiSeriesPersonajesCedex.Services;
using System.Diagnostics;

namespace MvcApiSeriesPersonajesCedex.Controllers
{
    public class HomeController : Controller
    {
        private ServiceLogicApps service;

        public HomeController(ServiceLogicApps service)
        {
            this.service = service;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(EmailModel model)
        {
            await this.service.SendMailAsync(model);
            ViewData["MENSAJE"] = "Su email ha sido enviado correctamente";
            return View();
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}