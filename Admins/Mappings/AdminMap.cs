using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionExplorer.Entities;

namespace QuestionExplorer.Admins.Mappings
{
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("admins");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.Username).HasColumnName("username");
            builder.Property(o => o.Email).HasColumnName("email");
            builder.Property(o => o.Password).HasColumnName("password");
        }
    }
}
