﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    }
}