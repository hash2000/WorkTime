//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkTime.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeNormOfMonth
    {
        public int Id { get; set; }
        public double Norm { get; set; }
        public int RegNormsId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    
        public virtual RegNorm RegNorm { get; set; }
    }
}