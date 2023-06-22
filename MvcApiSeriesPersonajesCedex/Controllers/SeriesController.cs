using Microsoft.AspNetCore.Mvc;
using MvcApiSeriesPersonajesCedex.Services;
using NugetSeriesPersonajes;

namespace MvcApiSeriesPersonajesCedex.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceApiSeries service;

        public SeriesController(ServiceApiSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }
    }
}
