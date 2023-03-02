using System;
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
        public DateTime ReceivedDate { get; set; }
        public DateTime SoldDate { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public Rep Rep { get; set; }
        public int RepId { get; set; }
        public Customer Customer  { get; set; }
        public int CustomerId { get; set; }

    }
}
