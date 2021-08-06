////using System;
////using Microsoft.AspNetCore.Builder;

////namespace OLT.Core
////{
////    public class OltAspNetHostingConfiguration<TSettings>
////        where TSettings : OltAspNetAppSettings
////    {

////        public OltAspNetHostingConfiguration(TSettings settings, Action<IApplicationBuilder, TSettings> middlewareAction)
////        {
////            Settings = settings;
////            MiddlewareAction = middlewareAction;
////        }

////        public TSettings Settings { get; }
////        public Action<IApplicationBuilder, TSettings> MiddlewareAction { get; }
////    }
////}