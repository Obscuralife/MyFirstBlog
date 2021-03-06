﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MyBlog.Services.Models
{
    [Serializable]
    public class RequestedResourceHasConflictException : Exception
    {
        public RequestedResourceHasConflictException()
        {
        }

        public RequestedResourceHasConflictException(string message)
            : base(message)
        {
        }

        public RequestedResourceHasConflictException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected RequestedResourceHasConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
