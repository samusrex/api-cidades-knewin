using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class AuthRequest
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}