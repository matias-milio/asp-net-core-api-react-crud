namespace DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }        
        public int Stock { get; set; }
        public int ProductCategoryId { get; set; }  
        public int ProductBrandId { get; set; }       
    }
}
