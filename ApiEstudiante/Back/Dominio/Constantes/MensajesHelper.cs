
namespace Dominio.Constantes;

public static class MensajesHelper
{
    public static string CreadoOk(string entidad, bool esFemenino = false)
    {
        string sufijo = esFemenino ? "a" : "o";
        return $"{Capitalizar(entidad)} cread{sufijo} con éxito.";
    }

    public static string ActualizadoOk(string entidad, bool esFemenino = false)
    {
        string sufijo = esFemenino ? "a" : "o";
        return $"{Capitalizar(entidad)} actualizad{sufijo} con éxito.";
    }

    public static string EliminadoOk(string entidad, bool esFemenino = false)
    {
        string sufijo = esFemenino ? "a" : "o";
        return $"{Capitalizar(entidad)} eliminad{sufijo} con éxito.";
    }

    public static string ErrorGuardado(string entidad, bool esFemenino = false)
    {
        string articulo = esFemenino ? "la" : "el";
        return $"No se pudo guardar {articulo} {entidad.ToLower()}. Intenta de nuevo.";
    }

    public static string ErrorActualizar(string entidad, bool esFemenino = false)
    {
        string articulo = esFemenino ? "la" : "el";
        return $"No se pudo actualizar {articulo} {entidad.ToLower()}. Intenta de nuevo.";
    }

    public static string ErrorEliminar(string entidad, bool esFemenino = false)
    {
        string articulo = esFemenino ? "la" : "el";
        return $"No se pudo eliminar {articulo} {entidad.ToLower()}. Intenta de nuevo.";
    }

    public static string ErrorConsulta(string entidadPlural, bool esFemenino = false)
    {
        string articulo = esFemenino ? "las" : "los";
        return $"No se pudieron cargar los datos de {articulo} {entidadPlural.ToLower()}.";
    }

    private static string Capitalizar(string texto) =>
        string.IsNullOrEmpty(texto) ? texto : char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
}
