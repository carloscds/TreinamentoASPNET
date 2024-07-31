﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static System.Formats.Asn1.AsnWriter;

namespace APIBanco.Middlewares
{
    public class MiddlewareToken
    {
        private readonly RequestDelegate _next;

        public MiddlewareToken(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/" || context.Request.Path == "/favicon.ico")
            {
                await _next(context);
                return;
            }
            string autorizacao = context.Request.Headers["Authorization"];
            if (autorizacao == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Token");
                return;
            }
            // validar o token
            await _next(context);
        }
    }
}