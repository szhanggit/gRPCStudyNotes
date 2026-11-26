using MediatR;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services.Wrappers
{
    public interface IRequestWrapper<T> : IRequest<Response<T>>
    {
    }
}
