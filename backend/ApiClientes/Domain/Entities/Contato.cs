namespace Domain.Entities
{
    public class Contato
    {
        public int Id { get; private set; }
        public string Tipo { get; private set; }
        public int Ddd { get; private set; }
        public string Telefone { get; private set; }

        public Contato(int id, string tipo, int ddd, string telefone)
        {
            Id = id;
            Tipo = tipo;
            Ddd = ddd;
            Telefone = telefone;
        }
    }
}
