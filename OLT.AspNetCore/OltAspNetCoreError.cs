﻿using System;

namespace OLT.Core
{
    public class OltAspNetCoreError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
