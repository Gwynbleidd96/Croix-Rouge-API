using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CroixRouge.DTO;
using CroixRouge.Model.Exceptions;

namespace CroixRouge.api.Infrastructure 
{
    public class PersonnalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public PersonnalExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("CroixRouge.api.Exceptions");
        }

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Une erreur inattendue s'est produite");


            if (context.Exception.GetType()==typeof(DbUpdateConcurrencyException))
            {
               
                var result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Content = Newtonsoft.Json.JsonConvert.SerializeObject(new PersonnalErrorDTO("Access concurent à la base de donnée")),
                    ContentType = "application/json"

                };
                context.Result = result;
            }
            else
            {
                if (context.Exception.GetType().IsSubclassOf(typeof(PersonnalException)))
                {
                    var result = new ContentResult()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Content = Newtonsoft.Json.JsonConvert.SerializeObject(new PersonnalErrorDTO(context.Exception.Message)),
                        ContentType = "application/json"

                    };
                    context.Result = result;
                }
            }
        }
    }
}