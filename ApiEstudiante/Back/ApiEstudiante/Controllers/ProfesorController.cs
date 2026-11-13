using Aplicación.Dtos.In;
using Aplicación.Dtos.Out;
using Aplicación.Services;
using Dominio.Constantes;
using Dominio.Entidades;
using Dominio.Interface.Services;
using Dominio.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApiProfesor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesorController(IProfesorService profesorService, ILogger<ProfesorController> logger) : ControllerBase
{
    private readonly IProfesorService _profesorService = profesorService;
    private readonly ILogger<ProfesorController> _logger = logger;


    [HttpPost("RegistrarProfesor")]
    public async Task<ActionResult<ResponseModel<ConsultaProfesorResponse>>> RegistrarProfesor([FromBody] RegistrarProfesorRequest request)
    {
        try
        {
            var Profesor = new Profesor(0, request.Nombre);
            var ProfesorResponse = await _profesorService.GuardarNuevoProfesor(Profesor);

            if (ProfesorResponse != null)
                return Ok(ResponseHelper.Exito(ProfesorResponse, HttpStatusCode.OK.ToString(), MensajesHelper.CreadoOk("profesor"), (int)HttpStatusCode.OK));
            else
                return BadRequest(ResponseHelper.Error<ConsultaProfesorResponse>(HttpStatusCode.BadRequest.ToString(), MensajesHelper.CreadoOk("profesor"), (int)HttpStatusCode.BadRequest));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado RegistrarProfesor");
            return BadRequest(ResponseHelper.Error<ConsultaProfesorResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.CreadoOk("profesor"), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet("ConsultarProfesores")]
    public async Task<ActionResult<List<ConsultaProfesorResponse>>> ConsultarProfesores()
    {
        try
        {
            var Profesors = await _profesorService.ConsultarAllProfesor();

            if (Profesors is null)
            {
                return NotFound((ResponseHelper.Error<ConsultaProfesorResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorConsulta("profesores"), (int)HttpStatusCode.NotFound)));
            }

            var consultaProfesorResponse = Profesors
               .Select(e => new ConsultaProfesorResponse(e.Id, e.Nombre ?? string.Empty))
               .ToList();

            return Ok(ResponseHelper.Exito(consultaProfesorResponse, HttpStatusCode.OK.ToString(), string.Empty, (int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ConsultarProfesores");
            return BadRequest(ResponseHelper.Error<ConsultaProfesorResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorConsulta("profesores"), (int)HttpStatusCode.InternalServerError));
        }
    }


    [HttpGet("ConsultarProfesorForId/{id:int}")]
    public async Task<ActionResult<ConsultaProfesorResponse>> ConsultarProfesorForId(int id)
    {
        try
        {
            var Profesor = await _profesorService.ConsultarProfesorId(id);

            if (Profesor is null)
            {
                return NotFound((ResponseHelper.Error<DataModelResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorConsulta("profesor"), (int)HttpStatusCode.NotFound)));
            }

            var consultaProfesorResponse = new ConsultaProfesorResponse(Profesor.Id, Profesor.Nombre ?? string.Empty);
            return Ok(ResponseHelper.Exito(consultaProfesorResponse, HttpStatusCode.OK.ToString(), string.Empty, (int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ConsultarProfesorForId {Id}",id);
            return BadRequest(ResponseHelper.Error<ConsultaProfesorResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorConsulta("profesor"), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpPut("ActualizarProfesor")]
    public async Task<IActionResult> ActualizarProfesor([FromBody] ActualizarProfesorRequest request)
    {
        try
        {
            var Profesor = new Profesor(request.Id, request.Nombre);
            var ProfesorResponse = await _profesorService.GuardarNuevoProfesor(Profesor);
            if (ProfesorResponse != null)
                return Ok(ResponseHelper.Exito(ProfesorResponse, HttpStatusCode.OK.ToString(), MensajesHelper.ActualizadoOk( "profesor"), (int)HttpStatusCode.OK));
            else
                return BadRequest(ResponseHelper.Error<ConsultaProfesorResponse>(HttpStatusCode.BadRequest.ToString(), MensajesHelper.ErrorActualizar("profesor"), (int)HttpStatusCode.BadRequest));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ActualizarProfesor");
            return BadRequest(ResponseHelper.Error<DataModelResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorActualizar("profesor"), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpDelete("EliminarProfesor/{id:int}")]
    public async Task<ActionResult<ResponseModel<ConsultaProfesorResponse>>> EliminarProfesor(int id)
    {
        try
        {
            var Profesor = new Profesor(id, string.Empty);
            var ProfesorMod = await _profesorService.EliminarProfesor(Profesor);

            if (ProfesorMod.Status == (int)HttpStatusCode.OK)
                return Ok(ProfesorMod);
            else
                return NotFound((ResponseHelper.Error<DataModelResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorEliminar("profesor"), (int)HttpStatusCode.NotFound)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado EliminarProfesor {id}",id);
            return BadRequest(ResponseHelper.Error<DataModelResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorEliminar("profesor"), (int)HttpStatusCode.InternalServerError));
        }
    }
}
