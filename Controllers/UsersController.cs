using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("auth")]
        public IActionResult Authenticate(AuthRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Usuario ou senha incorreta" });

            return Ok(response);
        }

        [HttpPost("insert")]
        public IActionResult NovoUsuario(Users usuario)
        {
            var response = _userService.Insert(usuario);

            if (response == 0)
                return BadRequest(new { message = "Usuario ou senha incorreta" });

            return Ok(new { message = "Usuario criado com sucesso" });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
