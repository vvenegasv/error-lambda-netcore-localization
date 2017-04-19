using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using System.Threading;
using System.Globalization;
using ErrorMultilingualResourcesFile.Resources;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System.IO;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ErrorMultilingualResourcesFile
{
    public class Function
    {
        
        /// <summary>
        /// This function shows a message depending on selected language
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var jsonProxyRequest = Serialize(request);
            var response = new APIGatewayProxyResponse();
            context.Logger.LogLine($"Proxy request: {jsonProxyRequest}");

            if (request == null || request.QueryStringParameters == null || request.QueryStringParameters["language"] == null)
            {
                response.Body = "Invalid request";
                response.StatusCode = 400;
                return response;
            }
                

            switch(request.QueryStringParameters["language"])
            {
                case "1":
                    CultureInfo.CurrentCulture = new CultureInfo("fr");
                    CultureInfo.CurrentUICulture = new CultureInfo("fr");
                    break;
                case "2":
                    CultureInfo.CurrentCulture = new CultureInfo("it");
                    CultureInfo.CurrentUICulture = new CultureInfo("it");
                    break;
                default:                    
                    response.Body = "Invalid language";
                    response.StatusCode = 400;
                    return response;                    
            }
            
            response.Body = Messages.Greetings;
            response.StatusCode = 200;
            return response;
        }

        private string Serialize(Object item)
        {
            var jsonSerializer = new JsonSerializer();
            using (var writer = new StringWriter())
            {
                jsonSerializer.Serialize(writer, item);
                return writer.ToString();
            }
        }
    }
}
