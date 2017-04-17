using DAL = LMS1701.USL.UBEAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class User
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string password { get; set; }
        public int UserType { get; set; }
        public string email { get; set; }
        //public string password { get; set; }
        public virtual ICollection<ExamSetting> ExamSettings { get; set; }
        public virtual UserType UserType1 { get; set; }
    }
}