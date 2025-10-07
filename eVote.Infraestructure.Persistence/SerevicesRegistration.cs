using eVote.Infraestructure.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace eVote.Infraestructure.Persistence
{
    public static class SerevicesRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<eVoteDbContext>(opt =>
            opt.UseSqlServer(connectionString,
            m => m.MigrationsAssembly(typeof(eVoteDbContext).Assembly.FullName))
            , ServiceLifetime.Transient);


        }
    }
}
