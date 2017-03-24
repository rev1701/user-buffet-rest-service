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
    
    public partial class ExamSetting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamSetting()
        {
            this.ExamAssessments = new HashSet<ExamAssessment>();
        }
    
        public int ExamSettingsID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<int> LengthOfExamInMinutes { get; set; }
        public string ExamTemplateID { get; set; }
        public Nullable<bool> Editable { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> AllowedAttempts { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamAssessment> ExamAssessments { get; set; }
    }
}
