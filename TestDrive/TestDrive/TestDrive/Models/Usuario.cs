using Newtonsoft.Json;

namespace TestDrive.Models
{
    //[JsonObject("usuario")]
    public class Usuario
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("dataNascimento")]
        public string DataNascimento { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }
    }

    public class ResultadoLogin
    {
        [JsonProperty("usuario")]
        public Usuario Usuario { get; set; }
    }
}
