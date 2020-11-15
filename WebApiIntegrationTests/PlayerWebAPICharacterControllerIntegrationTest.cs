using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http.Results;
using GameDataAccessLayer;
using GameDataAccessLayer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayerWebAPI.Controllers;
using PlayerWebAPI.Models;

namespace WebApiIntegrationTests
{
    [TestClass]
    public class PlayerWebAPICharacterControllerIntegrationTest
    {
        private CharactersController _controller;

        private const string GOOD_ID = "a434cf78-2b44-4132-8b60-db567f64f93d";
        private const string BAD_ID = "f1bcbe50-ff61-43bf-bb96-ae119f53209e";

        [TestInitialize]
        public void Initialize()
        {
            var repos = RepositoryFactory.Create<Character>(()=>new SqlConnection("data source=(localdb)\\mssqllocaldb;initial catalog=SomeOnlineRPG;integrated security=True"));
            _controller = new CharactersController(repos);
        }

        [TestMethod]
        public void TestGetCharacterThatExists()
        {
            var data = _controller.GetCharacter(Guid.Parse(GOOD_ID));
            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(OkNegotiatedContentResult<CharacterDto>));
        }

        [TestMethod]
        public void TestGetItemsForCharacterThatExists()
        {
            var data = _controller.GetCharacterItems(Guid.Parse(GOOD_ID));
            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(OkNegotiatedContentResult<List<string>>));
        }
    }
}
