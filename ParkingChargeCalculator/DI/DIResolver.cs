using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ParkingChargeCalculator.DI
{
    public class DIResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DIResolver()
        {
            IServiceCollection services = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        public TInterface Get<TInterface>()
        {
            return _serviceProvider.GetService<TInterface>();
        }
        
        public TInterface Get<TInterface, TConcreateType>() where TConcreateType : class
        {
            return _serviceProvider
                .GetServices<TInterface>()
                .FirstOrDefault(service => service.GetType() == typeof(TConcreateType));
        }
    }
}
