namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get;  set; }
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public string Cpf { get;  set; }
        public string Rg { get;  set; }
        public List<Contato> Contatos { get; set; } = new();
        public List<Endereco> Enderecos { get; set; } = new();

        public Cliente(int id, string nome, string email, string cpf, string rg)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Rg = rg;
            Contatos = new List<Contato>();
            Enderecos = new List<Endereco>();
        }

        public void AdicionarContato(Contato contato)
        {
            Contatos.Add(contato);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            Enderecos.Add(endereco);
        }
    }
}
