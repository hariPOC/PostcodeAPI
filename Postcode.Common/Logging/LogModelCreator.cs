using System;
using Newtonsoft.Json;
using Postcode.Common.Logging.Models;

namespace Postcode.Common.Logging
{
    public interface ILogModelCreator
    {
        LogModel LogModel { get; }
        string LogString();
    }


    public class LogModelCreator : ILogModelCreator
    {
        public LogModel LogModel { get; private set; }

        public LogModelCreator()
        {
            LogModel = new LogModel();
        }

        public string LogString()
        {
            var jsonString = JsonConvert.SerializeObject(LogModel);
            return jsonString;
        }
    }
}

