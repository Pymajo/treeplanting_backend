using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netgo.treeplanting.Domain.Core.Entities
{
    public class Entity
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public bool Deleted { get; private set; }

        public void Delete()
        {
            Deleted = true;
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (compareTo is null)
            {
                return false;
            }

            return Id == compareTo.Id;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";

        }

    }
}
