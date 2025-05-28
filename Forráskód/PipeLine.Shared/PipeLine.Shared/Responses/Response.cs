using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Responses
{
    public class Response : ErrorStore
    {
        public bool IsSuccess => !HasError;
        public Guid Id { get; set; }
        public Response() : base() { }

        public Response(string errorMessage)
        {
            AppendNewError(errorMessage);
        }

        public Response(Guid id)
        {
            Id = id;
        }

        public static Response Success(Guid id) => new Response(id);
        public static Response Failure(string error) => new Response(error);

        public static void CombineWith(Response r1, Response r2)
        {
            if (r2 == null || string.IsNullOrWhiteSpace(r2.Error)) return;

            if (string.IsNullOrWhiteSpace(r1.Error))
            {
                r1.Error = r2.Error;
            }
            else
            {
                r1.Error += "\n" + r2.Error;
            }
        }
    }
}
