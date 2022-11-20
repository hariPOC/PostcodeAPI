using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
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
            
            IFlurlRequest request = UrlFactory.PostcodeUrl;
            var result = await request
                .AppendPathSegment(postcode + "/autocomplete")
                .GetJsonAsync<AutocompleteResponse>().ConfigureAwait(false);
            if(result.Postcodes == null)
            {
                result.Postcodes = new List<string>();// To handle 204 No content error
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

       
    }
}

