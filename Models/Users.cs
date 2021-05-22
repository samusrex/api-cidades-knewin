using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Users
    {

        [Key(), Column(Order = 0)]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

    }

}