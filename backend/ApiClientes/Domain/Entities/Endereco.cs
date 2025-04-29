using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class Endereco
    {
        public int Id { get;  set; }
        [Required(ErrorMessage = "Tipo de endereço é obrigatório.")]
        [EnumDataType(typeof(TipoEndereco), ErrorMessage = "Tipo de endereço inválido.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP em formato inválido.")]
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get;  set; }
        public int Numero { get;  set; }
        public string Bairro { get;  set; }
        public string Complemento { get;  set; }
        public string Cidade { get;  set; }
        public string Estado { get;  set; }
        public string Referencia { get;  set; }

        public Endereco(int id, string tipo, string cep, string logradouro, int numero,
                        string bairro, string complemento, string cidade, string estado, string referencia)
        {
            Id = id;
            Tipo = tipo;
            Cep = cep;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            Referencia = referencia;
        }
    }
}
