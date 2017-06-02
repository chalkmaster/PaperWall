using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaperWall.Core.DomainObject
{
    public abstract class Entity
    {
        public virtual long Id { get; set; }
        public virtual bool Removed { get; set; }

        public abstract bool Validate(out List<String>brokenRules);
    }
}