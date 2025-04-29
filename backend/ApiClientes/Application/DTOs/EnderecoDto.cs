using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class EnderecoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tipo do endereço é obrigatório.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP em formato inválido.")]
        public string Cep { get; set; } = string.Empty;

        public string Logradouro { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
    }
}
