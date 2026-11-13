using Aplicación.Dtos.In;
using Aplicación.Dtos.Out;
using Dominio.Constantes;
using Dominio.Entidades;
using Dominio.Model;
using Dominio.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiEstudiante.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotaController(INotaService notaService, ILogger<NotaController> logger) : ControllerBase
{
    private readonly INotaService _notaService = notaService;
    private readonly ILogger<NotaController> _logger = logger;


    [HttpPost("RegistrarNota")]
    public async Task<ActionResult<ResponseModel<ConsultaNotaResponse>>> RegistrarNota([FromBody] RegistrarNotaRequest request)
    {
        try
        {
            var Nota = new Nota(0, request.Nombre,request.IdProfesor,request.IdEstudiante,request.Valor);
            var NotaResponse = await _notaService.GuardarNuevaNota(Nota);

            if (NotaResponse.Status == (int)HttpStatusCode.OK)
                return Ok(NotaResponse);
            else
                return BadRequest(ResponseHelper.Error<ConsultaNotaResponse>(HttpStatusCode.BadRequest.ToString(), $" {NotaResponse.Mensaje} {MensajesHelper.ErrorGuardado("nota", true)}", (int)HttpStatusCode.BadRequest));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado RegistrarEstudiante");
            return BadRequest(ResponseHelper.Error<ConsultaNotaResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorGuardado("nota", true), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet("ConsultarNotas")]
    public async Task<ActionResult<List<ConsultaNotaResponse>>> ConsultarNotas()
    {
        try
        {
            var Notas = await _notaService.ConsultarAllNota();

            if (Notas is null)
            {
                return NotFound((ResponseHelper.Error<ConsultaNotaResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorConsulta("notas", true), (int)HttpStatusCode.NotFound)));
            }

            var consultaNotaResponse = Notas
               .Select(e => new ConsultaNotaResponse(e.Id, e.Nombre ?? string.Empty,e.IdProfesor,e.NombreProfesor!, e.IdEstudiante,e.NombreEstudiante!,e.Valor))
               .ToList();

            return Ok(ResponseHelper.Exito(consultaNotaResponse, HttpStatusCode.OK.ToString(), string.Empty, (int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ConsultarNotas");
            return BadRequest(ResponseHelper.Error<ConsultaNotaResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorConsulta("notas", true), (int)HttpStatusCode.InternalServerError));
        }
    }


    [HttpGet("ConsultarNotaForId/{id:int}")]
    public async Task<ActionResult<ConsultaNotaResponse>> ConsultarNotaForId(int id)
    {
        try
        {
            var Nota = await _notaService.ConsultarNotaId(id);

            if (Nota is null)
            {
                return NotFound((ResponseHelper.Error<DataModelResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorConsulta("nota", true), (int)HttpStatusCode.NotFound)));
            }

            var consultaNotaResponse = new ConsultaNotaResponse(Nota.Id, Nota.Nombre!, Nota.IdProfesor,Nota.NombreProfesor!,Nota.IdEstudiante,Nota.NombreEstudiante!,Nota.Valor);
            return Ok(ResponseHelper.Exito(consultaNotaResponse, HttpStatusCode.OK.ToString(), string.Empty, (int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ConsultarNotaForId {id}",id);
            return BadRequest(ResponseHelper.Error<ConsultaNotaResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorConsulta("nota", true), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpPut("ActualizarNota")]
    public async Task<IActionResult> ActualizarNota([FromBody] ActualizarNotaRequest request)
    {
        try
        {
            var Nota = new Nota(request.Id, request.Nombre,request.IdProfesor,request.IdEstudiante,request.Valor);
            var NotaResponse = await _notaService.GuardarNuevaNota(Nota);
            if (NotaResponse != null)
                return Ok(ResponseHelper.Exito(NotaResponse, HttpStatusCode.OK.ToString(), MensajesHelper.ActualizadoOk("nota", true), (int)HttpStatusCode.OK));
            else
            {
                _logger.LogError(MensajesHelper.ActualizadoOk("nota", true));
                return BadRequest(ResponseHelper.Error<ConsultaNotaResponse>(HttpStatusCode.BadRequest.ToString(), MensajesHelper.ErrorActualizar("nota", true), (int)HttpStatusCode.BadRequest));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado ActualizarNota");
            return BadRequest(ResponseHelper.Error<DataModelResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorActualizar("nota", true), (int)HttpStatusCode.InternalServerError));
        }
    }

    [HttpDelete("EliminarNota/{id:int}")]
    public async Task<ActionResult<ResponseModel<ConsultaNotaResponse>>> EliminarNota(int id)
    {
        try
        {
            var Nota = new Nota(id, string.Empty,0,0,0);
            var NotaMod = await _notaService.EliminarNota(Nota);

            if (NotaMod is null)
            {
                _logger.LogError(MensajesHelper.ErrorEliminar("nota", true));
                return NotFound((ResponseHelper.Error<DataModelResponse>(HttpStatusCode.NotFound.ToString(), MensajesHelper.ErrorEliminar("nota", true), (int)HttpStatusCode.NotFound)));
            }
            return Ok(ResponseHelper.Exito(NotaMod, HttpStatusCode.OK.ToString(), MensajesHelper.EliminadoOk("nota", true), (int)HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocurrió un error inesperado EliminarNota {id}",id);
            return BadRequest(ResponseHelper.Error<DataModelResponse>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorEliminar("nota", true), (int)HttpStatusCode.InternalServerError));
        }
    }
}
