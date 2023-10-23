using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAO
{
    public class DaoRol
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoRol()
        {
            conexion = new SqlConnection(cadena);
        }

        public List<DtoRol> Rol_Listar()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<DtoRol> _lista = null;
            DtoRol _entidad = null;

            using (IDbConnection dbConnection = conexion)
            {
                // Ejecuta el procedimiento almacenado y obtiene los resultados en una lista de objetos Usuario.
                _lista = dbConnection.Query<DtoRol>("USP_T_ROL_LISTAR", commandType: CommandType.StoredProcedure).ToList();
                // Ahora puedes trabajar con la lista de usuarios.
                //foreach (var usuario in usuarios)
                //{
                //    Console.WriteLine($"ID: {usuario.Id}, Nombre: {usuario.Nombre}, Email: {usuario.Email}");
                //}
            }

            //try
            //{
            //    cmd = new SqlCommand("USP_T_CATEGORIA_LISTAR", conexion);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    conexion.Open();
            //    dr = cmd.ExecuteReader();
            //    _lista = new List<DtoRol>();
            //    while (dr.Read())
            //    {
            //        _entidad = new DtoRol();
            //        _entidad.IDROL = Convert.tos
            //            //_lista.Add(_entidad);
            //        }
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //}
            //finally
            //{
            //    cmd.Connection.Close();
            //}

            return _lista;
        }

        public ClaseResultado<DtoRol> Rol_Insertar_Actualizar(DtoRol _entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoRol>();

            try
            {
                //conexion = DaoConexion.Conectar();
                cmd = new SqlCommand("USP_T_MANTENIMIENTO_ROL", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACCION", _entidad.ACCION);
                cmd.Parameters.AddWithValue("@P_ID_ROL", _entidad.IDROL);
                cmd.Parameters.AddWithValue("@P_NOMBRE_ROL", _entidad.NOMBREROL);
                cmd.Parameters.AddWithValue("@P_USUARIO_CREACION", _entidad.IDUSUARIOCREACION);
                cmd.Parameters.AddWithValue("@P_USUARIO_MODIFICACION", _entidad.IDUSUARIOMODIFICACION);
                cmd.Parameters.Add("@P_CODIGO", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@P_MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                //_parameterValue.Add("Msj", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
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


        public ClaseResultado<DtoRol> Rol_Activar_Inactivar(DtoRol entidad)
        {
            int success = 0;
            bool exito = false;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoRol>();

            try
            {
                cmd = new SqlCommand("USP_T_ROL_ACTIVAR_INACTIVAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_IDROL", entidad.IDROL);
                cmd.Parameters.AddWithValue("@P_ACCION", entidad.ACCION);
                conexion.Open();
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    resultado.Mensaje = "Se actualizó correctamente el estado del rol";
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.Mensaje = "No se pudo actualizar el estado del rol";
                    resultado.HuboError = true;
                }

            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message.ToString();
                resultado.HuboError = true;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return resultado;
        }

    }

}