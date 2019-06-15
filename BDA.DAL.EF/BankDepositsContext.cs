using System;
using BDA.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BDA.DAL.EF
{
    public class BankDepositsContext : DbContext
    {
        public BankDepositsContext()
        {
        }

        public BankDepositsContext(DbContextOptions<BankDepositsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountOperation> AccountOperations { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<CurrencyConversion> CurrencyConversions { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<DepositTerm> DepositTerms { get; set; }
        public virtual DbSet<Depositor> Depositors { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<IssuingAuthorityLocality> IssuingAuthoritiesLocalities { get; set; }
        public virtual DbSet<IssuingAuthority> IssuingAuthorities { get; set; }
        public virtual DbSet<Locality> Localities { get; set; }
        public virtual DbSet<Passport> Passports { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberOperator> PhoneNumberOperators { get; set; }
        public virtual DbSet<PhoneNumberOperatorCode> PhoneNumberOperatorsCodes { get; set; }
        public virtual DbSet<Street> Streets { get; set; }      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("account_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(30, 4)");

                entity.Property(e => e.ClosingDate)
                    .HasColumnName("closing_date")
                    .HasColumnType("date");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency_id");

                entity.Property(e => e.OpeningDate)
                    .HasColumnName("opening_date")
                    .HasColumnType("date");               
            });

            modelBuilder.Entity<AccountOperation>(entity =>
            {
                entity.ToTable("account_operations", "dbo");

                entity.Property(e => e.Id).HasColumnName("account_operation_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(30, 4)");

                entity.Property(e => e.DateTime)
                    .HasColumnName("date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Type).HasColumnName("type_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountOperations)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_operations_account");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses", "dbo");

                entity.HasIndex(e => new { e.StreetId, e.House, e.Housing, e.Apartment })
                    .HasName("ak_street_house_housing_apartment")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("address_id");

                entity.Property(e => e.Apartment).HasColumnName("apartment");

                entity.Property(e => e.House).HasColumnName("house");

                entity.Property(e => e.Housing)
                    .HasColumnName("housing")
                    .HasMaxLength(25);

                entity.Property(e => e.StreetId).HasColumnName("street_id");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_addresses_streets");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contracts", "dbo");

                entity.HasIndex(e => e.AccountId)
                    .HasName("ak_contracts_account_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("contract_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.ConclusionDate)
                    .HasColumnName("conclusion_date")
                    .HasColumnType("date");

                entity.Property(e => e.DepositId).HasColumnName("deposit_id");

                entity.Property(e => e.DepositorId).HasColumnName("depositor_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.TerminationDate)
                    .HasColumnName("termination_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Contract)
                    .HasForeignKey<Contract>(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contracts_accounts");

                entity.HasOne(d => d.Deposit)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.DepositId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contracts_deposits");

                entity.HasOne(d => d.Depositor)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.DepositorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contracts_depositors");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contracts_employees");
            });

            modelBuilder.Entity<CurrencyConversion>(entity =>
            {
                entity.HasKey(e => new { e.Currency, e.Buy, e.AmountInNativeCurrencyPerUnit })
                    .HasName("pk_currency_conversion");

                entity.ToTable("currency_conversions", "dbo");

                entity.Property(e => e.Currency).HasColumnName("currency_id");

                entity.Property(e => e.Buy).HasColumnName("buy");

                entity.Property(e => e.AmountInNativeCurrencyPerUnit)
                    .HasColumnName("amount_in_native_currency_per_unit")
                    .HasColumnType("decimal(10, 5)");
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.ToTable("deposits", "dbo");

                entity.HasIndex(e => new { e.DepositName, e.PaymentPeriod, e.Currency, e.DepositTermId, e.Rate, e.AccountReplenishment })
                    .HasName("ak_deposit_payment_period_currency_id_term_rate")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("deposit_id");

                entity.Property(e => e.AccountReplenishment).HasColumnName("account_replenishment");

                entity.Property(e => e.Currency).HasColumnName("currency_id");

                entity.HasOne(d => d.DepositTerm)
                    .WithMany(p => p.Deposits)
                    .HasForeignKey(d => d.DepositTermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_deposits_deposit_terms");

                entity.Property(e => e.DepositName)
                    .IsRequired()
                    .HasColumnName("deposit")
                    .HasMaxLength(255);

                entity.Property(e => e.DepositTermId).HasColumnName("deposit_term_id");

                entity.Property(e => e.PaymentPeriod).HasColumnName("payment_period");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("decimal(4, 2)");                
            });

            modelBuilder.Entity<DepositTerm>(entity =>
            {
                entity.ToTable("deposit_terms", "dbo");

                entity.HasIndex(e => new { e.DaysAmount, e.MonthsAmount, e.YearsAmount })
                    .HasName("ak_deposit_term_days_months_years")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("deposit_term_id");

                entity.Property(e => e.DaysAmount).HasColumnName("days_amount");

                entity.Property(e => e.MonthsAmount).HasColumnName("months_amount");

                entity.Property(e => e.YearsAmount).HasColumnName("years_amount");
            });

            modelBuilder.Entity<Depositor>(entity =>
            {
                entity.ToTable("depositors", "dbo");

                entity.HasIndex(e => e.PassportId)
                    .HasName("ak_depositor_passport_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("depositor_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(254);

                entity.Property(e => e.PassportId).HasColumnName("passport_id");

                entity.Property(e => e.PhoneNumberId).HasColumnName("phone_number_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Depositors)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depositors_addresses");

                entity.HasOne(d => d.Passport)
                    .WithOne(p => p.Depositor)
                    .HasForeignKey<Depositor>(d => d.PassportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depositors_passport");

                entity.HasOne(d => d.PhoneNumber)
                    .WithMany(p => p.Depositors)
                    .HasForeignKey(d => d.PhoneNumberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depositors_phone_numbers");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees", "dbo");

                entity.HasIndex(e => e.PassportId)
                    .HasName("ak_employees_passport_id")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("employee_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(254);

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.PassportId).HasColumnName("passport_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumberId).HasColumnName("phone_number_id");

                entity.Property(e => e.Position).HasColumnName("position_id");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employees_addresses");

                entity.HasOne(d => d.Passport)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PassportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employees_passports");

                entity.HasOne(d => d.PhoneNumber)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PhoneNumberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employees_phone_numbers");
            });
           
            modelBuilder.Entity<IssuingAuthorityLocality>(entity =>
            {
                entity.HasKey(e => new { e.IssuingAuthorityId, e.LocalityId })
                    .HasName("pk_issuing_authorities_localities");

                entity.ToTable("issuing_authorities_localities", "dbo");

                entity.Property(e => e.IssuingAuthorityId).HasColumnName("issuing_authority_id");

                entity.Property(e => e.LocalityId).HasColumnName("issuing_authority_locality_id");

                entity.HasOne(d => d.IssuingAuthority)
                    .WithMany(p => p.IssuingAuthoritiesLocalities)
                    .HasForeignKey(d => d.IssuingAuthorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_issuing_authorities_localities_issuing_authorities");

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.IssuingAuthoritiesLocalities)
                    .HasForeignKey(d => d.LocalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_issuing_authorities_localities_localities");
            });

            modelBuilder.Entity<IssuingAuthority>(entity =>
            {
                entity.ToTable("issuing_authorities", "dbo");

                entity.HasIndex(e => e.Name)
                    .HasName("ak_issuing_authority")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("issuing_authority_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("issuing_authority")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Locality>(entity =>
            {
                entity.ToTable("localities", "dbo");

                entity.HasIndex(e => new { e.LocalityType, e.Region, e.LocalityName })
                    .HasName("ak_locality_type_region_locality")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("locality_id");

                entity.Property(e => e.LocalityName)
                    .IsRequired()
                    .HasColumnName("locality")
                    .HasMaxLength(255);

                entity.Property(e => e.LocalityType).HasColumnName("locality_type_id");

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasColumnName("region")
                    .HasMaxLength(255);               
            });
            
            modelBuilder.Entity<Passport>(entity =>
            {
                entity.ToTable("passports", "dbo");

                entity.HasIndex(e => e.IdentificationNumber)
                    .HasName("ak_identification_number")
                    .IsUnique();

                entity.HasIndex(e => e.Number)
                    .HasName("ak_number")
                    .IsUnique();

                entity.HasIndex(e => new { e.FirstName, e.MiddleName, e.LastName, e.BirthDate, e.Gender, e.IssueDate })
                    .HasName("ak_first_name_middle_name_last_name_birth_date_gender_issue_date")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("passport_id");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.CitizenshipId).HasColumnName("citizenship_id");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("expiration_date")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdentificationNumber)
                    .IsRequired()
                    .HasColumnName("identification_number")
                    .HasMaxLength(20);

                entity.Property(e => e.IssueDate)
                    .HasColumnName("issue_date")
                    .HasColumnType("date");

                entity.Property(e => e.IssuingAuthorityId).HasColumnName("issuing_authority_id");

                entity.Property(e => e.IssuingAuthorityLocalityId).HasColumnName("issuing_authority_locality_id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(20);
                
                entity.HasOne(d => d.IssuingAuthorityLocality)
                    .WithMany(p => p.Passports)
                    .HasForeignKey(d => new { d.IssuingAuthorityId, d.IssuingAuthorityLocalityId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_passports_issuing_authorities_localities");
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.ToTable("phone_numbers", "dbo");

                entity.HasIndex(e => new { e.OperatorId, e.OperatorCode, e.Number })
                    .HasName("ak_operator_operator_code_phone_number")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("phone_number_id");

                entity.Property(e => e.OperatorCode).HasColumnName("operator_code");

                entity.Property(e => e.OperatorId).HasColumnName("operator_id");

                entity.Property(e => e.Number).HasColumnName("phone_number");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_phone_numbers_phone_number_operators");
            });

            modelBuilder.Entity<PhoneNumberOperator>(entity =>
            {
                entity.ToTable("phone_number_operators", "dbo");

                entity.HasIndex(e => e.Operator)
                    .HasName("ak_phone_number_operator")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("phone_number_operator_id");

                entity.Property(e => e.Operator)
                    .IsRequired()
                    .HasColumnName("phone_number_operator")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PhoneNumberOperatorCode>(entity =>
            {
                entity.HasKey(e => new { e.PhoneNumberOperatorId, e.Code })
                    .HasName("pk_phone_number_operators_codes");

                entity.ToTable("phone_number_operators_codes", "dbo");

                entity.Property(e => e.PhoneNumberOperatorId).HasColumnName("phone_number_operator_id");

                entity.Property(e => e.Code).HasColumnName("phone_number_operator_code");

                entity.HasOne(d => d.PhoneNumberOperator)
                    .WithMany(p => p.PhoneNumberOperatorsCodes)
                    .HasForeignKey(d => d.PhoneNumberOperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_phone_number_operators_codes_phone_number_operator");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("streets", "dbo");

                entity.HasIndex(e => new { e.LocalityId, e.StreetName, e.Postcode })
                    .HasName("ak_locality_street_postcode")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("street_id");

                entity.Property(e => e.LocalityId).HasColumnName("locality_id");

                entity.Property(e => e.Postcode).HasColumnName("postcode");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Locality)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.LocalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_streets_locality");
            });        
        }
    }
}