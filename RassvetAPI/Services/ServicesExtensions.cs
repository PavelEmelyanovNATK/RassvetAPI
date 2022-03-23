using Microsoft.Extensions.DependencyInjection;
using RassvetAPI.Services.JwtToken;
using RassvetAPI.Services.AuthorizationService;
using RassvetAPI.Services.PasswordHasher;
using RassvetAPI.Services.RegistrationService;
using RassvetAPI.Services.RefreshTokensRepository;
using RassvetAPI.Services.ClientsRepository;
using RassvetAPI.Services.TrainingsRepository;
using RassvetAPI.Services.SectionsRepository;
using RassvetAPI.Services.TrenersRepository;
using RassvetAPI.Services.GroupsRepository;

namespace RassvetAPI.Services
{
    public static class ServicesExtensions
    { 
        public static IServiceCollection AddPasswordHasher(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher.PasswordHasher>();

        public static IServiceCollection AddAccessTokenGeneratorService(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<JwtAccessTokenGenerator>();

        public static IServiceCollection AddRefreshTokenGeneratorService(this IServiceCollection serviceCollection)
           => serviceCollection.AddSingleton<JwtRefreshTokenGenerator>();

        public static IServiceCollection AddRefreshTokenValidator(this IServiceCollection serviceCollection)
           => serviceCollection.AddSingleton<JwtRefreshTokenValidator>();

        public static IServiceCollection AddRegistrationService(this IServiceCollection serviceCollection)
            => serviceCollection.AddScoped<IRegistrationService, RegistrationService.RegistrationService>();

        public static IServiceCollection AddAuthService(this IServiceCollection serviceCollection)
            => serviceCollection.AddScoped<IAuthorizationService, AuthorizationService.AuthorizationService>();

        public static IServiceCollection AddRefreshTokensRepository(this IServiceCollection serviceCollection)
           => serviceCollection.AddScoped<IRefreshTokensRepository, RefreshTokensRepository.RefreshTokensRepository>();

        public static IServiceCollection AddClientsRepository(this IServiceCollection serviceCollection)
           => serviceCollection.AddScoped<IClientsRepository, ClientsRepository.ClientsRepository>();

        public static IServiceCollection AddClientTrainingsRepository(this IServiceCollection serviceCollection)
           => serviceCollection.AddScoped<ITrainingsRepository, TrainingsRepository.TrainingsRepository>();

        public static IServiceCollection AddSectionsRepository(this IServiceCollection serviceCollection)
           => serviceCollection.AddScoped<ISectionsRepository, SectionsRepository.SectionsRepository>();

        public static IServiceCollection AddTrenersRepository(this IServiceCollection serviceCollection)
           => serviceCollection.AddScoped<ITrenersRepository, TrenersRepository.TrenersRepository>();

        public static IServiceCollection AddGroupsRepository(this IServiceCollection serviceCollection)
           => serviceCollection.AddScoped<IGroupsRepository, GroupsRepository.GroupsRepository>();
    }
}
