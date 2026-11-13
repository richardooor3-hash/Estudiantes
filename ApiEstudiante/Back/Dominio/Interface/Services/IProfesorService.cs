using Dominio.Entidades;
using Dominio.Model;

namespace Dominio.Interface.Services;

public interface IProfesorService
{
    Task<Profesor?> GuardarNuevoProfesor(Profesor nuevoProfesor);
    Task<List<Profesor>> ConsultarAllProfesor();
    Task<Profesor?> ConsultarProfesorId(int id);
    Task<ResponseModel<Profesor?>> EliminarProfesor(Profesor nuevoProfesor);
}
