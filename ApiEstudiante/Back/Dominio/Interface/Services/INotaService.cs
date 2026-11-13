using Dominio.Entidades;
using Dominio.Model;

namespace Dominio.Interface.Services;

public interface INotaService
{
    Task<ResponseModel<Nota>> GuardarNuevaNota(Nota nuevoNota);
    Task<List<Nota>> ConsultarAllNota();
    Task<Nota?> ConsultarNotaId(int id);
    Task<Nota?> EliminarNota(Nota nuevoNota);
}
