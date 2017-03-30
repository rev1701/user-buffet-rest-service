using DAL = LMS1701.USL.UBEAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS1701.USL.UBEAPI.Models
{
    public class Roster
    {
        public int UserID { get; set; }
        public int BatchID { get; set; }
        public int Status { get; set; }

        public virtual User User { get; set; }

        public virtual StatusType StatusType { get; set; }
    }
}