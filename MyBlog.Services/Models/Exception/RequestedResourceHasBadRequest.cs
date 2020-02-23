using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyBlog.Services.Models
{
    [Serializable]
    public class RequestedResourceHasBadRequest : Exception
    {
        public RequestedResourceHasBadRequest()
        {
        }

        public RequestedResourceHasBadRequest(string message) : base(message)
        {
        }

        public RequestedResourceHasBadRequest(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestedResourceHasBadRequest(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
