namespace Dominio.Entidades;

public class Nota
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int IdProfesor { get; set; }
    public string? NombreProfesor { get; set; }
    public int IdEstudiante { get; set; }
    public string? NombreEstudiante { get; set; }
    public decimal Valor { get; set; }

    public Nota() 
    { 
    }

    public Nota(int id,string nombre,int idProfesor, int idEstudiante,decimal valor)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.IdProfesor = idProfesor;
        this.IdEstudiante = idEstudiante;
        this.Valor = valor;
    }
}
