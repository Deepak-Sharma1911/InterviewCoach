using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCoach.Application.Wrappers
{
    public class Response<T>
    {
        public Response(T Data, string Message)
        {
            this.Data = Data;
            this.Message = Message;
            this.Errors = new List<string>();
        }
        public Response(string message)
        {
            this.Message = message;
            this.Errors = new List<string> { message };
            this.Data = default!;
        }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success => !Errors.Any();
    }
}
