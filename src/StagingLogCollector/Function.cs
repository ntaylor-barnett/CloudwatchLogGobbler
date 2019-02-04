using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace StagingLogCollector
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(Amazon.Lambda.CloudWatchLogsEvents.CloudWatchLogsEvent input, ILambdaContext context)
        {
            // Sometimes, you have to have a sense of humor or else you will go
            var insane = JsonConvert.DeserializeObject<IHateAWSSomedays>(input.Awslogs.DecodeData());
            foreach (var log in insane.LogEvents)
            {
                Console.WriteLine(log.Message);
            }
        }

        public class IHateAWSSomedays
        {
            [JsonProperty("owner")]
            public string Owner { get; set; }
            [JsonProperty("logGroup")]
            public string LogGroup { get; set; }
            [JsonProperty("logEvents")]
            public List<IMeanReallyTheyCouldHaveDoneThisForUs> LogEvents { get; set; }
        }

        public class IMeanReallyTheyCouldHaveDoneThisForUs
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("timestamp")]
            public string TimeStamp { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}
