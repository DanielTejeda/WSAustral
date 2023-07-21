using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSAustral.Modelos;
using WSAustral.UnitOfWork;
using IronPython.Hosting;
using IronPython.Runtime;
using IronPython;
using IronPython.Modules;
using Microsoft.Scripting.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;

namespace WSAustral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportefaenasycalasController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public ReportefaenasycalasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("EjecutarPy")]
        [Authorize]
        public Respuesta EjecutarPythonScript()
        {
            Respuesta objRespuesta = new Respuesta();
            try
            {
                string ironPythonPath = @"Scripts\ipython.exe";

                string pythonScript = @"Scripts\ReporteFaenasyCalasScript.py";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ironPythonPath,
                    Arguments = pythonScript,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };


                using (Process process = Process.Start(startInfo))
                {
                    // Leer la salida estándar del proceso
                    string output = process.StandardOutput.ReadToEnd();
                    Console.WriteLine(output);
                    objRespuesta.C_MESSAGE = "EXITO!";

                    process.WaitForExit();
                }

                objRespuesta.C_STATUS = "101";

            }
            catch(Exception e)
            {
                objRespuesta.C_STATUS = "299";
                objRespuesta.C_MESSAGE = "Interrupción por error: " + e.Message;
            }

            return objRespuesta;
        }


    }
}
