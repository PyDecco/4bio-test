namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public string Rg { get; private set; }
        public List<Contato> Contatos { get; private set; }
        public List<Endereco> Enderecos { get; private set; }

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
