namespace Backend.DTOs.AddressDTOs
{
    public class AddressFilterDto
    {

        // Paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Ordenamiento
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; } = false;

        // Columnas seleccionadas
        public List<string>? SelectFields { get; set; }
    }
}
