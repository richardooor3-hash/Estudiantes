namespace Aplicación.Dtos.In;

public record ActualizarNotaRequest
(
    int Id,
    string Nombre,
    int IdProfesor,
    int IdEstudiante,
    decimal Valor
);
