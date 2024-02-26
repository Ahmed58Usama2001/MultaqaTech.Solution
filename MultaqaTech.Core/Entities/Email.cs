using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultaqaTech.Core.Entities
{
    public class Email : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
    }
}
