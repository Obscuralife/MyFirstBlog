using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyBlog.Services.Models
{
    [Serializable]
    public class RequestedResourceNotFoundException : Exception
    {
        public RequestedResourceNotFoundException()
        {
        }

        public RequestedResourceNotFoundException(string message)
            : base(message)
        {
        }

        public RequestedResourceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RequestedResourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
