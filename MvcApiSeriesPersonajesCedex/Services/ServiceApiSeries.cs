using Newtonsoft.Json;
using NugetSeriesPersonajes;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace MvcApiSeriesPersonajesCedex.Services
{
    public class ServiceApiSeries
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiSeries(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiSeriesPersonajes");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                //var queryString = HttpUtility.ParseQueryString(string.Empty);
                //request += "?" + queryString;
                string url = this.UrlApi + request;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //client.DefaultRequestHeaders.CacheControl =
                //    CacheControlHeaderValue.Parse("no-cache");
                //DEBEMOS AÑADIR LA CLAVE DE SUBSCRIPCION
                //string subscriptionKey = "3321c68de537414584558200196c4fa1";
                //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                HttpResponseMessage response =
                    await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            string request = "api/personajes/" + idpersonaje;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(int idserie)
        {
            string request = "api/Personajes/PersonajesSerie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task CreatePersonajesAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string url = this.UrlApi + request;
                HttpResponseMessage response =
                    await client.PostAsync(url, content);
            }
        }

        public async Task UpdatePersonajeSerie(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/UpdatePersonaje/" 
                    + idpersonaje + "/" + idserie;
                string url = this.UrlApi + request;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PutAsync(url, null);
            }
        }
    }
}
