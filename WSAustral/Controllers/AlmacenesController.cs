using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WSAustral.Core.Dapper.Repositories;
using WSAustral.Modelos;
using WSAustral.UnitOfWork;

namespace WSAustral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
/*        private readonly RepositoryRCNP repositoryRCNP;*/

        public AlmacenesController(IUnitOfWork unitOfWork)
        {
            //repositoryRCNP = new RepositoryRCNP("Server=10.5.0.52;Initial Catalog=Almacenes;Persist Security Info=False;User ID=UsrPortales;Password=$#ewo2001.2d;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public Respuesta SubirArchivoRCNP([FromBody] RptCargaNoPeligrosa objRptCargaNoPeligrosa)
        {
            Respuesta objRespuesta = new Respuesta();

            try
            {
                if (objRptCargaNoPeligrosa.C_PEPNO == string.Empty || objRptCargaNoPeligrosa.C_FILE64 == string.Empty)
                {
                    objRespuesta.C_STATUS = "202";
                    objRespuesta.C_MESSAGE = "Interrupción por error. Parámetros de entrada faltantes.";
                    return objRespuesta;
                }
                if (_unitOfWork.ReportesCNP.BuscarPEP(objRptCargaNoPeligrosa.C_PEPNO).Result == "NO EXISTE")
                {
                    objRespuesta.C_STATUS = "203";
                    objRespuesta.C_MESSAGE = "Interrupción por error. El número de PEP no existe";
                    return objRespuesta;
                }

                string path_RCNP = _unitOfWork._configuration["Configuraciones:Settings:RutaWebRCNP"];
                string sufijo = objRptCargaNoPeligrosa.C_PEPNO + "_RCNP.pdf";

                string path_escribir = path_RCNP + sufijo;

                objRespuesta = SubirArchivo(objRptCargaNoPeligrosa.C_FILE64, path_escribir);
            }
            catch (Exception e)
            {
                objRespuesta.C_STATUS = "2991";
                objRespuesta.C_MESSAGE = "Interrupción por error: " + e.Message;
            }

            return objRespuesta;
        }
        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public Respuesta SubirArchivo(string archivo, string path_escribir)
        {
            Respuesta objRespuesta = new Respuesta();

            try
            {
                if (IsBase64String(archivo))
                {

                    if (!System.IO.File.Exists(path_escribir))
                    {
                        objRespuesta.C_STATUS = "101";
                        objRespuesta.C_MESSAGE = "Finalizado sin errores. PDF subido.";
                    }
                    else
                    {
                        objRespuesta.C_STATUS = "102";
                        objRespuesta.C_MESSAGE = "Finalizado sin errores. PDF actualizado.";
                    }

                    var obje = new LogRCNP();
                    obje.USUARIO = "SGS";
                    obje.TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
                    obje.JSON_Request = "{ \"C_PEPNO\" : \"6110002978\", \"C_FILE64\" : \"SADASDASD \" }";
                    obje.JSON_Response = "{ \"c_STATUS\" : \"102\", \"c_MESSAGE\" : \"Finalizado sin errores. PDF actualizado.\" }";
                    obje.FEC_REGIS = DateTime.Now;

                    _unitOfWork.ReportesCNP.Agregar(obje);

                    var dataByteArray = Convert.FromBase64String(archivo);
                    System.IO.File.WriteAllBytes(path_escribir, dataByteArray);
                }
                else
                {
                    objRespuesta.C_STATUS = "201";
                    objRespuesta.C_MESSAGE = "Interrupción por error. Formato base64 no es correcto.";
                }
            }
            catch (Exception e)
            {
                objRespuesta.C_STATUS = "2992";
                objRespuesta.C_MESSAGE = "Interrupción por error: " + e.Message;
            }

            return objRespuesta;
        }
    }
}
