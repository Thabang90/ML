using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionExplorer.Entities;

namespace QuestionExplorer.Users.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.Username).HasColumnName("username");
            builder.Property(o => o.Email).HasColumnName("email");
            builder.Property(o => o.Password).HasColumnName("password");
            builder.Property(o => o.AdminId).HasColumnName("admin_id");
        }
    }
}
