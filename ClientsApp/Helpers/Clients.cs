using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace ClientsApp.Helpers
{
    [XmlRoot("Clients")]
    public class Clients
    {
        [XmlElement("Client")]
        public List<Client> ClientList { get; set; }
    }

    public class Client
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlArray("Addresses")]
        [XmlArrayItem("Address")]
        public List<Address> Addresses { get; set; }

        [XmlElement("BirthDate")]
        public string BirthDate { get; set; }
    }

    public class Address
    {
        [XmlAttribute("Type")]
        public int Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
