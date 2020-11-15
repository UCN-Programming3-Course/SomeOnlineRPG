using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataAccessLayer.Model
{
    public class Character
    {
        public int Id { get; set; }
        public Guid WebId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Items { get; set; }
    }
}
