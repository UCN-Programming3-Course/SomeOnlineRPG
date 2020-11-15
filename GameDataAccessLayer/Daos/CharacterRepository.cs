using Dapper;
using GameDataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataAccessLayer.Daos
{
    public class CharacterRepository : BaseRepository, IRepository<Character>
    {
        public CharacterRepository(Func<IDbConnection> connectionFactory) : base(connectionFactory)
        {
        }

        public IEnumerable<Character> GetAll()
        {
            using (var conn = GetConnection())
            {
                var characters = conn.Query<Character>("SELECT * FROM Characters");
                foreach (var character in characters)
                {
                    character.Items = conn.Query<string>("SELECT Name FROM Items WHERE CharacterId = @id", new { id = character.Id });
                }
                return characters;
            }
        }

        public Character GetById(Guid id)
        {
            using (var conn = GetConnection())
            {
                var character = conn.QuerySingleOrDefault<Character>("SELECT * FROM Characters WHERE WebId = @id", new { id });
                if (character != null)
                {
                    character.Items = conn.Query<string>("SELECT Name FROM Items WHERE CharacterId = @id", new { id = character.Id });
                }
                return character;
            }
        }
    }
}
