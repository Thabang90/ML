using Microsoft.EntityFrameworkCore;
using QuestionExplorer.Admins.Mappings;
using QuestionExplorer.Entities;
using QuestionExplorer.Questions.Mappings;
using QuestionExplorer.Users.Mappings;

namespace QuestionExplorer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Question> Questions { get; set; }

        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = this.Add(entity);
            entry.State = EntityState.Added;
            await this.SaveChangesAsync();
            return entry?.Entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = this.Update(entity);
            entry.State = EntityState.Modified;
            await this.SaveChangesAsync();
            return entry?.Entity;
        }

        public async Task<TEntity> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = this.Remove(entity);
            entry.State = EntityState.Deleted;
            await this.SaveChangesAsync();
            return entry?.Entity;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AdminMap());
            modelBuilder.ApplyConfiguration(new QuestionMap());
        }

    }
}
