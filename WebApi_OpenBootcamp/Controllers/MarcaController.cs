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
    public class MarcaController : ControllerBase
    {
        CtrMarca ctr = null;
        ClaseResultado<DtoMarca> resultado = null;
        public MarcaController() 
        {
            ctr = new CtrMarca();
            resultado = new ClaseResultado<DtoMarca>();
        }

        [HttpGet]
        public ClaseResultado<DtoMarca> Marca_Listar()
        {
            try
            {
                resultado = ctr.Marca_Listar();
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.Mensaje = ex.Message.ToString();
            }
            return resultado;
        }


        [HttpPost]
        public ClaseResultado<DtoMarca> Marca_Insertar_Actualizar(DtoMarca _entidad)
        {
            try
            {
                resultado = ctr.Marca_Insertar_Actualizar(_entidad);
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
        public ClaseResultado<DtoMarca> Marca_Activar_Inactivar(DtoMarca _entidad)
        {
            try
            {
                resultado = ctr.Marca_Activar_Inactivar(_entidad);
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.UltimoId = 0;
                resultado.Mensaje = ex.Message.ToString();
            }
            return resultado;
        }

    }
}
