using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FronEndProyecto.modelos
{
    class Class1
    {
        private static readonly HttpClient client = new HttpClient();
        string url = "http://apibrandon.eastus.cloudapp.azure.com/api/secciones.php";

        public async void getSecciones()
        {
            try {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseBody);

            }
            catch (HttpRequestException e){ 
            }
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        public int num { get; set; }
        public List<Seccione> secciones { get; set; }
    }

    public class Seccione
    {
        public string nombre { get; set; }
        public List<Subtema> subtemas { get; set; }
    }

    public class Subtema
    {
        public string subtema { get; set; }
        public string contenido { get; set; }
    }


}
