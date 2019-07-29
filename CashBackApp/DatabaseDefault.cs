using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CashBackApp.Api
{
    public class DatabaseDefault : IHostedService
    {
        private Task _executingTask;
        private IServiceProvider _serviceProvider;

        private string _spotifyClientID;
        private string _spotifyPassword;

        public DatabaseDefault(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;

            _spotifyClientID = configuration["SpotifyAuth:ClientID"];
            _spotifyPassword = configuration["SpotifyAuth:Password"];
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = InitializeDatabaseAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                var companyId = await CreateCompany();
                await CreateCustomers();
                await CreateDisks();

                if (companyId != Guid.Empty)
                {
                    await CreateCashbackSettings(companyId);
                }
            }
            catch (Exception ex)
            {
                //gravar log
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<Guid> CreateCompany()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _companyServiceAsync = scope.ServiceProvider.GetService<ICompanyServiceAsync>();
                    var companyId = Guid.NewGuid();

                    await _companyServiceAsync.AddAsync(new Domain.Entities.Company { Id = companyId, Name = "E-commerce Discos Vinil" });
                    await _companyServiceAsync.CommitAsync();

                    return companyId;
                }
            }
            catch (Exception ex)
            {
                //gravar log
                Console.WriteLine(ex.Message);

                return Guid.Empty;
            }
        }

        private async Task CreateCustomers()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _customerServiceAsync = scope.ServiceProvider.GetService<ICustomerServiceAsync>();

                    await _customerServiceAsync.AddAsync(new Domain.Entities.Customer { Id = Guid.NewGuid(), Name = "Wellington Kolenyak Gomes" });
                    await _customerServiceAsync.AddAsync(new Domain.Entities.Customer { Id = Guid.NewGuid(), Name = "Heitor Fachini Gomes" });
                    await _customerServiceAsync.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                //gravar log
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateDisks()
        {
            try
            {
                Integration.Spotify.DisksService _spotifyDiskService = new Integration.Spotify.DisksService(_spotifyClientID, _spotifyPassword);
                foreach (var genre in Enum.GetValues(typeof(GenreEnum)))
                {
                    var listOfDisks = _spotifyDiskService.GetDisksByGenre(50, (GenreEnum)genre);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _productServiceAsync = scope.ServiceProvider.GetService<IProductServiceAsync>();
                        await _productServiceAsync.AddAsync(listOfDisks);
                        await _productServiceAsync.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                //gravar log
                Console.WriteLine(ex.Message);
            }            
        }

        private async Task CreateCashbackSettings(Guid companyId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {               
                var _cashbackSettingsServiceAsync = scope.ServiceProvider.GetService<ICashbackSettingsServiceAsync>();
                await _cashbackSettingsServiceAsync.AddAsync(GenerateCashbackPOP(companyId));
                await _cashbackSettingsServiceAsync.AddAsync(GenerateCashbackMPB(companyId));
                await _cashbackSettingsServiceAsync.AddAsync(GenerateCashbackCLASSIC(companyId));
                await _cashbackSettingsServiceAsync.AddAsync(GenerateCashbackROCK(companyId));

                await _cashbackSettingsServiceAsync.CommitAsync();
            }
        }

        private List<CashbackSettings> GenerateCashbackPOP(Guid companyId)
        {
            List<CashbackSettings> cashbacksList = new List<CashbackSettings>();
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Sunday, Percentage = 25 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Monday, Percentage = 7 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Tuesday, Percentage = 6 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Wednesday, Percentage = 2 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Thursday, Percentage = 10 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Friday, Percentage = 15 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.POP, DayOfWeek = DayOfWeek.Saturday, Percentage = 20 });

            return cashbacksList;
        }

        private List<CashbackSettings> GenerateCashbackMPB(Guid companyId)
        {
            List<CashbackSettings> cashbacksList = new List<CashbackSettings>();
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Sunday, Percentage = 30 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Monday, Percentage = 5 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Tuesday, Percentage = 10 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Wednesday, Percentage = 15 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Thursday, Percentage = 20 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Friday, Percentage = 25 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.MPB, DayOfWeek = DayOfWeek.Saturday, Percentage = 30 });

            return cashbacksList;
        }

        private List<CashbackSettings> GenerateCashbackCLASSIC(Guid companyId)
        {
            List<CashbackSettings> cashbacksList = new List<CashbackSettings>();
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Sunday, Percentage = 35 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Monday, Percentage = 3 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Tuesday, Percentage = 5 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Wednesday, Percentage = 8 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Thursday, Percentage = 13 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Friday, Percentage = 18 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.CLASSIC, DayOfWeek = DayOfWeek.Saturday, Percentage = 25 });

            return cashbacksList;
        }

        private List<CashbackSettings> GenerateCashbackROCK(Guid companyId)
        {
            List<CashbackSettings> cashbacksList = new List<CashbackSettings>();
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Sunday, Percentage = 40 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Monday, Percentage = 10 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Tuesday, Percentage = 15 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Wednesday, Percentage = 15 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Thursday, Percentage = 15 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Friday, Percentage = 20 });
            cashbacksList.Add(new CashbackSettings { Id = Guid.NewGuid(), CompanyId = companyId, Active = true, Genre = GenreEnum.ROCK, DayOfWeek = DayOfWeek.Saturday, Percentage = 40 });

            return cashbacksList;
        }
    }
}
