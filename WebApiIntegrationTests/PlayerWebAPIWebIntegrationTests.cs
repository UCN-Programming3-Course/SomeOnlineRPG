using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Dapper;
using GameDataAccessLayer;
using GameDataAccessLayer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlayerWebAPI;

namespace WebApiIntegrationTests
{
    [TestClass]
    public class PlayerWebAPIWebIntegrationTests
    {
        private static HttpServer _server;
        private static string _baseAddress = "http://localhost/";
        private static string _goodId = "f1bcbe52-ff61-43bf-bbf6-fe119f53209e";
        private static string _badId = "f1bcbe50-ff61-43bf-bb96-ae119f53209e";

        [ClassInitialize]
        public static void HostWebAPI(TestContext testContext)
        {
            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            // Register routes
            WebApiConfig.Register(config);

            // Creating mock objects
            var mockedDBConn = new Mock<IRepository<Character>>();
            mockedDBConn.Setup(dao => dao.GetById(Guid.Parse(_goodId)))
                .Returns(new Character
                {
                    Name = "Gilderoy Lockheart",
                    WebId = Guid.Parse(_goodId),
                    Items = new string[] { "Wand", "Cape" }
                });

            // Configuring caontainer for dependency injection
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(Startup).Assembly);
            builder.RegisterInstance(mockedDBConn.Object).As<IRepository<Character>>();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());

            // Starting server
            _server = new HttpServer(config);
        }

        [ClassCleanup]
        public static void CleanupWebAPI()
        {
            _server?.Dispose();
        }

        [TestMethod]
        public void GetCharacterTest200() // Test a successfull call to ../characters/[testid]
        {
            var client = new HttpClient(_server);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_baseAddress + "characters/" + _goodId)
            };
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = client.SendAsync(request).GetAwaiter().GetResult())
            {
                Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public void GetItemsTest200() // Test a successfull call to ../characters/[testid]/items
        {
            var client = new HttpClient(_server);
            var request = new HttpRequestMessage(HttpMethod.Get, _baseAddress + "characters/" + _goodId + "/items");

            using (var response = client.SendAsync(request).GetAwaiter().GetResult())
            {
                Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                var result = response.Content.ReadAsAsync<string[]>().GetAwaiter().GetResult();

                Assert.IsTrue(result.Length == 2);
            }
        }

        [TestMethod]
        public void GetCharacterTest404() // Test a successfull call to ../characters/[badid]
        {
            var client = new HttpClient(_server);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_baseAddress + "characters/" + _badId)
            };

            using (var response = client.SendAsync(request).GetAwaiter().GetResult())
            {
                Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

            }
        }

        [TestMethod]
        public void GetItemsTest404() // Test a successfull call to ../characters/[badid]/items
        {
            var client = new HttpClient(_server);
            var request = new HttpRequestMessage(HttpMethod.Get, _baseAddress + "characters/" + _badId + "/items");

            using (var response = client.SendAsync(request).GetAwaiter().GetResult())
            {
                Assert.IsInstanceOfType(response, typeof(HttpResponseMessage));
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}