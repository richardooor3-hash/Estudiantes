using Dominio.Entidades;

namespace Dominio.Interface;

public interface IEstudianteRepository
{
    Task<Estudiante?> GuardarEstudiante(Estudiante estudiante);

    Task<List<Estudiante>> ListEstudiantes();

    Task<Estudiante?> EstudianteId(int id);

    Task <Estudiante?>EliminarEstudiante(Estudiante estudiante);
}
