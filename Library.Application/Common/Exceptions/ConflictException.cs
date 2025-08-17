﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Exceptions
{
    public sealed class ConflictException : Exception
    {
        public ConflictException(string? message = null) : base(message ?? "Conflict.") { }
    }
}
