using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Application.Bases;
using System.Net;

namespace SchoolProject.Presentation.Base;

[ApiController]
public class AppControllerBase : ControllerBase
{
   private IMediator? _mediator;

   protected IMediator Mediator
   {
      get
      {
         if (_mediator is not null)
         {
            return _mediator;
         }

         var mediator = HttpContext.RequestServices.GetService<IMediator>();
         if (mediator is null)
         {
            throw new InvalidOperationException("IMediator service is not available in the current request context.");
         }

         _mediator = mediator;
         return _mediator;
      }
   }


   public ObjectResult NewResult<T>(Response<T> response)
   {
      switch (response.StatusCode)
      {
         case HttpStatusCode.OK:
            return new OkObjectResult(response);
         case HttpStatusCode.Created:
            return new CreatedResult(string.Empty, response);
         case HttpStatusCode.Unauthorized:
            return new UnauthorizedObjectResult(response);
         case HttpStatusCode.BadRequest:
            return new BadRequestObjectResult(response);
         case HttpStatusCode.NotFound:
            return new NotFoundObjectResult(response);
         case HttpStatusCode.Accepted:
            return new AcceptedResult(string.Empty, response);
         case HttpStatusCode.UnprocessableEntity:
            return new UnprocessableEntityObjectResult(response);
         default:
            return new BadRequestObjectResult(response);
      }
   }

   public ObjectResult NewResult<T>(Response<IEnumerable<T>> response)
   {
      switch (response.StatusCode)
      {
         case HttpStatusCode.OK:
            return new OkObjectResult(response);
         case HttpStatusCode.Created:
            return new CreatedResult(string.Empty, response);
         case HttpStatusCode.Unauthorized:
            return new UnauthorizedObjectResult(response);
         case HttpStatusCode.BadRequest:
            return new BadRequestObjectResult(response);
         case HttpStatusCode.NotFound:
            return new NotFoundObjectResult(response);
         case HttpStatusCode.Accepted:
            return new AcceptedResult(string.Empty, response);
         case HttpStatusCode.UnprocessableEntity:
            return new UnprocessableEntityObjectResult(response);
         default:
            return new BadRequestObjectResult(response);
      }
   }

}
