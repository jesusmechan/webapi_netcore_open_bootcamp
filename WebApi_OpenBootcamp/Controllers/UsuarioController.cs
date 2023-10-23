using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase 
    {
        CtrUsuario ctr = null;
        ClaseResultado<DtoUsuario> resultado = null;
        public UsuarioController()
        {
            ctr = new CtrUsuario();
            resultado = new ClaseResultado<DtoUsuario>();
        }

        [HttpGet]
        public List<DtoUsuario> Listar_Usuario()
        {
            List<DtoUsuario> _lista = new List<DtoUsuario>();
            _lista = ctr.Usuario_Listar();
            return _lista;
        }


        [HttpPost]
        public ClaseResultado<DtoUsuario> Insertar_Actualizar_Usuario(DtoUsuario _entidad)
        {
            try
            {
                _entidad.PASSWORD = Encrypt.Encrypt.GetSHA256(_entidad.PASSWORD);
                resultado = ctr.Usuario_Insertar_Actualizar(_entidad);
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.UltimoId = 0;
                resultado.Mensaje = ex.Message.ToString();

            }
            return resultado;
        }

        [HttpPost]
        public ClaseResultado<DtoUsuario> Usuario_Activar_Inactivar(DtoUsuario _entidad)
        {
            resultado = ctr.Usuario_Activar_Inactivar(_entidad);
            return resultado;
        }


        [HttpGet]
        public async Task<respuestaDNI> ConsultaDatosReniec(string numeroDocumento)
        {
            respuestaDNI entidad = new respuestaDNI();
             return entidad  = await ctr.ConsultaDatosReniec(numeroDocumento);
        }

        //[HttpPost]
        //////public DtoUsuario InicioSesion(DtoUsuario entidad)
        //public DtoUsuario InicioSesion (string usuario, string password) 
        //{
        //    DtoUsuario data = new DtoUsuario();
        //    DtoUsuario entidad = new DtoUsuario();
        //    entidad.C_DNI = usuario;
        //    entidad.C_PASSWORD = password;
        //    data = ctr.InicioSesion(entidad);
        //    return data;
        //}
    }
}
