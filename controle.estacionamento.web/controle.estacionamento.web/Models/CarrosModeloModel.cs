using System.Text.Json.Serialization;

namespace controle.estacionamento.web.Models
{
    public class CarrosModeloModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("marca")]
        public string Marca { get; set; }

        [JsonPropertyName("modelo")]
        public string Modelo { get; set; }

        [JsonPropertyName("ano")]
        public int Ano { get; set;}
    }
}
