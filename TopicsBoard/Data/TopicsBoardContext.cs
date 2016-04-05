using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace TopicsBoard.Data
{
    public class TopicsBoardContext : DbContext
    {
        public TopicsBoardContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            //Using Code First Migrations

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TopicsBoardContext,TopicsBoardMigrationsConfiguration>());
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<FileUpload> FileUploads { get; set; }

        //This is created as part of New Mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //   modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            /*modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();*/
            /*modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

              modelBuilder.Entity<Topic>()
                  .HasRequired(f => f.FileUploads)
                  .WithRequiredDependent()
                  .WillCascadeOnDelete(false);*/
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace))
           .Where(type => type.BaseType != null && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);  

        }
    }
}