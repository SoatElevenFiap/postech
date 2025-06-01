﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Infra.Data.ModelConfiguration.Base
{
    public class EntityBaseModelConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : class, IEntity, IAuditable
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.CriadoEm)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("NOW()");

            builder.Property(c => c.ModificadoEm)
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("NOW()")
                   .ValueGeneratedOnAddOrUpdate();
        }
    }
}
