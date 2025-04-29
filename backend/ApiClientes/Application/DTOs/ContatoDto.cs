namespace Application.DTOs
{
    public class ContatoDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int Ddd { get; set; }
        public string Telefone { get; set; } = string.Empty;
    }
}
