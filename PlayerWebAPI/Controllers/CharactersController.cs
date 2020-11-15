using Dapper;
using GameDataAccessLayer;
using GameDataAccessLayer.Model;
using PlayerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlayerWebAPI.Controllers
{
    public class CharactersController : ApiController
    {
        private readonly IRepository<Character> _characterDao;

        public CharactersController(IRepository<Character> characterDao)
        {
            _characterDao = characterDao;
        }

        [HttpGet]
        public IHttpActionResult GetCharacter(Guid id)
        {
            var data = _characterDao.GetById(id);
            if (data != null)
            {
                return Ok(new CharacterDto
                {
                    Id = data.WebId,
                    Name = data.Name
                });
            }
            return NotFound();
        }

        [HttpGet]
        [Route("characters/{id}/items")]
        public IHttpActionResult GetCharacterItems(Guid id)
        {
            var data = _characterDao.GetById(id); 
            if (data != null)
            {
                var result = new List<string>();
                foreach (var item in data.Items)
                {
                    result.Add(item);
                }
                return Ok(result);
            }
            return NotFound();
        }
    }
}
