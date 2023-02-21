using netgo.treeplanting.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Entities
{
    public class Treeschool : Entity
    {
        public Treeschool(
            Guid id,
            string name,
            string description) : base(id)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; set;}        
        public virtual IEnumerable<Seedling>? Seedlings { get; set; }
        public string Name { get; set;}
        public string Description { get; set;}
    }
}
