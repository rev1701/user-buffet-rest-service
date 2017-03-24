using DAL = LMS1701.USL.UBEAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class User
    {
        public int UserPK { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public Nullable<int> UserType { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public virtual ICollection<DAL.ExamAssessment> ExamAssessments { get; set; }
        public virtual DAL.UserType UserType1 { get; set; }
    }
}