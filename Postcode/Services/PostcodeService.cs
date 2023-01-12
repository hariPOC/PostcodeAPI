using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Flurl.Http;
using Newtonsoft.Json;
using Postcode.Contracts;
using Postcode.Models;

namespace Postcode.Services
{
    public class PostcodeService : IPostcodeService
    {
        public IUrlFactoryService UrlFactory { get; set; }

        public async Task<IEnumerable<string>> Autocomplete(string postcode)
        {
            if (postcode == null)
            {
                return EmptyList();
            }

            IFlurlRequest request = UrlFactory.PostcodeUrl;
            var result = await request
                .AppendPathSegment(postcode + "/autocomplete")
                .GetJsonAsync<AutocompleteResponse>().ConfigureAwait(false);
            if (result.Postcodes == null)
            {
                return EmptyList();
            }

            return result.Postcodes;
        }

        public async Task<PostcodeInfo> Lookup(string postcode)
        {
            IFlurlRequest request = UrlFactory.PostcodeUrl;
            var result = await request
                .AppendPathSegment(postcode)
                .GetJsonAsync<LookupResponse>().ConfigureAwait(false);
            PostcodeInfo postcodeInfo = result.PostcodeInfo;

            GetArea(postcodeInfo);

            return result.PostcodeInfo;
        }

        public async Task<string> DbConnection()
        {
            string secretName = "DbConnection";
            string region = "us-east-1";

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName,
                VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
            };

            GetSecretValueResponse response;

            response = await client.GetSecretValueAsync(request);


            string secret = response.SecretString;

            return secret;
        }

        private static void GetArea(PostcodeInfo postcodeInfo)
        {
            double latitude = postcodeInfo.latitude;
            if (latitude < 52.229466)
            {
                postcodeInfo.Area = "South";
            }
            else if (latitude >= 52.229466 && latitude < 53.27169)
            {
                postcodeInfo.Area = "Midlands";
            }
            else if (latitude >= 53.27169)
            {
                postcodeInfo.Area = "North";
            }
        }

        private static IEnumerable<string> EmptyList()
        {
            return new List<string>();
        }


    }
}

