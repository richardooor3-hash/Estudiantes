using Aplicación.Dtos;
using Aplicación.Dtos.In;
using Aplicación.Dtos.Out;
using Aplicación.Services;
using Dominio.Constantes;
using Dominio.Entidades;
using Dominio.Interface.Services;
using Dominio.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiEstudiante.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudianteController(IEstudianteServices estudianteServices, ILogger<EstudianteController> logger) : ControllerBase
{
    private readonly IEstudianteServices _estudianteServices= estudianteServices;
    private readonly ILogger<EstudianteController> _logger = logger;

    [HttpPost("RegistrarEstudiante")]
    public async Task<ActionResult<ResponseModel<ConsultaEstudianteResponse>>> RegistrarEstudiante([FromBody] RegistrarEstudianteDto request)
    {
        try
        {
            var estudiante = new Estudiante(0,request.Nombre);
            var estudianteResponse = await _estudianteServices.GuardarNuevoEstudiante(estudiante);

            if (estudianteResponse != null) 
                return Ok(ResponseHelper.Exito(estudianteResponse,HttpStatusCode.OK.ToString(), MensajesHelper.CreadoOk("estudiante"),(int)HttpStatusCode.OK));
            else
                return BadRequest(ResponseHelper.Error<ConsultaEstudianteResponse>(HttpStatusCode.BadRequest.ToString(), MensajesHelper.ErrorGuardado("estudiante"), (int)HttpStatusCode.BadRequest));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado RegistrarEstudiante");
            return BadRequest(ResponseHelper.Error<ConsultaEstudianteResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorGuardado("estudiante"), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet("ConsultarEstudiantes")]
    public async Task<ActionResult<List<ConsultaEstudianteResponse>>> ConsultarEstudiantes()
    {
        try
        {
            var estudiantes = await _estudianteServices.ConsultarAllEstudiante();

            if (estudiantes is null)
            {
                return NotFound((ResponseHelper.Error<ConsultaEstudianteResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorConsulta("estudiante"), (int)HttpStatusCode.NotFound)));
            }

            var consultaEstudianteResponse = estudiantes
               .Select(e => new ConsultaEstudianteResponse(e.Id, e.Nombre ?? string.Empty))
               .ToList();
 
            return Ok(ResponseHelper.Exito(consultaEstudianteResponse,HttpStatusCode.OK.ToString(),string.Empty,(int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ConsultarEstudiantes");
            return BadRequest(ResponseHelper.Error<ConsultaEstudianteResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorConsulta("estudiante"), (int)HttpStatusCode.InternalServerError));
        }
    }


    [HttpGet("ConsultarEstudianteForId/{id:int}")]
    public async Task<ActionResult<ConsultaEstudianteResponse>> ConsultarEstudianteForId(int id)
    {
        try
        {
            var estudiante = await _estudianteServices.ConsultarEstudianteId(id);

            if (estudiante is null)
            {
                return NotFound((ResponseHelper.Error<DataModelResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorConsulta("estudiante"), (int)HttpStatusCode.NotFound)));
            }

            var consultaEstudianteResponse = new ConsultaEstudianteResponse(estudiante.Id, estudiante.Nombre ?? string.Empty);
            return Ok(ResponseHelper.Exito(consultaEstudianteResponse, HttpStatusCode.OK.ToString(),string.Empty,(int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ConsultarEstudianteForId {id}", id);
            return BadRequest(ResponseHelper.Error<ConsultaEstudianteResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorConsulta("estudiante"), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpPut("ActualizarEstudiante")]
    public async Task<IActionResult> ActualizarEstudiante([FromBody] ActualizarEstudianteRequest request)
    {
        try
        {
            var estudiante = new Estudiante(request.Id, request.Nombre);
            var estudianteResponse = await _estudianteServices.GuardarNuevoEstudiante(estudiante);
            if (estudianteResponse != null)
                return Ok(ResponseHelper.Exito(estudianteResponse, HttpStatusCode.OK.ToString(),MensajesHelper.ActualizadoOk("estudiante"),(int)HttpStatusCode.OK));
            else
                return BadRequest(ResponseHelper.Error<ConsultaEstudianteResponse>(HttpStatusCode.BadRequest.ToString(), MensajesHelper.ErrorActualizar("estudiante"), (int)HttpStatusCode.BadRequest));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ActualizarEstudiante");
            return BadRequest(ResponseHelper.Error<DataModelResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorActualizar("estudiante"), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpDelete("EliminarEstudiante/{id:int}")]
    public async Task<ActionResult<ResponseModel<ConsultaEstudianteResponse>>>EliminarEstudiante(int id)
    {
        try
        {
            var estudiante = new Estudiante(id,string.Empty);
            var estudianteMod = await _estudianteServices.EliminarEstudiante(estudiante);

            if (estudianteMod.Status == (int)HttpStatusCode.OK)
                return Ok(estudianteMod);
            else
                return NotFound((ResponseHelper.Error<DataModelResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorEliminar("estudiante"), (int)HttpStatusCode.NotFound)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado EliminarProfesor {id}", id);
            return BadRequest(ResponseHelper.Error<DataModelResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorEliminar("estudiante"), (int)HttpStatusCode.InternalServerError));
        }
    }

}
