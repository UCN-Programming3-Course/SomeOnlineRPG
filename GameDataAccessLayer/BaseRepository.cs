using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataAccessLayer
{
    public class BaseRepository
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public BaseRepository(Func<IDbConnection> connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }

        protected IDbConnection GetConnection()
        {
            return _connectionFactory?.Invoke();
        }
    }
}
