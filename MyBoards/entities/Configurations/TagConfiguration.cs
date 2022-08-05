﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBoards.entities.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(new Tag() { Id = 1, Value = "Web" },
                    new Tag { Id = 2, Value = "UI" },
                    new Tag { Id = 3, Value = "Desktop" },
                    new Tag { Id = 4, Value = "API" },
                    new Tag { Id = 5, Value = "Service" });
        }
    }
}
