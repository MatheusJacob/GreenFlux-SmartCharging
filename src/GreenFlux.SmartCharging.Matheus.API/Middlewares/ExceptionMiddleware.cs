using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace GreenFlux.SmartCharging.Matheus.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                ProblemDetails details = new ProblemDetails();
                
                var errorDetail = new ErrorDetails();
                errorDetail.Message = ex.Message;
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                details.Title = "ae";
                
                var json = JsonConvert.SerializeObject(details);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}

