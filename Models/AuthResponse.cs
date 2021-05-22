namespace api.Models
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Token { get; set; }


        public AuthResponse(Users usuario, string token)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Usuario = usuario.Usuario;
            Token = token;
        }

    }

}