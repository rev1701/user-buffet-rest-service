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
    
    public partial class QuestionAnswer
    {
        public int QuestionOrderID { get; set; }
        public string Answer { get; set; }
    
        public virtual QuestionOrder QuestionOrder { get; set; }
    }
}