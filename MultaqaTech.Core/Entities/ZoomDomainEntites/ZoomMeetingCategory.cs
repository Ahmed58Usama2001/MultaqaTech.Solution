using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultaqaTech.Core.Entities.ZoomDomainEntites
{
    public class ZoomMeetingCategory : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public List<ZoomMeeting> ZoomMeetings { get; set; } = new();
    }
}