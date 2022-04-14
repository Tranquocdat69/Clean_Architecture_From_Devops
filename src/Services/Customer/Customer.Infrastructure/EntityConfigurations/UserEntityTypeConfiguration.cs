﻿namespace ECom.Services.Customer.Infrastructure.EntityConfigurations;

class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> userConfiguration)
    {
        userConfiguration.ToTable("user", CustomerDbContext.DEFAULT_SCHEMA);

        userConfiguration.HasKey(u => u.Id);

        userConfiguration.Ignore(b => b.DomainEvents);

        userConfiguration.Property(u => u.Id);

        userConfiguration.Property(u => u.Name)
            .HasColumnName("User_Name");

        userConfiguration.Property(u => u.CreditLimit)
            .HasColumnName("Credit_Limit");
    }
}