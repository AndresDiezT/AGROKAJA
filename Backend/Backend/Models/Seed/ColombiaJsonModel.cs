using System.Text.Json.Serialization;

namespace Backend.Models.Seed
{
    public class ColombiaJsonModel
    {
        [JsonPropertyName("departamento")]
        public string Departamento { get; set; }

        [JsonPropertyName("ciudades")]
        public List<string> Ciudades { get; set; }
    }
}
