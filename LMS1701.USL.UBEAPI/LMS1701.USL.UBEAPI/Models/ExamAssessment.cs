﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class ExamAssessment
    {
        public int ExamAssessmentID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> Attempts { get; set; }
        public int SettingsID { get; set; }
        public Nullable<System.TimeSpan> TimeRemaining { get; set; }
        public Nullable<bool> IsExamComplete { get; set; }
        public Nullable<decimal> Score { get; set; }

        public virtual ExamSetting ExamSetting { get; set; }
        public virtual ICollection<QuestionOrder> QuestionOrders { get; set; }
    }
}