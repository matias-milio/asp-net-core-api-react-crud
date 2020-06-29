using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductDTO> Product { get; set; }
    }
}
