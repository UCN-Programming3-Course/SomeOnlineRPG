using GameDataAccessLayer.Daos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataAccessLayer
{
    public static class RepositoryFactory
    {
        public static IRepository<TEntity> Create<TEntity>(Func<IDbConnection> connectionFactory)
        {
            switch (typeof(TEntity).Name)
            {
                case "Character":
                    return new CharacterRepository(connectionFactory) as IRepository<TEntity>;
                default:
                    throw new Exception("Unknown entity");
            }
        } 
    }
}
