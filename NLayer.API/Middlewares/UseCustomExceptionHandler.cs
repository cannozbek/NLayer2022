﻿using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    //Bir exception sınıfı yazmak için sınıfımıın static olması gerekli
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<ExceptionHandlerFeature>();

                    var statusCode = exceptionFeature?.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500,
                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(exceptionFeature.Error.Message, statusCode);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });




            });

        }
    }
}
