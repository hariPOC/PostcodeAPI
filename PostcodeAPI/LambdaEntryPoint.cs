using System;
using Amazon.Lambda.AspNetCoreServer;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PostcodeAPI;

namespace Postcode.API
{

    /// <summary>
    /// This class extends from APIGatewayProxyFunction which contains the methods FunctionHandlerAsync which is the
    /// actual Lambda function entry point. The lambda handler field should be set to
    /// Postcode.API:: Postcode.API.LambdaEntryPoint::FunctionHandlerAsync.
    /// </summary>
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
        }
    }
}

