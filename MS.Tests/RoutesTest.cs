using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Moq;

namespace MS.Tests
{
    [TestClass]
    public class RoutesTest
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod)
                .Returns(httpMethod);

            // create mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns(It.IsAny<string>());

            return null;
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
