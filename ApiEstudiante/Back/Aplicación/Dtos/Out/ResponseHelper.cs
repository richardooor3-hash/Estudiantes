using Dominio.Model;

namespace Aplicación.Dtos.Out;

public static class ResponseHelper
{
    // Para respuestas exitosas con datos (HTTP 200)
    public static ResponseModel<T> Exito<T>(T model,string descripcion, string mensaje, int status)
    {
        return new ResponseModel<T>
        {
            Status = status,
            Descripcion = descripcion,
            Mensaje = mensaje,
            Model = model
        };
    }


    public static ResponseModel<object> ExitoSinDatos(string descripcion,string mensaje, int status)
    {
        return new ResponseModel<object>
        {
            Status = status,
            Descripcion = descripcion,
            Mensaje = mensaje,
            Model = null
        };
    }

    public static ResponseModel<T> Error<T>(string descripcion, string mensaje, int status)
    {
        return new ResponseModel<T>
        {
            Status = status,
            Descripcion = descripcion,
            Mensaje = mensaje,
            Model = default
        };
    }
}
