using Dominio.Entidades;
using Dominio.Model;

namespace Dominio.Interface.Services;

public interface IEstudianteServices
{
    Task<Estudiante?> GuardarNuevoEstudiante(Estudiante nuevoEstudiante);

    Task<List<Estudiante>> ConsultarAllEstudiante();

    Task<Estudiante?> ConsultarEstudianteId(int id);

    Task<ResponseModel<Estudiante?>> EliminarEstudiante(Estudiante estudiante);
}
