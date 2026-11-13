using Dominio.Entidades;

namespace Dominio.Interface.Repository;

public interface INotaRepositorio
{
    Task<Nota?> GuardarNota(Nota nota);
    Task<List<Nota>> ListNota();
    Task<Nota?> NotaId(int id);
    Task<Nota?> EliminarNota(Nota nota);
    Task<Boolean> ValidarEstudianteNota(int idEstudiante);
    Task<Boolean> ValidarProfesorNota(int idProfesor);
}
