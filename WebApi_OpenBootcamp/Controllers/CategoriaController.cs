using Controladora;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        CtrCategoria ctr = null;
        public CategoriaController()
        {
            ctr = new CtrCategoria();
        }

        [HttpGet]
        public List<DtoCategoria> Categoria_Listar()
        {
            List<DtoCategoria> _lista = new List<DtoCategoria>();
            _lista = ctr.Categoria_Listar();
            return _lista;
        }

        [HttpPost]
        public ClaseResultado<DtoCategoria> Categoria_Insertar_Actualizar(DtoCategoria _entidad)
        {
            ClaseResultado<DtoCategoria> resultado = new ClaseResultado<DtoCategoria>();
            resultado = ctr.Categoria_Insertar_Actualizar(_entidad);
            return resultado;
        }

        [HttpPost]
        public ClaseResultado<DtoCategoria> Categoria_Activar_Desactivar(DtoCategoria _entidad)
        {
            ClaseResultado<DtoCategoria> resultado = new ClaseResultado<DtoCategoria>();
            resultado = ctr.Categoria_Activar_Inactivar(_entidad);
            return resultado;
        }

    }
}
