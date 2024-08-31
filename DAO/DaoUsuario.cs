using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
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

        public DtoUsuario InicioSesion(DtoUsuario entidad)
        {
            var resultado = new DtoUsuario();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("P_USUARIO", entidad.LOGIN, DbType.String, ParameterDirection.Input);
                parametros.Add("P_PASSWORD", entidad.PASSWORD, DbType.String, ParameterDirection.Input);
                parametros.Add("P_MENSAJE", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
                var response = conexion.Query<DtoUsuario>("USP_T_USUARIO_VERIFICAR_ACCESO", parametros, commandType: CommandType.StoredProcedure);
                if(response.Count() > 0)
                    resultado = (DtoUsuario)response.ToList()[0];
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //resultado.Mensaje = ex.Message.ToString();
                //resultado.HuboError = true;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

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
                    _entidad.IDUSUARIO = Convert.ToInt32(dr["IDUSUARIO"]);
                    _entidad.IDROL = Convert.ToInt32(dr["IDROL"]);
                    _entidad.NUMERODOCUMENTO = Convert.ToString(dr["NUMERODOCUMENTO"]).Trim();
                    _entidad.NOMBRE = Convert.ToString(dr["NOMBRE"]);
                    _entidad.APELLIDOPATERNO = Convert.ToString(dr["APELLIDOPATERNO"]);
                    _entidad.APELLIDOMATERNO = Convert.ToString(dr["APELLIDOMATERNO"]);
                    _entidad.CORREO = Convert.ToString(dr["CORREO"]);
                    _entidad.SEXO = Convert.ToString(dr["SEXO"]);
                    _entidad.FECHANACIMIENTO = Convert.ToString(dr["FECHANACIMIENTO"]);
                    _entidad.LOGIN = Convert.ToString(dr["LOGIN"]);
                    _entidad.ESTADO = Convert.ToBoolean(dr["ESTADO"]);
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
        public ClaseResultado<DtoUsuario> Usuario_Insertar_Actualizar(DtoUsuario _entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoUsuario>();

            try
            {
                //conexion = DaoConexion.Conectar();
                cmd = new SqlCommand("USP_T_MANTENIMIENTO_USUARIO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACCION", _entidad.ACCION);
                cmd.Parameters.AddWithValue("@P_ID_USUARIO", _entidad.IDUSUARIO);
                cmd.Parameters.AddWithValue("@P_ID_ROL", _entidad.IDROL);
                cmd.Parameters.AddWithValue("@P_NUMERO_DOCUMENTO", _entidad.NUMERODOCUMENTO);
                cmd.Parameters.AddWithValue("@P_NOMBRE", _entidad.NOMBRE);
                cmd.Parameters.AddWithValue("@P_APELLIDO_PATERNO", _entidad.APELLIDOPATERNO);
                cmd.Parameters.AddWithValue("@P_APELLIDO_MATERNO", _entidad.APELLIDOMATERNO);
                cmd.Parameters.AddWithValue("@P_FECHA_NACIMIENTO", _entidad.FECHANACIMIENTO);
                cmd.Parameters.AddWithValue("@P_SEXO", _entidad.SEXO);
                cmd.Parameters.AddWithValue("@P_CORREO", _entidad.CORREO);
                cmd.Parameters.AddWithValue("@P_LOGIN", _entidad.LOGIN);
                cmd.Parameters.AddWithValue("@P_PASSWORD", _entidad.PASSWORD);
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

        public ClaseResultado<DtoUsuario> Usuario_Activar_Inactivar(DtoUsuario entidad)
        {
            int success = 0;
            bool exito = false;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoUsuario>();

            try
            {
                cmd = new SqlCommand("USP_T_USUARIO_ACTIVAR_INACTIVAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_IDUSUARIO", entidad.IDUSUARIO);
                cmd.Parameters.AddWithValue("@P_ACCION", entidad.ACCION);
                conexion.Open();
                success = cmd.ExecuteNonQuery();
                if (success == 1)
                {
                    resultado.Mensaje = "Se actualizó correctamente el estado del usuario";
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
                resultado.Mensaje = ex.Message.ToString();
                resultado.HuboError = true;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return resultado;
        }


        public ClaseResultado<Sesion> Sesion_MNT(Sesion entidad)
        {
            var resultado = new ClaseResultado<Sesion>();

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("IDSESION", entidad.IDSESION, DbType.String, ParameterDirection.Input);
                parametros.Add("IDUSUARIO", entidad.IDUSUARIO, DbType.String, ParameterDirection.Input);
                parametros.Add("ACCION", entidad.ACCION, DbType.String, ParameterDirection.Input);
                parametros.Add("P_CODIGO", DBNull.Value, DbType.Int32, ParameterDirection.Output);
                parametros.Add("P_MENSAJE", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
                if (conexion.Execute("USP_MNT_SESION", parametros, commandType: CommandType.StoredProcedure) > 0)
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


        public ClaseResultado<Sesion> Validar_Sesion(Sesion entidad)
        {
            var resultado = new ClaseResultado<Sesion>();

            try
            {
                var parametros = new DynamicParameters();
                //parametros.Add("IDSESION", entidad.IDSESION, DbType.String, ParameterDirection.Input);
                parametros.Add("IDUSUARIO", entidad.IDUSUARIO, DbType.String, ParameterDirection.Input);
                parametros.Add("P_EXISTE", DBNull.Value, DbType.Boolean, ParameterDirection.Output);
                parametros.Add("P_MENSAJE", DBNull.Value, DbType.String, ParameterDirection.Output, 100);
                if (conexion.Execute("USP_VALIDAR_SESION_X_USUARIO", parametros, commandType: CommandType.StoredProcedure) > 0)
                {
                    resultado.ExisteSesion = parametros.Get<bool>("P_EXISTE");
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.ExisteSesion = false;
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


        public ClaseResultado<SesionXUsuario> Listar_Usuarios_Logueados()
        {
            var resultado = new ClaseResultado<SesionXUsuario>();
            try
            {

                //_lista = new List<SesionXUsuario>();
                var response = conexion.Query<SesionXUsuario>("USP_LISTAR_USUARIO_X_SESION", null, commandType: CommandType.StoredProcedure);
                if (response.Count() > 0)
                    resultado.Lista = (List<SesionXUsuario>)response;
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
