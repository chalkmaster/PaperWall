using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaperWall.Core.DomainObject.Validation;

namespace PaperWall.Core.DomainObject
{
    public class Message: Entity
    {
        public virtual string MessageText { get; set; }
        public virtual string Writter { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual double Precision { get; set; }
        public virtual DateTime PostedAt { get; set; }

        public override bool Validate(out List<string> brokenRules)
        {
            return MessageValidator.Validate(this, out brokenRules);
        }
    }
}
