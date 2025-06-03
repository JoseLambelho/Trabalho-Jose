using Microsoft.AspNetCore.Mvc;
using Projeto.Modelo;
using System.Threading.Tasks;
using System.Collections.Generic;
using Projeto.Modelo;
using Projeto.Servicos;


[ApiController]
[Route("api/[controller]")]


public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Utilizador user)
    {
        if (_context.Utilizadores.Any(u => u.Email == user.Email))
            return BadRequest("Email já registado.");

        user.Tipo = "Utilizador"; // padrão
        _context.Utilizadores.Add(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var user = _context.Utilizadores
            .FirstOrDefault(u => u.Email == req.Email && u.Password == req.Password);

        if (user == null)
            return Unauthorized("Credenciais inválidas.");

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }
}