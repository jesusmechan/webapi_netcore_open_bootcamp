using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DTO;


namespace DAO
{
    public class DaoReportes
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoReportes()
        {
            conexion = new SqlConnection(cadena);
        }

        public ClaseResultado<DtoReporteAsistencia> listarReporteAsistencia(filtrosReporteAsistencia entidad)
        {
            var resultado = new ClaseResultado<DtoReporteAsistencia>();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("idEspecialidad", entidad.idEspecialidad, DbType.String, ParameterDirection.Input);
                parametros.Add("fechaInicio", entidad.fechaInicio, DbType.String, ParameterDirection.Input);
                parametros.Add("fechaFin", entidad.fechaFin, DbType.String, ParameterDirection.Input);
                var response = conexion.Query<DtoReporteAsistencia>("USP_T_REPORTE_ASISTENCIA", parametros, commandType: CommandType.StoredProcedure);
                if (response.Count() > 0)
                    resultado.Lista = (List<DtoReporteAsistencia>)response;
                //else{
                //    resultado.Lista = new List<DtoReporteAsistencia>();
                //}
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.Mensaje = ex.ToString();
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }

        public ClaseResultado<DtoReporteAsistenciaXMes> listarReporteAsistenciaXMes(filtrosRepXMes entidad)
        {
            var resultado = new ClaseResultado<DtoReporteAsistenciaXMes>();
            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ANIO", entidad.Anio, DbType.String, ParameterDirection.Input);
                var response = conexion.Query<DtoReporteAsistenciaXMes>("USP_T_LIS_DASHBOARD_PIVOT", parametros, commandType: CommandType.StoredProcedure);
                if (response.Count() > 0)
                    resultado.Lista = (List<DtoReporteAsistenciaXMes>)response;
            }
            catch (Exception ex)
            {
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
