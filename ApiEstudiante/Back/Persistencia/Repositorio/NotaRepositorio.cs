using Dominio.Entidades;
using Dominio.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Persistencia.Models;

namespace Persistencia.Repositorio;

public class NotaRepositorio
(
    ApplicationDbContext context
) : INotaRepositorio
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Nota?> GuardarNota(Nota nota)
    {
        var dbModel = await _context.Nota.FindAsync(nota.Id);

        if (dbModel == null)
        {
            dbModel = new NotaDbModel
            {
                id = nota.Id,
                nombre = nota.Nombre,
                idProfesor = nota.IdProfesor,
                idEstudiante = nota.IdEstudiante,
                valor = nota.Valor
            };
            await _context.Nota.AddAsync(dbModel);
        }
        else
        {
            dbModel.nombre = nota.Nombre;
            _context.Nota.Update(dbModel);
        }
        await _context.SaveChangesAsync();
        return new Nota(dbModel.id, dbModel.nombre ?? string.Empty, dbModel.idProfesor, dbModel.idEstudiante, dbModel.valor);
    }

    public async Task<List<Nota>> ListNota()
    {
        var notaDb = await _context.Nota.ToListAsync();

        var listaNota = await _context.Nota
                       .Join(_context.Profesor,
                          n => n.idProfesor,
                          p => p.id,
                          (n, p) => new { n, p })
                        .Join(_context.Estudiantes,
                              np => np.n.idEstudiante,
                              e => e.id,
                              (np, e) => new Nota
                              {
                                  Id = np.n.id,
                                  Nombre = np.n.nombre,
                                  IdProfesor = np.n.idProfesor,
                                  NombreProfesor = np.p.nombre,
                                  IdEstudiante = np.n.idEstudiante,
                                  NombreEstudiante = e.nombre,
                                  Valor = np.n.valor
                              })
                        .ToListAsync();

        return listaNota;
    }

    public async Task<Nota?> NotaId(int id)
    {
        NotaDbModel? notaDb = await _context.Nota.FindAsync(id);

        if (notaDb == null)
        {
            return null;
        }

        var Estudiante = new Nota
        {
            Id = notaDb.id,
            Nombre = notaDb.nombre,
            IdProfesor = notaDb.idProfesor,
            IdEstudiante = notaDb.idEstudiante,
            Valor = notaDb.valor
        };

        return Estudiante;
    }

    public async Task<Nota?> EliminarNota(Nota nota)
    {
        var dbModel = await _context.Nota.FindAsync(nota.Id);

        if (dbModel != null)
        {
            _context.Nota.Remove(dbModel);
            await _context.SaveChangesAsync();
            return nota;
        }
        else
        {
            return null;
        }
    }

    public async Task<Boolean> ValidarEstudianteNota(int idEstudiante)
    {
        var dbModel = await _context.Nota.Where(ent => ent.idEstudiante == idEstudiante).ToListAsync();

        if (dbModel.Any())
            return true;
        

        return false;
    }

    public async Task<Boolean> ValidarProfesorNota(int idProfesor)
    {
        var dbModel = await _context.Nota.Where(ent => ent.idProfesor == idProfesor).ToListAsync();

        if (dbModel.Any())
            return true;


        return false;
    }
}
