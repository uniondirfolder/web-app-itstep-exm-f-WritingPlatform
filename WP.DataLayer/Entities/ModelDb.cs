
using System.Data.Entity;

namespace WP.DataLayer.Entities
{
    public class ModelDb : DbContext
    {
        public ModelDb():base("name=WritingPlatformDb"){}
        public ModelDb(string connectionString):base(connectionString)
        {
            Database.SetInitializer(new ModelDbInitializer());
            ModelDb db = new ModelDb();
            db.Database.Initialize(true);
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(q => q.Works)
                .WithRequired(q => q.Genre)
                .HasForeignKey(q => q.GenreId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(q => q.Comments)
                .WithOptional(q => q.Users)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<User>()
                .HasMany(q=>q.Ratings)
                .WithOptional(q=>q.Users)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<User>()
                .HasMany(q => q.Works)
                .WithRequired(q => q.User)
                .HasForeignKey(q => q.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Work>()
                .HasMany(q => q.Comments)
                .WithOptional(q => q.Work)
                .HasForeignKey(q => q.WorkId);

            modelBuilder.Entity<Work>()
                .HasMany(q => q.Ratings)
                .WithOptional(q => q.Works)
                .HasForeignKey(q => q.WorkId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
