using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using ErrorMultilingualResourcesFile.Test;
using Amazon.Lambda.APIGatewayEvents;

namespace ErrorMultilingualResourcesFile.Test.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestFrench()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            request.QueryStringParameters = new Dictionary<string, string>() { { "language", "1" } };
            var response = function.FunctionHandler(request, context);

            Assert.Equal("Bonjour le monde", response.Body);
        }

        [Fact]
        public void TestItalian()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            request.QueryStringParameters = new Dictionary<string, string>() { { "language", "2" } };
            var response = function.FunctionHandler(request, context);

            Assert.Equal("Ciao mondo", response.Body);
        }

        [Fact]
        public void TestInvalid()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var request = new APIGatewayProxyRequest();
            request.QueryStringParameters = new Dictionary<string, string>() { { "language", "3" } };
            var response = function.FunctionHandler(request, context);

            Assert.Equal("Invalid language", response.Body);
        }
    }
}
