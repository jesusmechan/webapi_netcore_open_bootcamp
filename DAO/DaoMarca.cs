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
    public class DaoMarca
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        SqlCommand cmd = null;
        public DaoMarca() { 
            conexion = new SqlConnection(cadena);
        }
        
        //public List<DtoMarca> Marca_Listar()
        public ClaseResultado<DtoMarca> Marca_Listar()
        {
            var resultado = new ClaseResultado<DtoMarca>();
            try
            {
                var response = conexion.Query<DtoMarca>("USP_T_MARCA_LISTAR", null, commandType: CommandType.StoredProcedure);
                if(response.Count() > 0)
                    resultado.Lista = response.ToList();
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.Mensaje = ex.Message.ToString();
            }
            return resultado;
        }


        public ClaseResultado<DtoMarca> Marca_Insertar_Actualizar(DtoMarca _entidad)
        {
            var resultado = new ClaseResultado<DtoMarca>();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("ACCION", _entidad.ACCION, DbType.String, ParameterDirection.Input);
                parametros.Add("P_ID_MARCA", _entidad.IDMARCA, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_NOMBRE_MARCA", _entidad.NOMBREMARCA, DbType.String, ParameterDirection.Input);
                parametros.Add("P_USUARIO_CREACION", _entidad.IDUSUARIOCREACION, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_USUARIO_MODIFICACION", _entidad.IDUSUARIOMODIFICACION, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_CODIGO", DBNull.Value, DbType.Int32, ParameterDirection.Output);
                parametros.Add("P_MENSAJE", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
                if(conexion.Execute("USP_T_MANTENIMIENTO_MARCA", parametros, commandType: CommandType.StoredProcedure) > 0)
                {
                    resultado.UltimoId = parametros.Get<int>("P_CODIGO");
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.UltimoId = 0;
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = true;
                }
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

        public ClaseResultado<DtoMarca> Marca_Activar_Inactivar(DtoMarca _entidad)
        {
            var resultado = new ClaseResultado<DtoMarca>();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("P_ACCION", _entidad.ACCION, DbType.String, ParameterDirection.Input);
                parametros.Add("P_IDMARCA", _entidad.IDMARCA, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_CODIGO", DBNull.Value, DbType.Int32, ParameterDirection.Output);
                parametros.Add("P_MENSAJE", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
                if (conexion.Execute("USP_T_MARCA_ACTIVAR_INACTIVAR", parametros, commandType: CommandType.StoredProcedure) > 0)
                {
                    resultado.UltimoId = parametros.Get<int>("P_CODIGO");
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.UltimoId = parametros.Get<int>("P_CODIGO");
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = true;
                }
            }
            catch (Exception ex)
            {
                resultado.UltimoId = 0;
                resultado.HuboError = true;
                resultado.Mensaje = ex.Message.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }
    }
}
