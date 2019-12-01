using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonDirectory.API.HelperTypes;

namespace PersonDirectory.API.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                FormatResult(context.Result, nameof(StatusCodes.Status400BadRequest));
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            FormatResult(context.Result);
        }

        private void FormatResult(IActionResult result, string message = null)
        {
            if (result is ObjectResult)
            {
                var response = new ApiResponse<object>();

                var res = (ObjectResult)result;
                response.Code = res.StatusCode.HasValue ? (int)res.StatusCode : StatusCodes.Status500InternalServerError;
                if(response.Code == StatusCodes.Status200OK)
                {
                    response.Message = message ?? "success";
                    response.Result = res.Value;
                }
                else
                {
                    response.Message = message ?? "fail";
                    response.Result = new { Error = res.Value };
                }

                res.Value = response;
            }
        }
    }
}
