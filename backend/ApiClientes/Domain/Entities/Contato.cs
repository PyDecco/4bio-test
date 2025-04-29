using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    public class Contato
    {
        public int Id { get; set; }
        [EnumDataType(typeof(TipoContato), ErrorMessage = "Tipo de contato inválido.")]
        public string Tipo { get; set; } = string.Empty;
        public int Ddd { get; set; }
        public string Telefone { get; set; }

        public Contato(int id, string tipo, int ddd, string telefone)
        {
            Id = id;
            Tipo = tipo;
            Ddd = ddd;
            Telefone = telefone;
        }
    }
}
