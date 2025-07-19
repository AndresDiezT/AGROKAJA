namespace Backend.DTOs.ProductDTOs
{
    public class CreateProductDto
    {
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public int IdSubCategory { get; set; }
    }
}
