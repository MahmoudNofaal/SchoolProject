using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Application.Bases.CQRS;

public interface IQuery<TValue> : IRequest<TValue>
{
}
