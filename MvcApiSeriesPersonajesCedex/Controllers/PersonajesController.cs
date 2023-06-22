using Microsoft.AspNetCore.Mvc;
using MvcApiSeriesPersonajesCedex.Helpers;
using MvcApiSeriesPersonajesCedex.Services;
using NugetSeriesPersonajes;

namespace MvcApiSeriesPersonajesCedex.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiSeries service;
        private ServiceAzureStorage serviceAzureStorage;
        //private HelperPathProvider helper;

        public PersonajesController
            (ServiceApiSeries service, ServiceAzureStorage serviceAzureStorage)
        {
            this.serviceAzureStorage = serviceAzureStorage;
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
            (Personaje personaje, IFormFile file)
        {
            //MODIFICAMOS EL NOMBRE DE LA IMAGEN DEL PERSONAJE
            personaje.Imagen = file.FileName;
            //string path = this.helper.MapPath(file.FileName, Folders.Images);
            using (Stream stream = file.OpenReadStream())
            {
                await this.serviceAzureStorage.UploadBlobAsync
                    (file.FileName, stream);
            }
            await this.service.CreatePersonajesAsync(personaje);
            return RedirectToAction
                ("PersonajesSerie", new { idserie = personaje.IdSerie });
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
