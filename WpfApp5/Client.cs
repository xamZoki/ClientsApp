using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class Client
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeAddress { get; set; }
    }
}
