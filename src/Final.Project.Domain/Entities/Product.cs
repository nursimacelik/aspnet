using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public int UsingStatusId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsOfferable { get; set; } = false;
        public bool IsSold { get; set; } = false;

    }
}
