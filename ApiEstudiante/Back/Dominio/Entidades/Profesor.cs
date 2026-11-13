namespace Dominio.Entidades;

public class Profesor
{
    public int Id { get; set; }
    public string? Nombre { get; set; }

    public Profesor()
    {

    }
    public Profesor(int id, string nombre)
    {
        this.Id = id;
        this.Nombre = nombre;
    }
}
