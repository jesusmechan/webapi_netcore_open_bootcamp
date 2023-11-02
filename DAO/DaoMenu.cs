using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;
namespace DAO
{
    public class DaoMenu
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoMenu()
        {

            conexion = new SqlConnection(cadena);
        }

        public List<DtoMenu> Menu_Listar(DtoMenu entidad)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<DtoMenu> _lista = null;
            DtoMenu _Menu = null;
            DtoSubMenu _subMenu = null;
            try
            {
                //conexion = DaoConexion.Conectar();
                cmd = new SqlCommand("USP_T_MENU_ROL_LISTAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_USUARIO", entidad.IDUSUARIO);
                cmd.Parameters.AddWithValue("@ID_ROL", entidad.IDROL);
                conexion.Open();
                dr = cmd.ExecuteReader();
                _lista = new List<DtoMenu>();
                while (dr.Read())
                {
                    _Menu = new DtoMenu();
                    _Menu.IDMENU = Convert.ToInt32(dr["IDMENU"]);
                    _Menu.NOMBREMENU = Convert.ToString(dr["NOMBRE_MENU"]);
                    _Menu.NUMEROORDEN = Convert.ToInt32(dr["NUMERO_ORDEN_MENU"]);

                    _subMenu = new DtoSubMenu();
                    _subMenu.IDMENU = Convert.ToInt32(dr["IDMENU"]);
                    _subMenu.IDSUBMENU = Convert.ToInt32(dr["ID_MENU_SUB_MENU"]);
                    _subMenu.NOMBRESUBMENU = Convert.ToString(dr["NOMBRE_SUB_MENU"]);
                    _subMenu.RUTA = Convert.ToString(dr["RUTA"]);
                    _subMenu.ICONO = Convert.ToString(dr["ICONO"]);
                    _subMenu.NUMERO_ORDEN = Convert.ToInt32(dr["NUMEROORDENSUB_MENU"]);
                    _Menu.dtoSubMenu = _subMenu;
                    _lista.Add(_Menu);
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


        public List<DtoMenu> Menu_Listar_Tabla()
        {

            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoMenu>();
            List<DtoMenu> _lista = null;
            try
            {
                _lista = new List<DtoMenu>();
                var response = conexion.Query<DtoMenu>("USP_T_MENU_ROL_LISTAR_TABLA", null, commandType: CommandType.StoredProcedure);
                if (response.Count() > 0)
                    _lista = response.ToList();

            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.Mensaje = ex.Message.ToString();
            }
            return _lista;
        }

        public ClaseResultado<DtoMenu> Menu_Insertar_Actualizar(DtoMenu _entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoMenu>();

            try
            {
                //cmd = new SqlCommand("USP_T_MANTENIMIENTO_ROL", conexion);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ACCION", _entidad.ACCION);
                //cmd.Parameters.AddWithValue("@P_ID_MENU", _entidad.IDMENU);
                //cmd.Parameters.AddWithValue("@P_NOMBRE_MENU", _entidad.NOMBREMENU);
                //cmd.Parameters.AddWithValue("@P_NUMERO_ORDEN", _entidad.NUMEROORDEN);
                //cmd.Parameters.AddWithValue("@P_USUARIO_CREACION", _entidad.IDUSUARIOCREACION);
                //cmd.Parameters.AddWithValue("@P_USUARIO_MODIFICACION", _entidad.IDUSUARIOMODIFICACION);
                //cmd.Parameters.Add("@P_CODIGO", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@P_MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                //conexion.Open();
                //success = cmd.ExecuteNonQuery();
                //if (success == 1)
                //{
                //    resultado.UltimoId = Convert.ToInt32(cmd.Parameters["@P_CODIGO"].Value);
                //    resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
                //    resultado.HuboError = false;
                //}
                //else
                //{
                //    resultado.Mensaje = cmd.Parameters["@P_MENSAJE"].Value.ToString();
                //    resultado.HuboError = true;
                //}

                var parametros = new DynamicParameters();
                parametros.Add("ACCION", _entidad.ACCION, DbType.String, ParameterDirection.Input);
                parametros.Add("P_ID_MENU", _entidad.IDMENU, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_NOMBRE_MENU", _entidad.NOMBREMENU, DbType.String, ParameterDirection.Input);
                parametros.Add("P_NUMERO_ORDEN", _entidad.NUMEROORDEN, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_USUARIO_CREACION", _entidad.IDUSUARIOCREACION, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_USUARIO_MODIFICACION", _entidad.IDUSUARIOMODIFICACION, DbType.Int32, ParameterDirection.Input);
                parametros.Add("P_CODIGO", DBNull.Value, dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("P_MENSAJE", DBNull.Value, dbType: DbType.String, direction: ParameterDirection.Output , 100);
                if (conexion.Execute("USP_T_MANTENIMIENTO_MENU", parametros, commandType: CommandType.StoredProcedure) > 0)
                {
                    resultado.UltimoId = parametros.Get<int>("P_CODIGO");
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = false;
                }
                else
                {
                    resultado.Mensaje = parametros.Get<string>("P_MENSAJE");
                    resultado.HuboError = true;
                }
                //P_CODIGO
                //P_MENSAJE
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                resultado.Mensaje = ex.Message.ToString();
                resultado.HuboError = true;
            }
            finally
            {
                //conexion.Dispose();
                conexion.Close();
                //cmd.Connection.Close();
            }
            return resultado;
        }

        public ClaseResultado<DtoMenu> Menu_Activar_Inactivar(DtoMenu entidad)
        {
            int success = 0;
            bool exito = false;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoMenu>();

            try
            {
                cmd = new SqlCommand("USP_T_MENU_ACTIVAR_INACTIVAR", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_ID_MENU", entidad.IDMENU);
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