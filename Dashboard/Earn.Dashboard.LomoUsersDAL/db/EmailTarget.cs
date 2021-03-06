//
// Copyright (c) Microsoft Corporation. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.
//

// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>

namespace Earn.Dashboard.LomoUsersDAL.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailTarget
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailTarget()
        {
            this.EmailSchedules = new HashSet<EmailSchedule>();
            this.EmailSchedulesHistories = new HashSet<EmailSchedulesHistory>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsClo { get; set; }
        public string Description { get; set; }
        public string Query { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailSchedule> EmailSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailSchedulesHistory> EmailSchedulesHistories { get; set; }
    }
}