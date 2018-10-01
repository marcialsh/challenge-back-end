using System;
using BlogStarWars.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogStarWars.Infrastructure.Data.EntityFramework.Mappings
{
    public class PostMapping
    {
        public PostMapping(EntityTypeBuilder<Post> entityTypeBuilder)
        {
            entityTypeBuilder
                .ToTable("Post");
            
            entityTypeBuilder
                .HasKey(e => e.Id)
                .HasName("Id");
            entityTypeBuilder
                .Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("INTEGER")
                .ValueGeneratedOnUpdate()
                .IsRequired();
            
            entityTypeBuilder
                .Property(e => e.Titulo)
                .HasColumnName("Titulo")
                .HasColumnType("TEXT")
                .HasMaxLength(100)
                .IsRequired();
            
            entityTypeBuilder
                .Property(e => e.Descricao)
                .HasColumnName("Descricao")
                .HasColumnType("TEXT")
                .HasMaxLength(300)
                .IsRequired();
            
            entityTypeBuilder
                .Property(e => e.Conteudo)
                .HasColumnName("Conteudo")
                .HasColumnType("TEXT")
                .HasMaxLength(50000)
                .IsRequired();
            
            entityTypeBuilder
                .Property(e => e.QuantidadeLikes)
                .HasColumnName("QuantidadeLikes")
                .HasColumnType("INTEGER")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.QuantidadeViews)
                .HasColumnName("QuantidadeViews")
                .HasColumnType("INTEGER")
                .IsRequired();

            entityTypeBuilder
                .Property<bool>("EstaDeletado")
                .HasColumnType("INTEGER");
            entityTypeBuilder
                .Property<DateTime>("DataCriacao")
                .IsRequired();
        }
    }
}