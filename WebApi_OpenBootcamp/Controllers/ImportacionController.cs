using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using DTO;
using DocumentFormat.OpenXml.Math;
using Controladora;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImportacionController : ControllerBase
    {
        CtrImportacion ctrImportacion = null;
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

            var dataList = leerExcelAsistencia(filePath);



            return Ok("Data inserted successfully.");
        }

        private List<DtoImportacionAsitencia> leerExcelAsistencia(string filePath)
        {
            var dataList = new List<DtoImportacionAsitencia>();

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
                }
            }

            return dataList;
        }

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



        private const int BatchSize = 5000;

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

            foreach (var batch in leerExcelVisitanteEnLotes(filePath))
            {
                cargarEstudiante(batch);
            }

            return Ok("Data inserted successfully.");
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
                        codigo = row.Cell(1).GetValue<string>(),
                        nombres = row.Cell(2).GetValue<string>(),
                        apellidoPaterno = devolverApellido("AP", row.Cell(3).GetValue<string>()),
                        apellidoMaterno = devolverApellido("AM", row.Cell(3).GetValue<string>())
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
            if (string.IsNullOrWhiteSpace(apellidoFull))
                return string.Empty;

            var apellidos = apellidoFull.Split(' ');

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




        private void cargarEstudiante(List<DtoImportacionVisitante> lista)
        {
            ctrImportacion.InsertDataIntoDatabase(lista);
        }

    }


}
