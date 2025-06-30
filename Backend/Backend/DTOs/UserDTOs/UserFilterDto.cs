namespace Backend.DTOs.UserDTOs
{
    public class UserFilterDto
    {
        // Filtros
        public string? Document { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public int? IdRole { get; set; }
        public int? IdTypeDocument { get; set; }

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
