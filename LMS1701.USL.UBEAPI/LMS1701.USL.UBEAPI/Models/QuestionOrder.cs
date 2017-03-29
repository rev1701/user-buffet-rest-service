using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class QuestionOrder
    {
        public int QuestionNumber { get; set; }
        public string QuestionID { get; set; }
        public int ExamAssessmentFK { get; set; }
    
        //public virtual ExamAssessment ExamAssessment { get; set; }
    }
}