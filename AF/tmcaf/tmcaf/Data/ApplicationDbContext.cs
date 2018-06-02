using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

using MediaContentHSE.Models;

namespace MediaContentHSE.Data
{

    public class RecordsContext : DbContext
    {
        public RecordsContext() : base("Server=tcp:mediacontent.database.windows.net,1433;Initial Catalog=mediacontentdb;Persist Security Info=False;User ID=admindb;Password=Admin_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=True")
        {
            Database.SetInitializer<RecordsContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MediaContent> MediaContents { get; set; }
        public DbSet<TargetGroup> TargetGroups { get; set; }
        public DbSet<TargetMediaContent> TargetMediaContents { get; set; }
        public DbSet<TargetMediaContentInterface> TargetMediaContentInterfaces { get; set; }
    }
}
