using System.ComponentModel.DataAnnotations;

namespace Aplicación.Dtos.In;

public record RegistrarProfesorRequest
(
    [ Required(ErrorMessage = "El nombre del profesor es obligatorio.")]
    [ StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [ RegularExpression(@"^(?!string$).+$", ErrorMessage = "El nombre no puede ser la palabra 'string'.")]
    string Nombre
);
