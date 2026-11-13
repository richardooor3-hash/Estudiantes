

using Dominio.Entidades;

namespace Dominio.Interface.Repository;

public interface IProfesorRepositorio
{
    Task<Profesor?> GuardarProfesor(Profesor profesor);
    Task<List<Profesor>> ListProfesor();
    Task<Profesor?> profesorId(int id);
    Task<Profesor?> Eliminarprofesor(Profesor profesor);
}
