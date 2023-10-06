using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class DaoUsuario
    {

        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoUsuario()
        {
            conexion = new SqlConnection(cadena);
        }

        //public SqlDataReader GetDr()
        //{
        //    return dr;
        //}

        public DtoUsuario InicioSesion(DtoUsuario entidad)
        {
            SqlCommand cmd = null;
            SqlDataReader dr;
            DtoUsuario _entidad = null;
            DtoRol _rol = null;
            var resultado = new ClaseResultado<DtoUsuario>();
            try
            {
                //conexion = DaoConexion.Conectar();
                cmd = new SqlCommand("USP_T_USUARIO_VERIFICAR_ACCESO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_USUARIO", entidad.LOGIN);
                cmd.Parameters.AddWithValue("@P_PASSWORD", entidad.PASSWORD);
                //cmd.Parameters.AddWithValue("@P_ID_TIENDA", entidad.C_ID_TIENDA);
                cmd.Parameters.Add("@P_MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _entidad = new DtoUsuario();
                    _rol = new DtoRol();
                    _entidad.ID_USUARIO = Convert.ToInt32(dr["ID_USUARIO"]);
                    _entidad.NOMBRE = dr["NOMBRE_COMPLETO"].ToString();
                    _entidad.CORREO = dr["CORREO"].ToString();
                    _rol.ID_ROL = Convert.ToInt32(dr["ID_ROL"]);
                    _rol.NOMBRE_ROL = Convert.ToString(dr["NOMBRE_ROL"]);
                    
                    _entidad.Rol = _rol;
                    resultado.Entidad = _entidad;
                    resultado.HuboError = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return _entidad;
        }
        //public int Usuario_Cerrar_Sesion(int ultimo_id)
        //{
        //    int success = 0;
        //    SqlCommand cmd = null;
        //    try
        //    {
        //        conexion = DaoConexion.Conectar();
        //        cmd = new SqlCommand("USP_T_SESION_USUARIO_CERRAR", conexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ULTIMO_ID", ultimo_id);
        //        conexion.Open();
        //        success = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //    }
        //    return success;
        //}
        public List<DtoUsuario> Usuario_Listar()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<DtoUsuario> _lista = null;
            DtoUsuario _entidad = null;
            DtoTipoUsuario _entidadTipUsu = null;
            try
            {
                //conexion = DaoConexion.Conectar();
                cmd = new SqlCommand("USP_T_USUARIO_LISTAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                dr = cmd.ExecuteReader();
                _lista = new List<DtoUsuario>();
                while (dr.Read())
                {
                    _entidad = new DtoUsuario();
                    _entidad.ID_USUARIO = Convert.ToInt32(dr["ID_USUARIO"]);
                    //_entidad.C_ID_TIENDA = Convert.ToInt32(dr["C_ID_TIENDA"]);
                    //_entidad.C_NOMBRE_TIENDA = Convert.ToString(dr["C_NOMBRE_TIENDA"]);
                    //_entidad.DNI = Convert.ToString(dr["DNI"]).Trim();
                    //_entidad.C_NOMBRECOMPLETO = Convert.ToString(dr["C_NOMBRECOMPLETO"]);
                    _entidad.NOMBRE = Convert.ToString(dr["NOMBRE"]);
                    _entidad.APELLIDO_PATERNO = Convert.ToString(dr["APELLIDO_PATERNO"]);
                    _entidad.APELLIDO_MATERNO = Convert.ToString(dr["APELLIDO_MATERNO"]);
                    _entidad.CORREO = Convert.ToString(dr["CORREO"]);
                    _entidad.PASSWORD = Convert.ToString(dr["PASSWORD"]);
                    //_entidad.CELULAR = Convert.ToString(dr["CELULAR"]);
                    _entidad.ESTADO = Convert.ToBoolean(dr["ESTADO"]);

                    //_entidad.C_ID_TIPO_USUARIO = Convert.ToInt32(dr["C_ID_TIPO_USUARIO"]);
                    //_entidadTipUsu = new DtoTipoUsuario();
                    //_entidadTipUsu.C_NOMBRE_TIPO_USUARIO = Convert.ToString(dr["C_NOMBRE_TIPO_USUARIO"]);
                    //_entidad.TipoUsuario = _entidadTipUsu;
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
        //public ClaseResultado<DtoUsuario> Usuario_Insertar_Actualizar(DtoUsuario _entidad, string _accion)
        //{
        //    int success = 0;
        //    SqlCommand cmd = null;
        //    var resultado = new ClaseResultado<DtoUsuario>();

        //    try
        //    {
        //        conexion = DaoConexion.Conectar();
        //        cmd = new SqlCommand("USP_T_USUARIO_INSERTAR_ACTUALIZAR", conexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@P_DATO", _accion);
        //        cmd.Parameters.AddWithValue("@P_ID_USUARIO", _entidad.C_ID_USUARIO);
        //        cmd.Parameters.AddWithValue("@P_TIPOUSUARIO", _entidad.C_ID_TIPO_USUARIO);
        //        //cmd.Parameters.AddWithValue("@P_ID_TIENDA", _entidad.C_ID_TIENDA);
        //        cmd.Parameters.AddWithValue("@P_DNI", _entidad.C_DNI);
        //        cmd.Parameters.AddWithValue("@P_NOMBRES", _entidad.C_NOMBRES);
        //        cmd.Parameters.AddWithValue("@P_APELLIDO_PATERNO", _entidad.C_APELLIDO_PATERNO);
        //        cmd.Parameters.AddWithValue("@P_APELLIDO_MATERNO", _entidad.C_APELLIDO_MATERNO);
        //        cmd.Parameters.AddWithValue("@P_CORREO", _entidad.C_CORREO);
        //        cmd.Parameters.AddWithValue("@P_PASSWORD", _entidad.C_PASSWORD);
        //        //cmd.Parameters.AddWithValue("@P_ESTADO", _entidad.C_ESTADO);
        //        cmd.Parameters.AddWithValue("@P_CELULAR", _entidad.C_CELULAR);
        //        cmd.Parameters.Add("@P_MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
        //        //_parameterValue.Add("Msj", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
        //        conexion.Open();
        //        success = cmd.ExecuteNonQuery();
        //        if (success == 1)
        //        {
        //            resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
        //            resultado.HuboError = false;
        //        }
        //        else
        //        {
        //            resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
        //            resultado.HuboError = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //    }
        //    return resultado;
        //}
        //public DtoUsuario Usuario_Consultar(int _usuarioid)
        //{
        //    SqlCommand cmd = null;
        //    SqlDataReader dr = null;
        //    DtoUsuario _entidad = null;

        //    try
        //    {
        //        conexion = DaoConexion.Conectar();

        //        cmd = new SqlCommand("USP_T_USUARIO_DETALLE", conexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@P_IDUSUARIO", _usuarioid);
        //        conexion.Open();
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            _entidad = new DtoUsuario();
        //            _entidad.C_ID_USUARIO = Convert.ToInt32(dr["C_ID_USUARIO"]);
        //            DtoTipoUsuario _entidadTipUsu = new DtoTipoUsuario();
        //            _entidadTipUsu.C_ID_TIPO_USUARIO = Convert.ToInt32(dr["C_ID_TIPO_USUARIO"]);
        //            _entidadTipUsu.C_NOMBRE_TIPO_USUARIO = Convert.ToString(dr["C_NOMBRE_TIPO_USUARIO"]);
        //            _entidad.TipoUsuario = _entidadTipUsu;
        //            _entidad.C_DNI = Convert.ToString(dr["C_DNI"]);
        //            _entidad.C_NOMBRES = Convert.ToString(dr["C_NOMBRES"]);
        //            _entidad.C_APELLIDO_PATERNO = Convert.ToString(dr["C_APELLIDO_PATERNO"]);
        //            _entidad.C_APELLIDO_MATERNO = Convert.ToString(dr["C_APELLIDO_MATERNO"]);
        //            _entidad.C_PASSWORD = Convert.ToString(dr["C_PASSWORD"]);
        //            //_entidad.C_ESTADO = Convert.ToString(dr["C_ESTADO"]);
        //            _entidad.C_CORREO = Convert.ToString(dr["C_CORREO"]);
        //            _entidad.C_CELULAR = Convert.ToString(dr["C_CELULAR"]);
        //        }

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //    }
        //    return _entidad;
        //}
        //public ClaseResultado<DtoUsuario> Usuario_Activar_Inactivar(int id_usuario, string parametro_cambio_estado)
        //{
        //    int success = 0;
        //    bool exito = false;
        //    SqlCommand cmd = null;
        //    var resultado = new ClaseResultado<DtoUsuario>();

        //    try
        //    {
        //        conexion = DaoConexion.Conectar();
        //        cmd = new SqlCommand("USP_T_USUARIO_ACTIVAR_INACTIVAR", conexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@P_ID_USUARIO", id_usuario);
        //        cmd.Parameters.AddWithValue("@P_PARAMETRO", parametro_cambio_estado);
        //        conexion.Open();
        //        success = cmd.ExecuteNonQuery();
        //        if (success == 1)
        //        {
        //            resultado.Mensaje = "Se actualizó correctamente el estado del usuario";
        //            resultado.HuboError = false;
        //        }
        //        else
        //        {
        //            resultado.Mensaje = "No se pudo actualizar el estado del usuario";
        //            resultado.HuboError = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Mensaje = ex.Message.ToString();
        //        resultado.HuboError = true;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //    }
        //    return resultado;
        //}
        //public int Usuario_Eliminar(int _usuarioid)
        //{
        //    SqlCommand cmd = null;
        //    int success = 0;

        //    try
        //    {
        //        conexion = DaoConexion.Conectar();
        //        cmd = new SqlCommand("USP_T_USUARIO_ELIMINAR", conexion);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@P_IDUSUARIO", _usuarioid);
        //        conexion.Open();
        //        success = cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //    }
        //    return success;
        //}


        #region IMPORTACION
        public void Usuario_Importar(List<DtoUsuario> _lista)
        {
            //BulkInsert bulk = new BulkInsert();
        }
        #endregion
    }
}
