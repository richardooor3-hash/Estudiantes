namespace Aplicación.Dtos.Out;

public record ConsultaNotaResponse
(
    int Id,
    string Nombre,
    int IdProfesor,
    int IdEstudiante,
    decimal Valor
);
