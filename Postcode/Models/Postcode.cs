using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Postcode.Models
{
    public class LookupResponse
    {
        [JsonProperty("result")]
        public PostcodeInfo PostcodeInfo { get; set; }
    }

    public class PostcodeInfo
    {
        public string Postcode { get; set; }

        public string Country { get; set; }

        public double latitude { get; set; }

        public string Region { get; set; }

        [JsonProperty("admin_district")]
        public string AdminDistrict { get; set; }

        [JsonProperty("parliamentary_constituency")]
        public string ParliamentaryConstituency { get; set; }

        public string Area { get; set; }
    }

    public class AutocompleteResponse
    {
        [JsonProperty("result")]
        public List<string> Postcodes { get; set; }
    }

}

