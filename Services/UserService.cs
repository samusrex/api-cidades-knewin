using api.Helpers;
using api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    public interface IUserService
    {
        AuthResponse Authenticate(AuthRequest model);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        int Insert(Users usuario);
    }

    public class UserService : IUserService
    {

        private readonly ApiDbContext _context;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IOptions<ApiDbContext> apiDbContext)
        {
            _context = apiDbContext.Value;
            _appSettings = appSettings.Value;
        }

        public AuthResponse Authenticate(AuthRequest model)
        {
            var user = this.GetAll().SingleOrDefault(x => x.Usuario == model.Usuario && x.Senha == model.Senha);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthResponse(user, token);
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Usuarios;
        }

        public Users GetById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public int Insert(Users usuario)
        {
            _context.Usuarios.Add(usuario);
            return _context.SaveChanges();
        }

        private string generateJwtToken(Users user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}