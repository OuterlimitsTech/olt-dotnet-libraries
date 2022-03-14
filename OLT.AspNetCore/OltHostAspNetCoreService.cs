using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace OLT.Core
{
    public class OltHostAspNetCoreService : OltHostServiceBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public OltHostAspNetCoreService(IWebHostEnvironment environment)
        {
            _hostEnvironment = environment;            
        }

        public override string ResolveRelativePath(string filePath)
        {
            return Path.Combine(_hostEnvironment.WebRootPath, filePath.Replace("~/", string.Empty));
        }
               
        public override string EnvironmentName => _hostEnvironment.EnvironmentName;
        public override string ApplicationName => _hostEnvironment.ApplicationName;
    }
}