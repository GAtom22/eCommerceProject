//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCommerceProject.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberRole
    {
        public int MemberRoleId { get; set; }
        public Nullable<long> MemberId { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Role1 Role { get; set; }
    }
}
