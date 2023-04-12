using ClientsApp.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClientsApp.Services
{
    public class DataExportService : IDataExportService
    {
        public bool ExportToJson(List<Models.Client> clients)
        {
            try
            {
                string json = JsonSerializer.Serialize(clients);
                string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                File.WriteAllText(strExeFilePath + "../../../../../clientj.json", json);
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }

        }
    }
}
