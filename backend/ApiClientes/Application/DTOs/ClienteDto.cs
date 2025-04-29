using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "CPF em formato inválido.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "RG é obrigatório.")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\-\d{1}$", ErrorMessage = "RG em formato inválido.")]
        public string Rg { get; set; } = string.Empty;

        public List<ContatoDto> Contatos { get; set; } = new();
        public List<EnderecoDto> Enderecos { get; set; } = new();
    }
}
