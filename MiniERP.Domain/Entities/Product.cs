using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool Active { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
