using Fecovita.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fecovita.Core.Entities
{
    public class Product : BaseEntity
    {
        public int Code { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Liters { get; set; }
    }
}
