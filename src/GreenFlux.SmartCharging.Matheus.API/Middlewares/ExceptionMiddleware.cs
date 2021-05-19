using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using GreenFlux.SmartCharging.Matheus.Domain.Exceptions;
using GreenFlux.SmartCharging.Matheus.API.Resources.ProblemDetail;

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
            catch(CapacityExceededException ex)
            {
                CapacityExceededProblemDetail capacityExceededResponse = new CapacityExceededProblemDetail(ex.ExceededCapacity, ex.RemoveSuggestions);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var json = JsonConvert.SerializeObject(capacityExceededResponse);
                await httpContext.Response.WriteAsync(json);
            }
            catch (NoSlotsAvailableException ex)
            {
                NoSlotsAvailableProblemDetail noSlotAvailableResponse = new NoSlotsAvailableProblemDetail(ex.ChargeStationId);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var json = JsonConvert.SerializeObject(noSlotAvailableResponse);
                await httpContext.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                //TODO log internally the ex message
                ProblemDetails errorDetail = new ProblemDetails();
                
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetail.Title = "Internal server Error";
                errorDetail.Status = 500;
                var json = JsonConvert.SerializeObject(errorDetail);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}

