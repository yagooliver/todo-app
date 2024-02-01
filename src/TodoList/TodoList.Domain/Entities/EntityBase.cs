using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}
