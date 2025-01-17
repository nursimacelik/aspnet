﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Final.Project.Core.Shared
{
    public class ApplicationResult<T> : CommonApplicationResult
    {
        public T Result { get; set; }
    }

    public class ApplicationResult : CommonApplicationResult
    {

    }
    public class CommonApplicationResult
    {
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

    }
}
