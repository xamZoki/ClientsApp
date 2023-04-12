using ClientsApp.Models;
using System.Collections.Generic;


namespace ClientsApp.Interfaces
{
    public interface IDataImportService
    {
        List<Client> ImportFromXml(string xml);
    }
}
