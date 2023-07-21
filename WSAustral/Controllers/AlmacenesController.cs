using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.DataContracts;
using WSAustral.Core.Dapper.Repositories;
using WSAustral.Modelos;
using WSAustral.UnitOfWork;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;

namespace WSAustral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public AlmacenesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SubirArchivoRCNP")]
        [Authorize]

        public Respuesta SubirArchivoRCNP([FromBody] RptCargaNoPeligrosa objRptCargaNoPeligrosa)
        {
            Respuesta objRespuesta = new Respuesta();
            // set usuario
            var obj = new TBL_LogWS();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                obj.USUARIO = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            }
            else
            {
                obj.USUARIO = "";
            }

            // set fecha
            obj.FEC_REGIS = DateTime.Now;

            try
            {
                if (objRptCargaNoPeligrosa.C_PEPNO == string.Empty || objRptCargaNoPeligrosa.C_FILE64 == string.Empty)
                {
                    objRespuesta.C_STATUS = "202";
                    objRespuesta.C_MESSAGE = "Interrupción por error. Parámetros de entrada faltantes.";
                    //return objRespuesta;
                }
                else if (_unitOfWork.ReportesCNP.BuscarPEP(objRptCargaNoPeligrosa.C_PEPNO).Result == "NO EXISTE")
                {
                    objRespuesta.C_STATUS = "203";
                    objRespuesta.C_MESSAGE = "Interrupción por error. El número de PEP no existe";
                    //return objRespuesta;
                }
                else
                {
                    string path_RCNP = _unitOfWork._configuration["Configuraciones:Settings:RutaWebRCNP"];
                    string sufijo = objRptCargaNoPeligrosa.C_PEPNO + "_RCNP.pdf";

                    string path_escribir = path_RCNP + sufijo;

                    objRespuesta = SubirArchivo(objRptCargaNoPeligrosa.C_FILE64, path_escribir);
                }
            }
            catch (Exception e)
            {
                objRespuesta.C_STATUS = "299";
                objRespuesta.C_MESSAGE = "Interrupción por error: " + e.Message;
            }

            // set json request =  serializar RptCargaNoPeligrosa
            var jsonRequest = JsonConvert.SerializeObject(objRptCargaNoPeligrosa);
            obj.JSON_Request = jsonRequest;

            // set json response = serializar Respuesta
            var jsonResponse = JsonConvert.SerializeObject(objRespuesta);
            obj.JSON_Response = jsonResponse;

            // set endpoint
            obj.END_POINT = HttpContext.Request.GetDisplayUrl();
            
            // insert
            _unitOfWork.ReportesCNP.Agregar(obj);

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
                objRespuesta.C_STATUS = "299";
                objRespuesta.C_MESSAGE = "Interrupción por error: " + e.Message;
            }

            return objRespuesta;
        }
    }
}
