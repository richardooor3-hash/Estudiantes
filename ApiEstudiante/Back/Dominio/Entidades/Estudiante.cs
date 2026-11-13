namespace Dominio.Entidades;

public class Estudiante
{
    public int Id { get; set; }
    public string? Nombre { get; set; }

    public Estudiante() 
    { 
    
    }
    public Estudiante(int id, string nombre)
    { 
        this.Id = id;
        this.Nombre = nombre;
    }
}
