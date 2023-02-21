using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularLogin.Models
{
    public class Rep
    {
        public int RepId { get; set; }
        public string RepPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public IList<Lot> Lots { get; set; }
    }
}
