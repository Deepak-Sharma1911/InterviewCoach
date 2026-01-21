using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message, HttpStatusCode httpStatus = HttpStatusCode.InternalServerError) : base(message)
        {
            StatusCode = httpStatus;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
