using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HealthCatalystUserSearchAPI.JsonHelpers
{
    public class JsonErrorResult : JsonResult
    {
        private readonly HttpStatusCode _statusCode;

        public JsonErrorResult(object json) : this(json, HttpStatusCode.InternalServerError)
        {
        }

        public JsonErrorResult(object json, HttpStatusCode statusCode) : base(json)
        {
            _statusCode = statusCode;
        }

        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)_statusCode;
            base.ExecuteResult(context);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)_statusCode;
            return base.ExecuteResultAsync(context);
        }
    }
}
