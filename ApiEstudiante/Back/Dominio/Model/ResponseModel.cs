namespace Dominio.Model;

public class ResponseModel<T>
{
    public int Status { get; set; }
    public string? Descripcion { get; set; }
    public string? Mensaje { get; set; }
    public T? Model { get; set; }
}
