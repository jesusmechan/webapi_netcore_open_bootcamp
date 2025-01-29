using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.ConstrainedExecution;
using ClosedXML.Excel;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        CtrReporteAsistencia ctr = null;
        ClaseResultado<DtoReporteAsistencia> resultado = null;
        ClaseResultado<DtoReporteAsistenciaXMes> _resRepXMEs = null;
        public ReportesController()
        {
            ctr = new CtrReporteAsistencia();
            resultado = new ClaseResultado<DtoReporteAsistencia>();
            _resRepXMEs = new ClaseResultado<DtoReporteAsistenciaXMes>();
        }

        [HttpPost]
        public ClaseResultado<DtoReporteAsistencia> listarReporteAsistencia(filtrosReporteAsistencia entidad)
        {
            try
            {
                resultado = ctr.listarReporteAsistencia(entidad);
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.Mensaje = ex.Message.ToString();
            }
            return resultado;
        }

        [HttpPost]
        public ClaseResultado<DtoReporteAsistenciaXMes> listarReporteAsistenciaXMes(filtrosRepXMes entidad)
        {
            try
            {
                _resRepXMEs = ctr.listarReporteAsistenciaXMes(entidad);
                return _resRepXMEs;
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.Mensaje = ex.Message.ToString();
            }
            return _resRepXMEs;
        }


        [HttpPost]
        public IActionResult ExportToExcelReporteAsistencia(DtoListReporteAsistencia lista)
        {

            List<DtoReporteAsistencia> datos = new List<DtoReporteAsistencia>();
            datos = lista.lista;

            // Crear workbook y hoja
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Asistencias");
            // Cabecera dinámica
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Código";
            worksheet.Cell(1, 3).Value = "Nombres";
            worksheet.Cell(1, 4).Value = "Apellido Paterno";
            worksheet.Cell(1, 5).Value = "Apellido Materno";
            worksheet.Cell(1, 6).Value = "Especialidad";
            worksheet.Cell(1, 7).Value = "Fecha Ingreso";
            worksheet.Cell(1, 8).Value = "Hora Ingreso";


            // Estilo para la cabecera
            var headerRange = worksheet.Range("A1:H1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int currentRow = 2;

            foreach (var row in datos)
            {
                worksheet.Cell(currentRow, 1).Value = row.idEstudiante;
                worksheet.Cell(currentRow, 2).Value = row.codigoEstudiante;
                worksheet.Cell(currentRow, 3).Value = row.nombre;
                worksheet.Cell(currentRow, 4).Value = row.apellidoPaterno;
                worksheet.Cell(currentRow, 5).Value = row.apellidoMaterno;
                worksheet.Cell(currentRow, 6).Value = row.especialidad;
                worksheet.Cell(currentRow, 7).Value = row.fechaIngresoFormat;
                worksheet.Cell(currentRow, 8).Value = row.horaIngreso;

                // Estilo para centrar los valores de Enero a Diciembre
                for (int col = 2; col <= 8; col++)
                {
                    worksheet.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                currentRow++;
            }

            // Ajustar ancho de columnas
            worksheet.Columns().AdjustToContents();

            // Guardar en memoria
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Retornar el archivo como respuesta
            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Reporte_Asistencia.xlsx"
            );


        }



        [HttpPost]
        public IActionResult ExportToExcelReporteAsistenciaXMes(DtoListReporteAsistenciaXMes lista)
        {

            List<DtoReporteAsistenciaXMes> datos = new List<DtoReporteAsistenciaXMes>();
            datos = lista.lista;

            // Crear workbook y hoja
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Asistencias");
            // Cabecera dinámica
            worksheet.Cell(1, 1).Value = "Especialidad";
            worksheet.Cell(1, 2).Value = "Enero";
            worksheet.Cell(1, 3).Value = "Febrero";
            worksheet.Cell(1, 4).Value = "Marzo";
            worksheet.Cell(1, 5).Value = "Abril";
            worksheet.Cell(1, 6).Value = "Mayo";
            worksheet.Cell(1, 7).Value = "Junio";
            worksheet.Cell(1, 8).Value = "Julio";
            worksheet.Cell(1, 9).Value = "Agosto";
            worksheet.Cell(1, 10).Value = "Septiembre";
            worksheet.Cell(1, 11).Value = "Octubre";
            worksheet.Cell(1, 12).Value = "Noviembre";
            worksheet.Cell(1, 13).Value = "Diciembre";


            // Estilo para la cabecera
            var headerRange = worksheet.Range("A1:M1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int currentRow = 2;

            foreach (var row in datos)
            {
                worksheet.Cell(currentRow, 1).Value = row.Especialidad;
                worksheet.Cell(currentRow, 2).Value = row.Enero;
                worksheet.Cell(currentRow, 3).Value = row.Febrero;
                worksheet.Cell(currentRow, 4).Value = row.Marzo;
                worksheet.Cell(currentRow, 5).Value = row.Abril;
                worksheet.Cell(currentRow, 6).Value = row.Mayo;
                worksheet.Cell(currentRow, 7).Value = row.Junio;
                worksheet.Cell(currentRow, 8).Value = row.Julio;
                worksheet.Cell(currentRow, 9).Value = row.Agosto;
                worksheet.Cell(currentRow, 10).Value = row.Septiembre;
                worksheet.Cell(currentRow, 11).Value = row.Octubre;
                worksheet.Cell(currentRow, 12).Value = row.Noviembre;
                worksheet.Cell(currentRow, 13).Value = row.Diciembre;

                // Estilo para centrar los valores de Enero a Diciembre
                for (int col = 2; col <= 13; col++)
                {
                    worksheet.Cell(currentRow, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                currentRow++;
            }

            // Ajustar ancho de columnas
            worksheet.Columns().AdjustToContents();

            // Guardar en memoria
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            // Retornar el archivo como respuesta
            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Asistencias_Pivot.xlsx"
            );


        }

    }
}
