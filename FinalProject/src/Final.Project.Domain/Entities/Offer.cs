using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Domain.Entities
{
    public class Offer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductOwnerId { get; set; }
        public int UserId { get; set; }
        public decimal PriceOffered { get; set; }
        public string Status { get; set; } = "On hold";
    }
}
