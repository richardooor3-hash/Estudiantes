namespace Aplicación.Dtos.Out;

public record ConsultaNotaResponse
(
    int Id,
    string Nombre,
    int IdProfesor,
    string NombreProfesor,
    int IdEstudiante,
    string NombreEstudiante,
    decimal Valor
);
