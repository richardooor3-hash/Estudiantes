using Dominio.Entidades;
using Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Persistencia.Models;

namespace Persistencia.Repositorio;

public class EstudianteRepositorio
(
    ApplicationDbContext context
): IEstudianteRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Estudiante?> GuardarEstudiante(Estudiante estudiante)
    {
        var dbModel = await _context.Estudiantes.FindAsync(estudiante.Id);

        if (dbModel == null)
        {
            dbModel = new EstudianteDbModel
            {
                id = estudiante.Id,
                nombre = estudiante.Nombre,
            };
            await _context.Estudiantes.AddAsync(dbModel);
        }
        else
        {
            dbModel.nombre = estudiante.Nombre;
            _context.Estudiantes.Update(dbModel);
        }
        await _context.SaveChangesAsync();
        return new Estudiante(dbModel.id,dbModel.nombre ?? string.Empty);
    }

    public async Task<List<Estudiante>> ListEstudiantes()
    {
        var estudiantesDb = await _context.Estudiantes.ToListAsync();

        var listaEstudiantes = estudiantesDb.Select(e => new Estudiante
        {
            Id = e.id,
            Nombre = e.nombre,
        }).ToList();

        return listaEstudiantes;
    }

    public async Task<Estudiante?> EstudianteId(int id)
    {
        EstudianteDbModel? estudiantesDb = await _context.Estudiantes.FindAsync(id);

        if (estudiantesDb == null)
        {
            return null;
        }

        var Estudiante = new Estudiante
        {
            Id = estudiantesDb.id,
            Nombre = estudiantesDb.nombre,
        };

        return Estudiante;
    }

    public async Task<Estudiante?> EliminarEstudiante(Estudiante estudiante)
    {
        var dbModel = await _context.Estudiantes.FindAsync(estudiante.Id);

        if (dbModel != null)
        {
            _context.Estudiantes.Remove(dbModel);
            await _context.SaveChangesAsync();
            return estudiante;
        }
        else 
        {
            return null;
        }
    }
}
