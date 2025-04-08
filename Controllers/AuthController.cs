using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using E_Commerce_API.Models.Entities;
using E_Commerce_API.Services;

namespace E_Commerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar([FromBody] UsuarioRegistro model)
        {
            var usuario = new Usuario
            {
                UserName = model.Email, // UserName é obrigatório
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                DataNascimento = model.DataNascimento
            };

            var resultado = await _userManager.CreateAsync(usuario, model.Senha);

            if (!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors);
            }

            return Ok(new
            {
                Token = _tokenService.GerarToken(usuario),
                UsuarioId = usuario.Id,
                Nome = usuario.NomeCompleto
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioLogin model)
        {
            var usuario = await _userManager.FindByEmailAsync(model.Email);

            if (usuario == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(
                usuario, model.Senha, false);

            if (!resultado.Succeeded)
            {
                return Unauthorized("Credenciais inválidas");
            }

            return Ok(new
            {
                Token = _tokenService.GerarToken(usuario),
                UsuarioId = usuario.Id,
                Nome = usuario.NomeCompleto
            });
        }
    }

    // DTOs para autenticação
    public class UsuarioRegistro
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public class UsuarioLogin
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}