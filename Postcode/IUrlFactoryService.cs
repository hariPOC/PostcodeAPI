using System;
using Flurl.Http;
namespace Postcode
{
	public interface IUrlFactoryService
	{
		IFlurlRequest PostcodeUrl { get; }
	}
}

