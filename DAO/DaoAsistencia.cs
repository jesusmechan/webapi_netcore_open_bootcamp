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
    public class DaoAsistencia
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoAsistencia()
        {
            conexion = new SqlConnection(cadena);
        }
        public ClaseResultado<DtoAsistencia> MNT_Asistencia(DtoAsistencia _entidad)
        {
            int success = 0;
            SqlCommand cmd = null;
            var resultado = new ClaseResultado<DtoAsistencia>();

            try
            {
                cmd = new SqlCommand("USP_T_MANTENIMIENTO_ASISTENCIA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACCION", _entidad.ACCION);
                cmd.Parameters.AddWithValue("@codigoEstudiante", _entidad.codigoEstudiante);
                //cmd.Parameters.AddWithValue("@fechaIngreso", _entidad.fechaIngreso);
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
                    resultado.UltimoId = Convert.ToInt32(cmd.Parameters["@P_CODIGO"].Value);
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
    }
}
