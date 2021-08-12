﻿using System;
using Microsoft.EntityFrameworkCore;

namespace OLT.Core
{
    public interface IOltFileBuilder<in TRequest, in TParameter> : IOltFileBuilder, IOltInjectableSingleton
        where TRequest : IOltRequest
        where TParameter : class, IOltGenericParameter
    {
        IOltFileBase64 Build(TRequest request, TParameter parameter);

    }

    public interface IOltFileBuilder<in TRequest> : IOltFileBuilder, IOltInjectableSingleton
        where TRequest : IOltRequest
    {
        IOltFileBase64 Build(TRequest request);
    }
}