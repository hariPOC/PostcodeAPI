using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Postcode.Contracts;
using Postcode.Models;

namespace PostcodeAPI.Controllers
{
    /// <inheritdoc cref="IPostcodeService"/>
    [Route("[controller]")]
    public class PostcodeController : ControllerBase, IPostcodeService
    {
        private readonly IPostcodeService postcodeService;

        public PostcodeController(IPostcodeService postcodeService)
        {
            this.postcodeService = postcodeService;
        }

        [HttpGet("AutoComplete")]
        public Task<IEnumerable<string>> Autocomplete(string postcode)
        {
            var result = postcodeService.Autocomplete(postcode);
            return result;
        }

        

        [HttpGet("Lookup")]
        [Produces("application/json")]
        public Task<PostcodeInfo> Lookup(string postcode)
        {
            var result = postcodeService.Lookup(postcode);
            return result;
        }


    }
}

