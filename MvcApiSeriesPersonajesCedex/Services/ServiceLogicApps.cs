using MvcApiSeriesPersonajesCedex.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiSeriesPersonajesCedex.Services
{
    public class ServiceLogicApps
    {
        public async Task SendMailAsync(EmailModel model)
        {
            string urlMail = "https://prod-89.westeurope.logic.azure.com:443/workflows/50bebd5bcdfa4db0a4263eb8d6c0b3ed/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=C9RF7Ef5FLW37jtzOULpDM8T84uksNKK4ICNHyQ1cE4";
            using (HttpClient client = new HttpClient())
            {
                MediaTypeWithQualityHeaderValue header =
                    new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                string json = JsonConvert.SerializeObject(model);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync(urlMail, content);
            }
        }
    }
}
