using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APIBanco.Middlewares
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;

        public MiddlewareException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var _modelErros = new List<ModelErrors>();
            MontaMensagemErro(_modelErros, ex);

            var result = System.Text.Json.JsonSerializer.Serialize(_modelErros);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
        private static void MontaMensagemErro(List<ModelErrors> _modelErros, Exception ex)
        {
            _modelErros.Add(new ModelErrors { Field = "Erro", Message = ex.Message });
            if (ex.InnerException != null)
            {
                MontaMensagemErro(_modelErros, ex.InnerException);
            }
        }
    }
}
