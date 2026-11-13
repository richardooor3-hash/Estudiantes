using System.ComponentModel.DataAnnotations;

namespace Aplicación.Dtos.In;

public record RegistrarNotaRequest
(
    [ Required(ErrorMessage = "El concepto de la nota es obligatorio.")]
    [ StringLength(100, MinimumLength = 2, ErrorMessage = "El concepto debe tener entre 2 y 100 caracteres.")]
    [ RegularExpression(@"^(?!string$).+$", ErrorMessage = "El nombre no puede ser la palabra 'string'.")]
    string Nombre,

    [ Required(ErrorMessage = "El profesor es obligatorio.")]
    [ Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un profesor válido.")]
    int IdProfesor,

    [ Required(ErrorMessage = "El estudiante es obligatorio.")]
    [ Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante válido.")]
    int IdEstudiante,

    [ Required(ErrorMessage = "El valor de la nota es obligatorio.")]
    [ Range(0.0, 5.0, ErrorMessage = "El valor de la nota debe estar entre 0.00 y 5.00.")]
    decimal Valor
);
