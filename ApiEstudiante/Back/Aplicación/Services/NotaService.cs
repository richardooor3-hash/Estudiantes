using Dominio.Constantes;
using Dominio.Entidades;
using Dominio.Interface;
using Dominio.Interface.Repository;
using Dominio.Interface.Services;
using Dominio.Model;
using System.Net;
using Aplicación.Dtos.Out;

namespace Aplicación.Services;

public class NotaService(INotaRepositorio notaRepositorio,IProfesorRepositorio profesorRepositorio,IEstudianteRepository estudianteRepository) : INotaService
{
    private readonly INotaRepositorio _notaRepositorio = notaRepositorio;
    private readonly IProfesorRepositorio _profesorRepositorio = profesorRepositorio;
    private readonly IEstudianteRepository _estudianteRepository = estudianteRepository;

    public async Task<ResponseModel<Nota>> GuardarNuevaNota(Nota nuevoNota)
    {
        bool ExisteProfesor = await ValidarProfesor(nuevoNota.IdProfesor);

        if (!ExisteProfesor)
        {
            var response = AsignarResponse((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), ConstantesMsj.MsjProfesorNoExiste, null);
            return response;
        }
           
        bool ExisteEstudiante = await ValidarEstudiante(nuevoNota.IdEstudiante);

        if (!ExisteEstudiante)
        {
            var response = AsignarResponse((int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), ConstantesMsj.MsjProfesorNoExiste, null);
            return response;
        }
        Nota? data = await _notaRepositorio.GuardarNota(nuevoNota);

        if(data is not null)
            return ResponseHelper.Exito(data, HttpStatusCode.OK.ToString(), MensajesHelper.CreadoOk("nota", true), (int)HttpStatusCode.OK);
        else
            return ResponseHelper.Error<Nota>(HttpStatusCode.InternalServerError.ToString(), MensajesHelper.ErrorGuardado("nota", true), (int)HttpStatusCode.InternalServerError);
    }

    public async Task<List<Nota>> ConsultarAllNota()
    {
        return await _notaRepositorio.ListNota();
    }

    public async Task<Nota?> ConsultarNotaId(int id)
    {
        return await _notaRepositorio.NotaId(id);
    }

    public async Task<Nota?> EliminarNota(Nota nuevoNota)
    {
        return await _notaRepositorio.EliminarNota(nuevoNota);
    }

    private async Task<bool> ValidarProfesor(int id)
    {
        var profesorData = await _profesorRepositorio.profesorId(id);
        return profesorData is not null;
    }

    private async Task<bool> ValidarEstudiante(int id)
    {
        var profesorData = await _estudianteRepository.EstudianteId(id);
        return profesorData is not null;
    }

    private ResponseModel<Nota> AsignarResponse(int status, string desc, string msj, Nota? nota)
    {
        var response = new ResponseModel<Nota>()
        {
            Status = status,
            Descripcion = desc,
            Mensaje = msj,
            Model = nota
        };
        return response;
    }


}
