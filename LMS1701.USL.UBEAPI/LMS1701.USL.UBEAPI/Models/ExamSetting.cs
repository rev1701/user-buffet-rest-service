using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class ExamSetting
    {
        public int ExamSettingsID { get; set; }
        public System.DateTime StartTime { get; set; }
        public int LengthOfExamInMinutes { get; set; }
        public string ExamTemplateID { get; set; }
        public bool Editable { get; set; }
        public System.DateTime EndTime { get; set; }
        public int AllowedAttempts { get; set; }
    }
}