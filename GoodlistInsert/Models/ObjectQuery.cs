using System.ComponentModel.DataAnnotations;

namespace GoodlistInsert.Models
{
    public class ObjectQueryInserir
    {
        public long entidadeId { get; set; }
        public int UserId { get; set; }
        public string observacao { get; set; } = "";
        public int Ativar { get; set; } = 0;
        public string Cpf { get; set; }
        public string Tel { get; set; }
        public  string ddd { get; set; }
        public string Endereço { get; set; }
        [EmailAddress]
        public string Email { get; set; }
       
    }

    public class vwObjectQuery
    {

        public bool isBadlist { get; set; }
        public bool isGoodlist { get; set; }
        public bool isCpf { get; set; }
        public bool isEmail { get; set; }
        public bool isEndereco { get; set; }
        public bool isTelefone { get; set; }
        public int usuarioId { get; set; }
        public int entidadeid { get; set; }
        public int Ativar { get; set; } = 0;
        public string Observacao { get; set; } = "";
        public string Aba { get; set; }

    }
}
