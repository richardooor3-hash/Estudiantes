namespace Persistencia.Models;

public class NotaDbModel
{
    public int id { get; set; }
    public string? nombre { get; set; }
    public int idProfesor { get; set; }
    public int idEstudiante { get; set; }
    public decimal valor { get; set; }
}
