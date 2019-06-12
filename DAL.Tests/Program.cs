using BDA.Core;
using BDA.DAL.EF;
using BDA.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DAL.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //DbUpdateConcurrencyException
            //CheckDbUpdateConcurrencyExceptionWhenUpdateModifiedEntity();

            //DbUpdateConcurrencyException
            //CheckDbUpdateConcurrencyExceptionWhenUpdateDeletedEntity();

            // DbUpdateConcurrencyException
            //CheckDbUpdateConcurrencyExceptionWhenDeleteDeletedEntity();

            // DbUpdateConcurrencyException
            //CheckDbUpdateConcurrencyExceptionWithDifferentUnitOfWorkAndContext();

            UpdateUseCase();
        }

        static void CheckDbUpdateConcurrencyExceptionWhenUpdateModifiedEntity()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankDepositsDatabase"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<BankDepositsContext>()
                .UseSqlServer(connectionString);

            using (var context = new BankDepositsContext(optionsBuilder.Options))
            {
                var rand = new Random();

                var unitOfWork = new UnitOfWork(context);
                IAddressRepository addressRepository = new AddressRepository(unitOfWork);

                var address = new Address();
                addressRepository.Create(address);
                address.Apartment = 5;
                address.House = 10;
                address.Housing = "A";
                address.Street = new Street()
                {
                    Locality = new Locality()
                    {
                        LocalityName = "Partizanski" + rand.NextDouble(),
                        LocalityType = LocalityType.City,
                        Region = "Minsk"
                    },
                    StreetName = "Frolikova",
                    Postcode = 220037
                };

                addressRepository.Commit().Wait();

                var addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);

                address.Housing = "OLD";
                unitOfWork.ExecuteQuery("UPDATE dbo.Addresses SET housing = 'NEW' where address_id = " + address.Id).Wait();

                addressRepository.Update(address);

                addressRepository.Commit().Wait();

                addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);
            }

            Console.ReadLine();
        }

        static void CheckDbUpdateConcurrencyExceptionWhenUpdateDeletedEntity()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankDepositsDatabase"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<BankDepositsContext>()
                .UseSqlServer(connectionString);

            using (var context = new BankDepositsContext(optionsBuilder.Options))
            {
                var rand = new Random();

                var unitOfWork = new UnitOfWork(context);
                IAddressRepository addressRepository = new AddressRepository(unitOfWork);

                var address = new Address();
                addressRepository.Create(address);
                address.Apartment = 5;
                address.House = 10;
                address.Housing = "A";
                address.Street = new Street()
                {
                    Locality = new Locality()
                    {
                        LocalityName = "Partizanski" + rand.NextDouble(),
                        LocalityType = LocalityType.City,
                        Region = "Minsk"
                    },
                    StreetName = "Frolikova",
                    Postcode = 220037
                };

                addressRepository.Commit().Wait();

                var addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);

                address.Housing = "OLD";
                unitOfWork.ExecuteQuery("Delete from dbo.Addresses where address_id = " + address.Id).Wait();

                addressRepository.Update(address);

                addressRepository.Commit().Wait();

                addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);
            }

            Console.ReadLine();
        }

        static void CheckDbUpdateConcurrencyExceptionWhenDeleteDeletedEntity()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankDepositsDatabase"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<BankDepositsContext>()
                .UseSqlServer(connectionString);

            using (var context = new BankDepositsContext(optionsBuilder.Options))
            {
                var rand = new Random();

                var unitOfWork = new UnitOfWork(context);
                IAddressRepository addressRepository = new AddressRepository(unitOfWork);

                var address = new Address();
                addressRepository.Create(address);
                address.Apartment = 5;
                address.House = 10;
                address.Housing = "A";
                address.Street = new Street()
                {
                    Locality = new Locality()
                    {
                        LocalityName = "Partizanski" + rand.NextDouble(),
                        LocalityType = LocalityType.City,
                        Region = "Minsk"
                    },
                    StreetName = "Frolikova",
                    Postcode = 220037
                };

                addressRepository.Commit().Wait();

                var addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);

                unitOfWork.ExecuteQuery("Delete from dbo.Addresses where address_id = " + address.Id).Wait();

                addressRepository.Delete(address);

                addressRepository.Commit().Wait();

                addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);
            }

            Console.ReadLine();
        }

        static void CheckDbUpdateConcurrencyExceptionWithDifferentUnitOfWorkAndContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankDepositsDatabase"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<BankDepositsContext>()
                .UseSqlServer(connectionString);

            using (var context1 = new BankDepositsContext(optionsBuilder.Options))
            {
                var rand = new Random();

                var unitOfWork = new UnitOfWork(context1);
                IAddressRepository addressRepository = new AddressRepository(unitOfWork);

                var address = new Address();
                addressRepository.Create(address);
                address.Apartment = 5;
                address.House = 10;
                address.Housing = "A";
                address.Street = new Street()
                {
                    Locality = new Locality()
                    {
                        LocalityName = "Partizanski" + rand.NextDouble(),
                        LocalityType = LocalityType.City,
                        Region = "Minsk"
                    },
                    StreetName = "Frolikova",
                    Postcode = 220037
                };

                addressRepository.Commit().Wait();

                var addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);

                using (var context2 = new BankDepositsContext(optionsBuilder.Options))
                {
                    var unitOfWork2 = new UnitOfWork(context2);
                    unitOfWork2.ExecuteQuery("Delete from dbo.Addresses where address_id = " + address.Id).Wait();
                }

                addressRepository.Delete(address);

                addressRepository.Commit().Wait();

                addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);
            }

            Console.ReadLine();
        }

        static void UpdateUseCase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BankDepositsDatabase"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<BankDepositsContext>()
                .UseSqlServer(connectionString);

            using (var context = new BankDepositsContext(optionsBuilder.Options))
            {
                var rand = new Random();

                var unitOfWork = new UnitOfWork(context);
                IAddressRepository addressRepository = new AddressRepository(unitOfWork);

                var address = new Address();
                addressRepository.Create(address);
                address.Apartment = 5;
                address.House = 10;
                address.Housing = "A";
                address.Street = new Street()
                {
                    Locality = new Locality()
                    {
                        LocalityName = "Partizanski" + rand.NextDouble(),
                        LocalityType = LocalityType.City,
                        Region = "Minsk"
                    },
                    StreetName = "Frolikova",
                    Postcode = 220037
                };

                addressRepository.Commit().Wait();

                var addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);

                var adr = addressRepository.GetById(address.Id, a => a.Street).Result;
                context.Entry(adr).State = EntityState.Detached;
                adr.Housing = "OLD";
                adr.House = 0;

                addressRepository.Update(adr);

                addressRepository.Commit().Wait();

                addresses = addressRepository.GetBy(null, a => a.Street).ToListAsync().Result;
                PrintAddresses(addresses);
            }

            Console.ReadLine();
        }

        static void PrintAddresses(IEnumerable<Address> addresses)
        {            
            foreach (var a in addresses)
            {
                Console.WriteLine(a.Id + " " + a.Street.StreetName + " " + a.House + " " + a.Housing);
            }

            Console.WriteLine("---------------------------------------");
        }
    }
}
