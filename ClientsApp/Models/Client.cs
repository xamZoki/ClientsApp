using System;

namespace ClientsApp.Models
{
    public class Client 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeAddress { get; set; }
        public string WeekendAddress { get; set; }
        public Client(int id, string name, DateTime birthday, string homeAddress, string weekendAddress)
        {
            ID = id;
            Name = name;
            Birthday = birthday;
            HomeAddress = homeAddress;
            WeekendAddress = weekendAddress;
        }

    }
}
