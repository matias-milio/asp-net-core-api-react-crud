using DTO;
using System.Collections.Generic;

namespace ProductsAPI.Models
{
    public class ProductOverviewResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public double Price { get; set; }        
        public int Stock { get; set; }        
    }
}
