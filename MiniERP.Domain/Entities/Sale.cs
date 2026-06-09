using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public ICollection<SaleDetail> Details { get; set; }
            = new List<SaleDetail>();
    }
}
