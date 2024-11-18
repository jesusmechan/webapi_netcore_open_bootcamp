using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using System.Runtime.CompilerServices;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApi_OpenBootcamp.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InicioSesionController : ControllerBase
    {

        CtrUsuario ctr = null;
        ClaseResultado<Sesion> _resultadoSesion = null;
        ClaseResultado<SesionXUsuario> _resultadoSesionXUsu = null;
        private readonly string? secretKey;
        public InicioSesionController(IConfiguration config)
        {
            ctr = new CtrUsuario();
            _resultadoSesion = new ClaseResultado<Sesion>();
            _resultadoSesionXUsu = new ClaseResultado<SesionXUsuario>();
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
        }
        [HttpPost]
        public ClaseResultado<DtoUsuario> VerificarAcceso(Login login)
        {
            DtoUsuario data = new DtoUsuario();
            ClaseResultado<Sesion> resultadoSesion = new ClaseResultado<Sesion>();
            ClaseResultado<DtoUsuario> resultado = new ClaseResultado<DtoUsuario>();
            //data = ctr.InicioSesion(login);
            resultado = ctr.InicioSesion(login);

            //if (data.IDUSUARIO != 0)
            //{

            //    data.CTOKEN = GenerarTokenJWT(data.NUMERODOCUMENTO);

            //    ////resultadoSesion = RegistrarSesion(data.IDUSUARIO, "I");
            //    //var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            //    //var claims = new ClaimsIdentity();
            //    //claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, data.NUMERODOCUMENTO));
            //    //var tokenDescriptor = new SecurityTokenDescriptor
            //    //{
            //    //    Subject = claims,
            //    //    //Expires = DateTime.UtcNow.AddHours(1),
            //    //    Expires = DateTime.UtcNow.AddDays(1),
            //    //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
            //    //    SecurityAlgorithms.HmacSha256)
            //    //};

            //    //var tokenHandler = new JwtSecurityTokenHandler();
            //    //var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            //    //string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            //    //data.CTOKEN = tokenCreado;
            //    //data.IDSESION = resultadoSesion.UltimoId;

            //}
            return resultado;
        }

        [HttpPost]
        public DtoUsuario GenerarTokenJWT(DtoUsuario parametro)
        {
            DtoUsuario resultado = new DtoUsuario();
            resultado = parametro;



            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, parametro.NUMERODOCUMENTO));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            resultado.CTOKEN = tokenHandler.WriteToken(tokenConfig);
            return resultado;
        }




        [HttpPost]
        public ClaseResultado<Sesion> RegistrarSesion(int usuario, string accion)
        {
            Sesion entidad = new Sesion();
            try
            {
                entidad.IDUSUARIO = usuario;
                entidad.ACCION = accion;
                _resultadoSesion = ctr.Sesion_MNT(entidad);
                return _resultadoSesion;
            }
            catch (Exception ex)
            {
                _resultadoSesion.HuboError = true;
                _resultadoSesion.UltimoId = 0;
                _resultadoSesion.Mensaje = ex.Message.ToString();

            }
            return _resultadoSesion;
        }

        [HttpPost]
        public ClaseResultado<Sesion> CerrarSesion(Sesion sesion)
        {
            Sesion entidad = new Sesion();
            try
            {
                _resultadoSesion = ctr.Sesion_MNT(sesion);
                return _resultadoSesion;
            }
            catch (Exception ex)
            {
                _resultadoSesion.HuboError = true;
                _resultadoSesion.UltimoId = 0;
                _resultadoSesion.Mensaje = ex.Message.ToString();

            }
            return _resultadoSesion;
        }

        [HttpPost]
        public ClaseResultado<Sesion> Validar_Sesion(Sesion entidad)
        {
            try
            {
                _resultadoSesion = ctr.Validar_Sesion(entidad);
                return _resultadoSesion;
            }
            catch (Exception ex)
            {
                _resultadoSesion.HuboError = true;
                _resultadoSesion.UltimoId = 0;
                _resultadoSesion.Mensaje = ex.Message.ToString();
            }
            return _resultadoSesion;
        }

        [HttpPost]
        public ClaseResultado<SesionXUsuario> Listar_Usuarios_Logueados()
        {
            try
            {
                _resultadoSesionXUsu = ctr.Listar_Usuarios_Logueados();
                return _resultadoSesionXUsu;
            }
            catch (Exception ex)
            {
                _resultadoSesion.HuboError = true;
                _resultadoSesion.UltimoId = 0;
                _resultadoSesion.Mensaje = ex.Message.ToString();
            }
            return _resultadoSesionXUsu;
        }
    }
}
