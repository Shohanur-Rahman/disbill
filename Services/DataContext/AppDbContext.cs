using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.APIModels;
using Services.Domain.dishbill;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Services.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AreaList> AreaList { get; set; }
        public virtual DbSet<GRAHOK_TABLE> GRAHOK_TABLE { get; set; }
        public virtual DbSet<BillHistoryTable> BillHistoryTable { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<APIKeysTable> APIKeysTable { get; set; }
        public virtual DbSet<BillPaidTable> BillPaidTable { get; set; }
        public virtual DbSet<GrahokAreaMapper> GrahokAreaMapper { get; set; }
        public virtual DbSet<LiveChatMessages> LiveChatMessages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}