<<<<<<< HEAD:src/Services/Balance/Balance.Infrastructure/EntityConfigurations/UserEntityTypeConfiguration.cs
﻿namespace FPTS.FIT.BDRD.Services.Balance.Infrastructure.EntityConfigurations;
=======
﻿namespace ECom.Services.Balance.Infrastructure.EntityConfigurations;
>>>>>>> bcad93d (change customer to balance service + validator behavior):src/Services/Customer/Customer.Infrastructure/EntityConfigurations/UserEntityTypeConfiguration.cs

class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> userConfiguration)
    {
        userConfiguration.ToTable("userbalance");

        userConfiguration.HasKey(u => u.Id);

        userConfiguration.Ignore(b => b.DomainEvents);

        userConfiguration.Property(u => u.Id);

        userConfiguration.Property(u => u.Name)
            .HasColumnName("User_Name");

        userConfiguration.Property(u => u.CreditLimit)
            .HasColumnName("Credit_Limit")
            .HasColumnType("decimal(18,0)");
    }
}
