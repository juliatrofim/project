using System;

namespace MediaContentHSE.Models
{
    public class TargetGroup
    {
        public Guid TargetGroupId { get; set; }

        //group name
        public string GroupName { get; set; }

        //client gender 
        public string Gender { get; set; }

        //client age start
        public int StartAge { get; set; }

        //client age end
        public int EndAge { get; set; }
    }
}
