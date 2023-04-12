using ClientsApp.Helpers;
using ClientsApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ClientsApp.Services
{
    public class DataImportService : IDataImportService
    {
        public List<Models.Client> ImportFromXml(string xml)
        {
            string xmlPath = File.ReadAllText(xml);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Clients));
                using (StringReader reader = new StringReader(xmlPath))
                {
                    Clients clients = (Clients)serializer.Deserialize(reader);

                    List<Models.Client> lc = new List<Models.Client>();
                    foreach (Helpers.Client client in clients.ClientList)
                    {
                        int id = client.ID;
                        string name = client.Name;
                        string homeAdress = null;
                        string weekendAdress = null;
                        foreach (Helpers.Address address in client.Addresses)
                        {
                            if (address.Type == 1)
                            {
                                homeAdress = address.Value;
                            }
                            else
                            {
                                weekendAdress = address.Value;
                            }
                        }
                        DateTime birthDate = Convert.ToDateTime(client.BirthDate);

                        Models.Client c = new Models.Client(id = 0, name, birthDate, homeAdress, weekendAdress);
                        lc.Add(c);
                    }
                    return lc;
                }
            }
            catch (Exception e)
            {
                return new List<Models.Client>();
               
            }
            
            
        }
    }
}
