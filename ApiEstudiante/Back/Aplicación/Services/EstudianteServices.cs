using Aplicación.Dtos.Out;
using Dominio.Constantes;
using Dominio.Entidades;
using Dominio.Interface;
using Dominio.Interface.Repository;
using Dominio.Interface.Services;
using Dominio.Model;
using System.Net;

namespace Aplicación.Services;

public class EstudianteServices(IEstudianteRepository estudianteRepository,INotaRepositorio notaRepositorio) : IEstudianteServices
{
    private readonly IEstudianteRepository _estudianteRepository = estudianteRepository;
    private readonly INotaRepositorio _notaRepositorio = notaRepositorio;

    public async Task<Estudiante?> GuardarNuevoEstudiante(Estudiante nuevoEstudiante )
    {
        return await _estudianteRepository.GuardarEstudiante(nuevoEstudiante);
    }

    public async Task<List<Estudiante>>ConsultarAllEstudiante()
    {
        return await _estudianteRepository.ListEstudiantes();
    }
    
    public async Task<Estudiante?>ConsultarEstudianteId(int id)
    {
        return await _estudianteRepository.EstudianteId(id); 
    }

    public async Task<ResponseModel<Estudiante?>> EliminarEstudiante(Estudiante estudiante)
    {
        var EstudianteAsociado = await _notaRepositorio.ValidarProfesorNota(estudiante.Id);
        if (EstudianteAsociado)
        {
            return ResponseHelper.Error<Estudiante?>(HttpStatusCode.NotFound.ToString(), ConstantesMsj.MsjEstudianteExisteNota, (int)HttpStatusCode.NotFound);   
        }
        var data = await _estudianteRepository.EliminarEstudiante(estudiante);
        return ResponseHelper.Exito(data, HttpStatusCode.OK.ToString(), MensajesHelper.EliminadoOk("estudiante"), (int)HttpStatusCode.OK);
    }

}
