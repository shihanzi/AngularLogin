using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularLogin.Models
{
    public class Lot
    {
        [Key]
        public int LotId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Perches { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public bool Status { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
    }
}
