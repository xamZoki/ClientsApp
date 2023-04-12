using ClientsApp.Models;
using System.Collections.Generic;


namespace ClientsApp.Interfaces
{
    public interface IDataExportService
    {
        bool ExportToJson(List<Client> clients);
    }
}
