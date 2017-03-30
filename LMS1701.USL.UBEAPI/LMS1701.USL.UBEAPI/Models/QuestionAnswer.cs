using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class QuestionAnswer
    {
        public int QuestionOrderID { get; set; }
        public string Answer { get; set; }

        //public virtual QuestionOrder QuestionOrder { get; set; }
    }
}