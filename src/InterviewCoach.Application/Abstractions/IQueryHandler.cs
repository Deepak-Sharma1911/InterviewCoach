using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application.Abstractions
{
    public interface IQueryHandler<TQuery, TRequest> : IRequestHandler<TQuery, TRequest> where TQuery : IQuery<TRequest> where TRequest : notnull
    {
    }  
}
