using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Postcode.Models;

namespace Postcode.Contracts
{
    public interface IPostcodeService
    {
        Task<IEnumerable<string>> Autocomplete(string postcode);

        Task<PostcodeInfo> Lookup(string postcode);

        Task<string> DbConnection();
    }
}

