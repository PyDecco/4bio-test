namespace Domain.Entities
{
    public class Endereco
    {
        public int Id { get; private set; }
        public string Tipo { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public int Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Referencia { get; private set; }

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
