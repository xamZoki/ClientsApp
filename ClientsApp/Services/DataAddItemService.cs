using ClientsApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Controls;

namespace ClientsApp.Services
{
    public class DataAddItemService : IDataAddItemService
    {
        HttpClient client;

        public DataAddItemService()
        {
            client = new HttpClient(); 
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["url"]);
        }
        public bool AddItem(Models.Client clientDto)
        {
            client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

            Models.Client c = new Models.Client(0,clientDto.Name, clientDto.Birthday, clientDto.HomeAddress, clientDto.WeekendAddress);
          
            var json = JsonConvert.SerializeObject(c);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync("api/client/AddClient", content).Result;
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode) return true; else return false;
        }
    }
}
