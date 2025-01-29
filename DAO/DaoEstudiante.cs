using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;

namespace DAO
{
    public class DaoEstudiante
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoEstudiante()
        {
            conexion = new SqlConnection(cadena);
        }

        public ClaseResultado<DtoEstudiante> listarEstudiante(filtrosBusqueda entidad)
        {
            var resultado = new ClaseResultado<DtoEstudiante>();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdEspecialdiad", entidad.idEspcialidad, DbType.String, ParameterDirection.Input);
                var response = conexion.Query<DtoEstudiante>("USP_T_LISTAR_ESTUDIANTES", parametros, commandType: CommandType.StoredProcedure);
                if (response.Count() > 0)
                    resultado.Lista = (List<DtoEstudiante>)response;
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

        public ClaseResultado<DtoEstudiante> MNT_Estudiante(DtoEstudiante _entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoEstudiante>();

            try
            {
                //conexion = DaoConexion.Conectar();
                cmd = new SqlCommand("USP_T_MANTENIMIENTO_ESTUDIANTE", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACCION", _entidad.ACCION);
                cmd.Parameters.AddWithValue("@idEstudiante", _entidad.idEstudiante);
                cmd.Parameters.AddWithValue("@idEspecialidad", _entidad.idEspecialidad);
                cmd.Parameters.AddWithValue("@idTipo", 1);
                cmd.Parameters.AddWithValue("@codigoEstudiante", _entidad.codigoEstudiante);
                cmd.Parameters.AddWithValue("@nombre", _entidad.nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", _entidad.apellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", _entidad.apellidoMaterno);
                cmd.Parameters.AddWithValue("@usuarioRegistro", _entidad.IDUSUARIOCREACION);
                cmd.Parameters.Add("@P_CODIGO", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@P_MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                conexion.Open();
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    resultado.UltimoId = Convert.ToInt32(cmd.Parameters["@P_CODIGO"].Value);
                    resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
                    resultado.HuboError = true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                resultado.Mensaje = ex.Message.ToString();
                resultado.HuboError = true;
            }
            finally
            {

                cmd.Connection.Close();
            }
            return resultado;
        }

        public ClaseResultado<DtoEstudiante> Estudiante_Activar_Inactivar(DtoEstudiante entidad)
        {
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoEstudiante>();

            var parametros = new DynamicParameters();
            parametros.Add("P_ACCION", entidad.ACCION, DbType.String, ParameterDirection.Input);
            parametros.Add("P_IDESTUDIANTE", entidad.idEstudiante, DbType.Int32, ParameterDirection.Input);
            parametros.Add("P_MENSAJE", DBNull.Value, dbType: DbType.String, direction: ParameterDirection.Output, 100);


            try
            {
                if(conexion.Execute("USP_T_ESTUDIANTE_ACTIVAR_INACTIVAR", parametros, commandType: CommandType.StoredProcedure) > 0)
                {
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.Mensaje = "No se pudo actualizar el estado del usuario";
                    resultado.HuboError = true;
                }
                    
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                resultado.Mensaje = ex.Message.ToString();
                resultado.HuboError = true;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }


    }
}
