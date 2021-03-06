//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS1701.USL.UBEAPI.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExamAssessment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamAssessment()
        {
            this.QuestionOrders = new HashSet<QuestionOrder>();
        }
    
        public int ExamAssessmentID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> Attempts { get; set; }
        public int SettingsID { get; set; }
        public Nullable<System.TimeSpan> TimeRemaining { get; set; }
        public Nullable<bool> IsExamComplete { get; set; }
        public Nullable<decimal> Score { get; set; }
    
        public virtual ExamSetting ExamSetting { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionOrder> QuestionOrders { get; set; }
    }
}
