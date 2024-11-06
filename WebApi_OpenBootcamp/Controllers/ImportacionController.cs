using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using DTO;
using DocumentFormat.OpenXml.Math;
using Controladora;
using System.Text.RegularExpressions;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportacionController : ControllerBase
    {
        CtrImportacion ctrImportacion = null;
        private const int BatchSize = 5000;
        public ImportacionController()
        {
            ctrImportacion = new CtrImportacion();
        }

        [HttpPost]
        public IActionResult cargarAsistencia(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }


            bool success = true; // Inicialmente asumimos que el proceso es exitoso
            foreach (var batch in leerExcelAsistencia(filePath))
            {
                if (!cargarAsistencia(batch))
                {
                    success = false; // Si alguna inserción falla, cambiamos a false
                    break; // Puedes optar por continuar o detener el proceso según tu caso
                }
            }

            if (success)
                return Ok("Data inserted successfully.");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while inserting data.");
        }

        private bool cargarAsistencia(List<DtoImportacionAsitencia> lista)
        {
            return ctrImportacion.InsertDataIntoDatabase2(lista);
        }


        private IEnumerable<List<DtoImportacionAsitencia>> leerExcelAsistencia(string filePath)
        {
            var dataList = new List<DtoImportacionAsitencia>(BatchSize);

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Lee la primera hoja de Excel
                foreach (var row in worksheet.RowsUsed().Skip(1)) // Omite la primera fila si es encabezado
                {
                    var data = new DtoImportacionAsitencia
                    {
                        codigo = row.Cell(1).GetValue<string>(),
                        fecha = row.Cell(2).GetValue<string>()
                    };
                    dataList.Add(data);

                    if (dataList.Count >= BatchSize)
                    {
                        yield return dataList;
                        dataList.Clear();
                    }
                }

                if (dataList.Any())
                {
                    yield return dataList;
                }
            }
        }


        //private List<DtoImportacionAsitencia> leerExcelAsistencia(string filePath)
        //{
        //    var dataList = new List<DtoImportacionAsitencia>();

        //    using (var workbook = new XLWorkbook(filePath))
        //    {
        //        var worksheet = workbook.Worksheet(1); // Lee la primera hoja de Excel
        //        foreach (var row in worksheet.RowsUsed().Skip(1)) // Omite la primera fila si es encabezado
        //        {
        //            var data = new DtoImportacionAsitencia
        //            {
        //                codigo = row.Cell(1).GetValue<string>(),
        //                fecha = row.Cell(2).GetValue<string>()
        //            };
        //            dataList.Add(data);
        //        }
        //    }

        //    return dataList;
        //}

        //[HttpPost]
        //public IActionResult cargarEstudiante(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("Please upload a valid Excel file.");

        //    var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }

        //    var dataList = leerExcelVisitante(filePath);
        //    cargarEstudiante(dataList);
        //    return Ok("Data inserted successfully.");
        //}


        //private List<DtoImportacionVisitante> leerExcelVisitante(string filePath)
        //{
        //    var dataList = new List<DtoImportacionVisitante>();

        //    using (var workbook = new XLWorkbook(filePath))
        //    {
        //        var worksheet = workbook.Worksheet(1); // Lee la primera hoja de Excel
        //        foreach (var row in worksheet.RowsUsed().Skip(1)) // Omite la primera fila si es encabezado
        //        {
        //            var data = new DtoImportacionVisitante
        //            {
        //                codigo = row.Cell(1).GetValue<string>(),
        //                nombres = row.Cell(2).GetValue<string>(),
        //                apellidoPaterno = devolverApellido("AP", row.Cell(3).GetValue<string>()),
        //                apellidoMaterno = devolverApellido("AM", row.Cell(3).GetValue<string>())
        //            };
        //            dataList.Add(data);
        //        }
        //    }
        //    return dataList;
        //}

        [HttpPost]
        public IActionResult cargarEstudiante(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }


            bool success = true; // Inicialmente asumimos que el proceso es exitoso
            foreach (var batch in leerExcelVisitanteEnLotes(filePath))
            {
                if (!cargarEstudiante(batch))
                {
                    success = false; // Si alguna inserción falla, cambiamos a false
                    break; // Puedes optar por continuar o detener el proceso según tu caso
                }
            }

            if (success)
                return Ok("Data inserted successfully.");
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while inserting data.");
        }

        private IEnumerable<List<DtoImportacionVisitante>> leerExcelVisitanteEnLotes(string filePath)
        {
            var dataList = new List<DtoImportacionVisitante>(BatchSize);

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Lee la primera hoja de Excel
                foreach (var row in worksheet.RowsUsed().Skip(1)) // Omite la primera fila si es encabezado
                {
                    var data = new DtoImportacionVisitante
                    {
                        codigo = row.Cell(1).GetValue<string>().Trim(),
                        nombres = row.Cell(2).GetValue<string>().Trim(),
                        apellidoPaterno = devolverApellido("AP", row.Cell(3).GetValue<string>().Trim()),
                        apellidoMaterno = devolverApellido("AM", row.Cell(3).GetValue<string>().Trim()),
                        especialidad = row.Cell(4).GetValue<string>().Trim()
                    };
                    dataList.Add(data);

                    if (dataList.Count >= BatchSize)
                    {
                        yield return dataList;
                        dataList.Clear();
                    }
                }

                if (dataList.Any())
                {
                    yield return dataList;
                }
            }
        }
        private string devolverApellido(string tipoApe, string apellidoFull)
        {
            string apellidoLimpio = string.Empty;

            if (string.IsNullOrWhiteSpace(apellidoFull))
                return string.Empty;

            //limpiando apellido
            apellidoLimpio = quitarEspaciosApellido(apellidoFull);

            var apellidos = apellidoLimpio.Trim().Split(' ');

            // Verificamos que haya al menos dos apellidos
            if (apellidos.Length < 2)
                return string.Empty;

            return tipoApe switch
            {
                "AP" => apellidos[0],
                "AM" => apellidos[1],
                _ => string.Empty,
            };
        }

        private string quitarEspaciosApellido(string apellido)
        {
            string resultado = string.Empty;
            resultado = Regex.Replace(apellido, @"\s+", " ");
            return resultado;
        }



        private bool cargarEstudiante(List<DtoImportacionVisitante> lista)
        {
            return ctrImportacion.InsertDataIntoDatabase(lista);
        }



        #region RESPUESTA DE LA IMPORTACION DE VISITANTES Y ASISTENCIA
        [HttpGet]
        public List<DtoRespuestaImportacion> listarRespuestaImportacion()
        {
            List<DtoRespuestaImportacion> response = new List<DtoRespuestaImportacion>();
            try
            {
                response = ctrImportacion.listarRespuestaImportacion();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return response;
        }

        #endregion


    }


}
