using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Dapper;
using DTO;
using Microsoft.SqlServer.Server;

namespace DAO
{
    public class DaoImportacion
    {
        SqlConnection conexion = null;
        string cadena = DaoConexion.cadenaConexion;
        public DaoImportacion() 
        {
            conexion = new SqlConnection(cadena);
        }

        //public void InsertDataIntoDatabase(List<DtoImportacionVisitante> dataList)
        //{
        //        var dataTable = new DataTable();
        //        dataTable.Columns.Add("Column1", typeof(string));
        //        dataTable.Columns.Add("Column2", typeof(string));
        //        dataTable.Columns.Add("Column3", typeof(string));
        //        dataTable.Columns.Add("Column4", typeof(string));

        //        foreach (var data in dataList)
        //        {
        //            dataTable.Rows.Add(data.codigo, data.nombres, data.apellidoPaterno, data.apellidoMaterno);
        //        }

        //        using (var connection = new SqlConnection(conexion.ConnectionString))
        //        {
        //            connection.Open();
        //            using (var bulkCopy = new SqlBulkCopy(connection))
        //            {
        //                bulkCopy.DestinationTableName = "temporalImportacionUsuario";
        //                bulkCopy.ColumnMappings.Add("Column1", "CODIGO");
        //                bulkCopy.ColumnMappings.Add("Column2", "NOMBRES");
        //                bulkCopy.ColumnMappings.Add("Column3", "APELLIDOPATERNO");
        //                bulkCopy.ColumnMappings.Add("Column4", "APELLIDOMATERNO");
        //                bulkCopy.WriteToServer(dataTable);
        //            }
        //        }
        //}

        //public void InsertDataIntoDatabase(List<DtoImportacionVisitante> dataList)
        //{
        //    const int maxParameters = 2100; // Máximo de parámetros permitidos por solicitud
        //    const int columnsPerRecord = 2; // Número de columnas en cada registro
        //    int batchSize = 200;

        //    using (var connection = new SqlConnection(conexion.ConnectionString))
        //    {
        //        connection.Open();
        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            for (int i = 0; i < dataList.Count; i += batchSize)
        //            {
        //                var batch = dataList.Skip(i).Take(batchSize).ToList();
        //                var query = new StringBuilder("INSERT INTO temporalImportacionUsuario (CODIGO, NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO) VALUES ");

        //                var parameters = new DynamicParameters();
        //                for (int j = 0; j < batch.Count; j++)
        //                {
        //                    query.Append($"(@Column1_{j}, @Column2_{j},@Column3_{j}, @Column4_{j}),");

        //                    parameters.Add($"Column1_{j}", batch[j].codigo);
        //                    parameters.Add($"Column2_{j}", batch[j].nombres);
        //                    parameters.Add($"Column3_{j}", batch[j].apellidoPaterno);
        //                    parameters.Add($"Column4_{j}", batch[j].apellidoMaterno);

        //                }

        //                query.Length--; // Eliminar la última coma
        //                query.Append(";");

        //                connection.Execute(query.ToString(), parameters, transaction: transaction);
        //            }

        //            transaction.Commit();
        //        }
        //    }
        //}



        //public void BulkInsertData(List<DtoImportacionVisitante> dataList)
        //{
        //    using (var connection = new SqlConnection(conexion.ConnectionString))
        //    {
        //        connection.Open();

        //        // Convierte la lista a un DataTable
        //        var dataTable = ToDataTable(dataList);

        //        //foreach (var data in dataList)
        //        //{
        //        //    dataTable.Rows.Add(data.Column1, data.Column2);
        //        //}

        //        // Define el parámetro del tipo de tabla
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@DataModel", dataTable.AsTableValuedParameter("dbo.DataModelType"));

        //        // Llama al procedimiento almacenado
        //        connection.Execute("dbo.InsertDataModel", parameters, commandType: CommandType.StoredProcedure);
        //    }
        //}



        //public static DataTable ToDataTable<T>(List<T> items)
        //{
        //    var dataTable = new DataTable(typeof(T).Name);

        //    // Obtener todas las propiedades públicas del tipo T
        //    var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var prop in properties)
        //    {
        //        // Crear columnas en el DataTable con el nombre y el tipo de la propiedad
        //        dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //    }

        //    foreach (var item in items)
        //    {
        //        var values = new object[properties.Length];
        //        for (int i = 0; i < properties.Length; i++)
        //        {
        //            values[i] = properties[i].GetValue(item, null);
        //        }
        //        dataTable.Rows.Add(values);
        //    }

        //    return dataTable;
        //}

        public void InsertarDatosGA(List<DtoImportacionVisitante> valores)
        {
            try
            {
                using (var cnx = new SqlConnection(conexion.ConnectionString))
                {
                    using (var comando = new SqlCommand("InsertDataModel", cnx))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        SqlParameter parametro = new SqlParameter("@DataModel", SqlDbType.Structured)
                        {
                            TypeName = "dbo.DataModelType",
                            Value = ObtenerContenidoLista(valores)
                        };

                        comando.Parameters.Add(parametro);

                        cnx.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private static IEnumerable<SqlDataRecord> ObtenerContenidoLista(List<DtoImportacionVisitante> valores)
        {
            SqlMetaData[] esquema = new SqlMetaData[] 
            {
                new SqlMetaData("Column1", SqlDbType.NVarChar, 100),
                new SqlMetaData("Column2", SqlDbType.NVarChar, 100),
                new SqlMetaData("Column3", SqlDbType.NVarChar, 100),
                new SqlMetaData("Column4", SqlDbType.NVarChar, 100),
            };

            SqlDataRecord _DataRecord = new SqlDataRecord(esquema);

            foreach (var valor in valores)
            {
                _DataRecord.SetString(0, valor.codigo);
                _DataRecord.SetString(1, valor.nombres);
                _DataRecord.SetString(2, valor.apellidoPaterno);
                _DataRecord.SetString(3, valor.apellidoMaterno);
                yield return _DataRecord;
            }
        }



    }
}
