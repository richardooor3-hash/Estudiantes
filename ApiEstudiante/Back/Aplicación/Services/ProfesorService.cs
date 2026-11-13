using Aplicación.Dtos.Out;
using Dominio.Constantes;
using Dominio.Entidades;
using Dominio.Interface;
using Dominio.Interface.Repository;
using Dominio.Interface.Services;
using Dominio.Model;
using System.Net;

namespace Aplicación.Services;

public class ProfesorService(IProfesorRepositorio profesorRepositorio, INotaRepositorio notaRepositorio) : IProfesorService
{
    private readonly IProfesorRepositorio _profesorRepositorio = profesorRepositorio;
    private readonly INotaRepositorio _notaRepositorio = notaRepositorio;

    public async Task<Profesor?> GuardarNuevoProfesor(Profesor nuevoProfesor)
    {
        return await _profesorRepositorio.GuardarProfesor(nuevoProfesor);
    }

    public async Task<List<Profesor>> ConsultarAllProfesor()
    {
        return await _profesorRepositorio.ListProfesor();
    }

    public async Task<Profesor?> ConsultarProfesorId(int id)
    {
        return await _profesorRepositorio.profesorId(id);
    }

    public async Task<ResponseModel<Profesor?>> EliminarProfesor(Profesor nuevoProfesor)
    {
        var profesorAsociado = await _notaRepositorio.ValidarProfesorNota(nuevoProfesor.Id);

        if (profesorAsociado)
        {
            return ResponseHelper.Error<Profesor?>(HttpStatusCode.NotFound.ToString(), ConstantesMsj.MsjProfesorExisteNota, (int)HttpStatusCode.NotFound);
        }
        var data = await _profesorRepositorio.Eliminarprofesor(nuevoProfesor);
        return ResponseHelper.Exito(data, HttpStatusCode.OK.ToString(), MensajesHelper.EliminadoOk("profesor"), (int)HttpStatusCode.OK);
    }
}
