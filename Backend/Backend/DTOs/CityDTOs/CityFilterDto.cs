namespace Backend.DTOs.CityDTOs
{
    public class CityFilterDto
    {
        public string? NameCity { get; set; }
        public int? IdCountry { get; set; }
        public int? IdDepartment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        // Paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Ordenamiento
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; } = false;

        // Campos seleccionados
        public List<string>? SelectFields { get; set; }
    }
}
