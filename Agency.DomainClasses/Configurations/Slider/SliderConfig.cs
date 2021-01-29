using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Agency.DomainClasses.Configurations.Slider
{
    public class SliderConfig : EntityTypeConfiguration<Entities.Slider.Slider>
    {
        public SliderConfig()
        {
            ToTable("Slider");
            Property(p => p.Describ).IsOptional().HasMaxLength(150);
            Property(p => p.Index)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Index") { IsUnique = true }));
            Property(p => p.UserId).IsRequired();
            Property(p => p.PicSrc).IsRequired().HasMaxLength(150);
            Property(p => p.Title).IsOptional().HasMaxLength(50);
            Property(p => p.Link).IsOptional().HasMaxLength(300);
            Property(p => p.ButtonTitle).IsOptional().HasMaxLength(50);
            Property(p => p.RowVersion).IsRowVersion();

            HasRequired(p=>p.User).WithMany(p=>p.Sliders).HasForeignKey(p=>p.UserId).WillCascadeOnDelete(false);
        }
    }
}
