using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CtrCategoria
    {
        DaoCategoria _dao = null;
        ClaseResultado<DtoCategoria> _resultado = null;
        public CtrCategoria()
        {
            _dao = new DaoCategoria();
            _resultado = new ClaseResultado<DtoCategoria>();
        }

        public List<DtoCategoria> Categoria_Listar()
        {
            try
            {
                return _dao.Categoria_Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public ClaseResultado<DtoCategoria> Categoria_Insertar_Actualizar(DtoCategoria _entidad)
        {
            return _resultado = _dao.Categoria_Insertar_Actualizar(_entidad);
        }

        public bool Producto_Insertar_Imagen(DtoCategoria entidad)
        {
            return _dao.Categoria_Insertar_Imagen(entidad);
        }

        public ClaseResultado<DtoCategoria> Categoria_Activar_Inactivar(DtoCategoria entidad)
        {
            try
            {
                return _resultado = _dao.Categoria_Activar_Inactivar(entidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
