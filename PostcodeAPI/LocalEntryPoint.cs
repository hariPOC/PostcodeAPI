using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PostcodeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("PostcodeUrl", "http://api.postcodes.io/postcodes/");
            BuildHost(args).Run();
        }

        private static IHost BuildHost(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
            return builder.Build();
        }
       
    }
}

