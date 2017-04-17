using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class UserGradebook
    {
        public User user { get; set; }
        public Dictionary<int,string> Batches { get; set; } //gradebook id as key, batch name as value
        public virtual List<ExamAssessment> gradebook { get; set; }
    }
}