
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingChargeCalculator.Calculator;
using ParkingChargeCalculator.Strategies;
using ParkingChargeCalculator.Strategies.Interfaces;

namespace ParkingChargeCalculator
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IParkingCostCalculator, ParkingCostCalculator>();
            services.AddSingleton<IParkingChargeStrategy, ShortStayStrategy>();
            services.AddSingleton<IParkingChargeStrategy, LongStayStrategy>();
        }
    }
}
