using ClientsApp.Interfaces;
using ClientsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientsApp.Services
{
    public class ClientRepoService : IClientRepoService
    {
        HttpClient client;
        
        public ClientRepoService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["url"]);
        }

        public async Task<List<Client>> GetAll()
        {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/client/GetAllClients").Result;
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();

                List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(result);
                return clients;
        }
    }
}
