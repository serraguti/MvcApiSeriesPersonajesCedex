using Microsoft.AspNetCore.Mvc;
using MvcApiSeriesPersonajesCedex.Services;
using NugetSeriesPersonajes;

namespace MvcApiSeriesPersonajesCedex.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiSeries service;

        public PersonajesController(ServiceApiSeries service)
        {
            this.service = service;
        }

        public async Task< IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> PersonajesSerie(int idserie)
        {
            List<Personaje> personajes = 
                await this.service.GetPersonajesSerieAsync(idserie);
            return View(personajes);
        }

        public IActionResult CreatePersonaje()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonaje
            (Personaje personaje)
        {
            await this.service.CreatePersonajesAsync(personaje);
            return RedirectToAction
                ("PersonajesSerie", new { idserie = personaje.IdPersonaje });
        }

        public async Task<IActionResult> Details(int idpersonaje)
        {
            Personaje p = await this.service.FindPersonajeAsync(idpersonaje);
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonajeSerie
            (int idpersonaje, int idserie)
        {
            await this.service.UpdatePersonajeSerie(idpersonaje, idserie);
            return RedirectToAction
                ("PersonajesSerie", new { idserie = idserie });
        }
    }
}
