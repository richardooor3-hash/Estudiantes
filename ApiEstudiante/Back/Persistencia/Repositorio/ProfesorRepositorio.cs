using Dominio.Entidades;
using Dominio.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Persistencia.Models;

namespace Persistencia.Repositorio;

public class ProfesorRepositorio(ApplicationDbContext context): IProfesorRepositorio
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Profesor?> GuardarProfesor(Profesor profesor)
    {
        var dbModel = await _context.Profesor.FindAsync(profesor.Id);

        if (dbModel == null)
        {
            dbModel = new ProfesorDbModel
            {
                id = profesor.Id,
                nombre = profesor.Nombre,
            };
            await _context.Profesor.AddAsync(dbModel);
        }
        else
        {
            dbModel.nombre = profesor.Nombre;
            _context.Profesor.Update(dbModel);
        }
        await _context.SaveChangesAsync();
        return new Profesor(dbModel.id, dbModel.nombre ?? string.Empty);
    }

    public async Task<List<Profesor>> ListProfesor()
    {
        var profesorDb = await _context.Profesor.ToListAsync();

        var listaProfesor = profesorDb.Select(e => new Profesor
        {
            Id = e.id,
            Nombre = e.nombre,
        }).ToList();

        return listaProfesor;
    }

    public async Task<Profesor?> profesorId(int id)
    {
        ProfesorDbModel? profesorDb = await _context.Profesor.FindAsync(id);

        if (profesorDb == null)
        {
            return null;
        }

        var profesor = new Profesor
        {
            Id = profesorDb.id,
            Nombre = profesorDb.nombre,
        };

        return profesor;
    }

    public async Task<Profesor?> Eliminarprofesor(Profesor profesor)
    {
        var dbModel = await _context.Profesor.FindAsync(profesor.Id);

        if (dbModel != null)
        {
            _context.Profesor.Remove(dbModel);
            await _context.SaveChangesAsync();
            return profesor;
        }
        else
        {
            return null;
        }
    }
}
