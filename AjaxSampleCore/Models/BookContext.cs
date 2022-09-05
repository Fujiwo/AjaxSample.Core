// 最初にコマンドラインで下記を実行
// dotnet ef migrations add InitialCreate
// dotnet ef database update

using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AjaxSampleCore.Models
{
    public class BookContext : DbContext
    {
        public BookContext()
        {}

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {}

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<Author>(entity => { entity.ToTable("Author");

                entity.Property(e => e.Name).IsRequired();

                entity.HasMany(e => e.Books)
                      .WithMany(e => e.Authors)
                      .UsingEntity(j => j.ToTable("Writing"));
            });

            modelBuilder.Entity<Book>(entity => {
                entity.HasKey(e => e.Code);

                entity.ToTable("Book");

                entity.Property(e => e.Code)
                      .HasMaxLength(10)
                      .IsUnicode(false)
                      .IsFixedLength(true);

                entity.Property(e => e.PublisherCode)
                      .HasMaxLength(10)
                      .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Publisher)
                      .WithMany(p => p.Books)
                      .HasForeignKey(d => d.PublisherCode)
                      .HasConstraintName("FK_Book_Publisher");
            });

            modelBuilder.Entity<Publisher>(entity => {
                entity.HasKey(e => e.Code);

                entity.ToTable("Publisher");

                entity.Property(e => e.Code)
                      .HasMaxLength(10)
                      .IsUnicode(false);

                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
