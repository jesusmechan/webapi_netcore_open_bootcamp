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
    public class DaoCategoria
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoCategoria()
        {

            conexion = new SqlConnection(cadena);
        }

        public List<DtoCategoria> Categoria_Listar()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<DtoCategoria> _lista = null;
            DtoCategoria _entidad = null;
            try
            {
                cmd = new SqlCommand("USP_T_CATEGORIA_LISTAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                dr = cmd.ExecuteReader();
                _lista = new List<DtoCategoria>();
                while (dr.Read())
                {
                    _entidad = new DtoCategoria();
                    _entidad.C_ID_CATEGORIA = Convert.ToInt32(dr["C_ID_CATEGORIA"]);
                    _entidad.C_NOMBRE_CATEGORIA = Convert.ToString(dr["C_NOMBRE_CATEGORIA"]);
                    _entidad.RutaImagen = dr["C_RUTA_IMAGEN"] == DBNull.Value ? string.Empty : Convert.ToString(dr["C_RUTA_IMAGEN"]);
                    _entidad.NombreImagen = dr["C_NOMBRE_IMAGEN"] == DBNull.Value ? string.Empty : Convert.ToString(dr["C_NOMBRE_IMAGEN"]);
                    _entidad.C_ESTADO = Convert.ToBoolean(dr["C_ESTADO"]);
                    _entidad.C_USUARIO_REGISTRO = Convert.ToInt32(dr["C_USUARIO_REGISTRO"]);
                    _entidad.C_FECHA_REGSITRO = Convert.ToString(dr["C_FECHA_REGISTRO"]);
                    _lista.Add(_entidad);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                cmd.Connection.Close();
            }

            return _lista;
        }
        public ClaseResultado<DtoCategoria> Categoria_Insertar_Actualizar(DtoCategoria _entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoCategoria>();

            try
            {
                cmd = new SqlCommand("USP_T_CATEGORIA_INSERTAR_ACTUALIZAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_DATO", _entidad.C_ACCION);
                cmd.Parameters.AddWithValue("@P_ID_CATEGORIA", _entidad.C_ID_CATEGORIA);
                cmd.Parameters.AddWithValue("@P_USUARIO_REGISTRO", _entidad.C_USUARIO_REGISTRO);
                cmd.Parameters.AddWithValue("@P_NOMBRE_CATEGORIA", _entidad.C_NOMBRE_CATEGORIA);
                cmd.Parameters.AddWithValue("@P_USUARIO_MODIFICACION", _entidad.C_USUARIO_MODIFICACION);
                cmd.Parameters.Add("@P_MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ULTIMO_ID_INSERTADO", SqlDbType.Int).Direction = ParameterDirection.Output;
                conexion.Open();
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
                    resultado.UltimoId = Convert.ToInt32(cmd.Parameters["@ULTIMO_ID_INSERTADO"].Value);
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
            }
            finally
            {
                cmd.Connection.Close();
            }
            return resultado;
        }

        public bool Categoria_Insertar_Imagen(DtoCategoria entidad)
        {
            int success = 0;
            bool resultado = false;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("USP_T_CATEGORIA_ACTUALIZAR_IMAGEN", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_ID_CATEGORIA", entidad.C_ID_CATEGORIA);
                cmd.Parameters.AddWithValue("@P_RUTA_IMAGEN", entidad.RutaImagen);
                cmd.Parameters.AddWithValue("@P_NOMBRE_IMAGEN", entidad.NombreImagen);
                conexion.Open();
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                resultado = false;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return resultado;
        }

        public ClaseResultado<DtoCategoria> Categoria_Activar_Inactivar(DtoCategoria entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoCategoria>();

            try
            {
                cmd = new SqlCommand("USP_T_CATEGORIA_ACTIVAR_INACTIVAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_ID_CATEGORIA", entidad.C_ID_CATEGORIA);
                cmd.Parameters.AddWithValue("@P_PARAMETRO", entidad.C_ACCION);
                conexion.Open();
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    resultado.Mensaje = "Se actualizó correctamente el estado de la categoria";
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.Mensaje = "No se pudo actualizar el estado de la categoria";
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
