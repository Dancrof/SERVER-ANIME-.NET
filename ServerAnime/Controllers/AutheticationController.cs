using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServerAnime.Model;
using ServerAnime.Model.ModelDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAnime.Controllers
{
    [Route("api/autenticacion")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        private readonly string? secretkey;

        public AutheticationController(IConfiguration config) => secretkey = config.GetSection("settings").GetSection("secretkey").ToString();

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto modelo)
        {
            try
            {
                Usuario usuario = this.MAppingDtoWithModel(modelo);
                if (usuario.Username == "admin" && usuario.Password == "1234")
                {
                    byte[] keyBytes = Encoding.ASCII.GetBytes(secretkey);
                    ClaimsIdentity claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, modelo.Username));

                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    SecurityToken tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string createToken = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status202Accepted, new { Token_access = createToken });
                }
                return StatusCode(StatusCodes.Status401Unauthorized, new { msg = "Username o Passwor Incorrecto" });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, e);
            }

        }

        private Usuario MAppingDtoWithModel(UsuarioDto modelo)
        {
            return new Usuario() { Username = modelo.Username, Password = modelo.Password };
        }
    }
}
