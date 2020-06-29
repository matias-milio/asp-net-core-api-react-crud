using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public double Price { get; set; }
        public int ProductBrandId { get; set; }
        public int Stock { get; set; }

       
    }
}
