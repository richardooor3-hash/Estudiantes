namespace Aplicación.Dtos.In;

public record RegistrarNotaRequest
(
    string Nombre,
    int IdProfesor,
    int IdEstudiante,
    decimal Valor
);
