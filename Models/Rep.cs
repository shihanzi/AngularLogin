using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularLogin.Models
{
    public class Rep
    {
        [Key]
        public int RepId { get; set; }
        public string RepPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public IList<Lot> Lots { get; set; }
    }
}
