using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
using Newtonsoft.Json;

namespace Controladora
{
    public class CtrUsuario
    {
        DaoUsuario _dao = null;
        ClaseResultado<DtoUsuario> _resultado = null;
        ClaseResultado<Sesion> _resultadoSesion = null;
        ClaseResultado<SesionXUsuario> _resultadoSesionxUsu = null;
        string apiUrl = "https://api.apis.net.pe/v2/reniec/dni?numero=";
        string authToken = "apis-token-5900.yWdOuFOjkDEUSAO-YaR1IUQTnluDyGjf";
        public CtrUsuario()
        {
            _dao = new DaoUsuario();
            _resultado = new ClaseResultado<DtoUsuario>();
            _resultadoSesion = new ClaseResultado<Sesion>();
            _resultadoSesionxUsu = new ClaseResultado<SesionXUsuario>();
        }

        public DtoUsuario InicioSesion(DtoUsuario entidad)
        {
            try
            {
                return _dao.InicioSesion(entidad);
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public List<DtoUsuario> Usuario_Listar()
        {
            try
            {
                return _dao.Usuario_Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ClaseResultado<DtoUsuario> Usuario_Insertar_Actualizar(DtoUsuario _entidad)
        {
            try
            {
                _resultado = _dao.Usuario_Insertar_Actualizar(_entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultado;
        }

        public ClaseResultado<DtoUsuario> Usuario_Activar_Inactivar(DtoUsuario entidad)
        {
            try
            {
                _resultado = _dao.Usuario_Activar_Inactivar(entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultado;
        }


        public ClaseResultado<Sesion> Sesion_MNT(Sesion entidad)
        {
            try
            {
                _resultadoSesion = _dao.Sesion_MNT(entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultadoSesion;
        }

        public ClaseResultado<Sesion> Validar_Sesion(Sesion entidad)
        {
            try
            {
                _resultadoSesion = _dao.Validar_Sesion(entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultadoSesion;
        }

        public ClaseResultado<SesionXUsuario> Listar_Usuarios_Logueados()
        {
            try
            {
                _resultadoSesionxUsu = _dao.Listar_Usuarios_Logueados();
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultadoSesionxUsu;
        }


        public async Task<respuestaDNI> ConsultaDatosReniec(string numeroDocumento)
        {
            respuestaDNI entidad = new respuestaDNI();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Configura el encabezado "Authorization" con el token de tipo Bearer
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    // Realiza una solicitud GET al servicio
                    HttpResponseMessage response = await client.GetAsync(apiUrl+ numeroDocumento);
                    // Verifica si la solicitud fue exitosa (código de estado 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta como una cadena
                        string responseBody = await response.Content.ReadAsStringAsync();
                        entidad = JsonConvert.DeserializeObject<respuestaDNI>(responseBody);
                        Console.WriteLine("Respuesta del servicio:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return entidad;
        }
    }
    public class respuestaDNI
    {
        public string nombres { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;
        public int tipoDocumento { get; set; }
        public int numeroDocumento { get; set; }
    }
}
