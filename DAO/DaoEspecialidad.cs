using Dapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DaoEspecialidad
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoEspecialidad()
        {
            conexion = new SqlConnection(cadena);
        }

        public ClaseResultado<DtoEspecialidad> Listar_Especialidad()
        {
            var resultado = new ClaseResultado<DtoEspecialidad>();
            try
            {
                var response = conexion.Query<DtoEspecialidad>("USP_T_LISTAR_ESPECIALIDAD", null, commandType: CommandType.StoredProcedure);
                if (response.Count() > 0)
                    resultado.Lista = (List<DtoEspecialidad>)response;
            }
            catch (Exception ex)
            {
                resultado.UltimoId = 0;
                resultado.HuboError = true;
                resultado.Mensaje = ex.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return resultado;

        }
    }
}
