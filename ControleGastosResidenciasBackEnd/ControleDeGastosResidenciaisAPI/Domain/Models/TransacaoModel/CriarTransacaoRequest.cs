using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace ControleGastosResidenciaisAPI.Domain.Models.TransacaoModel
{
    public class CriarTransacaoRequest
    {
        //Request em DTO para abstrair e simplificar Request dos dados
        public Guid PessoaIdentificador { get; set; }
        public string Descricao { get; set; }
        public decimal Valor {  get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoTransacao Tipo { get; set; }
    }
}
