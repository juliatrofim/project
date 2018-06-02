using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaContentHSE.Models
{
    public class TargetMediaContent
    {
        public Guid TargetMediaContentId { get; set; }

        //group of target by age and gender 
        public Guid? TargetGroupId { get; set; }
        public virtual TargetGroup TargetGroup { get; set; }

        //media content
        public Guid? MediaContentId { get; set; }
        public virtual  MediaContent MediaContent { get; set; }


        //sequence 
        public int SequenceNumber { get; set; }

        //start date-time (showing time period)
        public DateTime StartDate { get; set; }

        //end date-time (showing time period)
        public DateTime EndDate { get; set; }

        //interface
        public Guid? TargetMediaContentInterfaceId { get; set; }
        public virtual TargetMediaContentInterface TargetMediaContentInterface { get; set; }

    }
}
