using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Domain.Utilities
{
    public class Response
    {
        public int Code { get; set; }                    // Código HTTP o de resultado
        public string Status { get; set; }               // "OK" o "ERROR"
        public string Message { get; set; }              // Mensaje principal
        public object? Data { get; set; }                // Datos devueltos

        //Lista opcional para errores múltiples (ej: validaciones)
        public List<string>? Errors { get; set; }

        //Determina si fue exitosa la operación
        public bool IsSuccess => Code >= 200 && Code < 300;

        public Response() { }

        public Response(int code, string status, string message, object? data = null)
        {
            Code = code;
            Status = status;
            Message = message;
            Data = data;
        }

        //Respuesta exitosa estándar
        public static Response Success(object? data = null, string message = "Operación exitosa")
            => new Response(200, "OK", message, data);

        //Respuesta de error estándar
        public static Response Error(string message, int code = 400)
            => new Response(code, "ERROR", message, null);

        //Nueva: errores de validación con lista
        public static Response ValidationError(List<string> errors)
        {
            return new Response
            {
                Code = 422,
                Status = "ERROR",
                Message = "Errores de validación",
                Errors = errors,
                Data = null
            };
        }
    }
}
