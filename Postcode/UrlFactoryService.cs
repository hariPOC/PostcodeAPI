using System;
using Flurl.Http;

namespace Postcode
{
	public class UrlFactoryService : IUrlFactoryService
	{
		

        public IFlurlRequest PostcodeUrl
		{
			get
			{
				string postcodeUrl = Environment.GetEnvironmentVariable("PostcodeUrl");
				IFlurlRequest request = new FlurlRequest(postcodeUrl);
				return request;
			}
		}
    }
}

