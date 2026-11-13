

using System.ComponentModel.DataAnnotations;

namespace Aplicación.Dtos.In;

public record ActualizarEstudianteRequest
(
    [ Required(ErrorMessage = "El Id del estudiante es obligatorio.")]
    [ Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante válido.")]
    int Id,
    [ Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
    [ StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [ RegularExpression(@"^(?!string$).+$", ErrorMessage = "El nombre no puede ser la palabra 'string'.")]
    string Nombre
);

