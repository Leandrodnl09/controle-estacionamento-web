using System.Text.Json.Serialization;

namespace controle.estacionamento.web.Models
{
    public class ControlePermanenciaCarrosModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("placa")]
        public string Placa { get; set; }
        [JsonPropertyName("idModelo")]
        public int IdModelo { get; set; }
        [JsonPropertyName("dataHoraEntrada")]
        public DateTime DataHoraEntrada { get; set; }
        [JsonPropertyName("dataHoraSaida")]
        public DateTime? DataHoraSaida { get; set; }
    }
}
