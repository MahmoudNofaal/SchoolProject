using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Bases.CQRS;

public interface ICommandHandler<TCommand, TValue> : IRequestHandler<TCommand, TValue>
   where TCommand : ICommand<TValue>
{
}
