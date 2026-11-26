using Common.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Wrappers
{
    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, Response<TOut>>
        where TIn : IRequestWrapper<TOut>
    {
    }
}
