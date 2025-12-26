using FT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Clave primaria
            builder.HasKey(u => u.Id);

            // Username
            builder.Property(u => u.Username)
                   .HasMaxLength(50)
                   .IsRequired();

            // Email
            builder.Property(u => u.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            // PasswordHash
            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(255)  // Para almacenar hash seguro
                   .IsRequired();

            // Profile como enum
            builder.Property(u => u.Profile)
                   .HasConversion<int>()  // Guarda el enum como int
                   .IsRequired();

            // Estado como enum
            builder.Property(u => u.State)
                   .HasConversion<int>()
                   .IsRequired();

            // Fecha de creación
            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            // Índice único para username y email
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
