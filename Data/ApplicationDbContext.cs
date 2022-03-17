using BlazorControlCefa.Data.Common;
using BlazorControlCefa.Data.Entities;
using BlazorControlCefa.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorControlCefa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IDateTimeService _dateTime;        
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            AuthenticationStateProvider authenticationStateProvider
            )
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;            
            _authenticationStateProvider = authenticationStateProvider;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<PersonType> PersonType { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleDetail> VehicleDetails { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitType> VisitTypes { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<VisitPersonDetail> VisitPersonDetails { get; set; }        
        public DbSet<VisitVehicleDetail> VisitVehicleDetails { get; set; }        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");

            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Visit>()
                .HasOne(s => s.PersonApprovers)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<Visit>()
                .HasOne(s => s.PersonVisitTos)
                .WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Visit>()
                .HasOne(s => s.Reasons)
                .WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Visit>()
                .HasOne(s => s.Departments)
                .WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Visit>()
                .HasOne(s => s.VisitTypes)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    //case EntityState.Detached:
                    //    break;
                    //case EntityState.Unchanged:
                    //    break;
                    case EntityState.Deleted:
                        //SoftDelete
                        entry.State = EntityState.Unchanged;
                        entry.Entity.DeletedDate = _dateTime.Now;
                        //entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        entry.Entity.LastModifiedBy = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

                        entry.Entity.IsActive = false;
                        entry.Entity.IsDeleted = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        //entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        entry.Entity.LastModifiedBy = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.Now;
                        //entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        entry.Entity.CreatedBy = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;
                        //entry.Entity.CreatedBy = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

                        entry.Entity.IsActive = true;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}