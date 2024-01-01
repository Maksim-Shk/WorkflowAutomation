using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Persistence.EntityTypeConfigurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        //builder.HasKey(note => note.Id);
        //builder.HasIndex(note => note.Id).IsUnique();
        //builder.Property(note => note.Title).HasMaxLength(250);
    }
}
