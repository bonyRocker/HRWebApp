//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class A_JournalEntry
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string ReferenceNo { get; set; }
        public string FolioNumber { get; set; }
        public string TransactionType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> TransactionId { get; set; }
        public string Description { get; set; }
    }
}