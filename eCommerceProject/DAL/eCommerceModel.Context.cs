﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbMyOnlineShopEntities : DbContext
    {
        public dbMyOnlineShopEntities()
            : base("name=dbMyOnlineShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartStatu> CartStatus { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<MemberRole> MemberRoles { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role1> Roles1 { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }
        public virtual DbSet<SlideImage> SlideImages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
    }
}
