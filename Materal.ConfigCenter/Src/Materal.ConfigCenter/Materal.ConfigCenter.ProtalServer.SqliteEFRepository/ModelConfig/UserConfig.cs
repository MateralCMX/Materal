﻿using Materal.ConfigCenter.ProtalServer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.ModelConfig
{
    internal sealed class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}