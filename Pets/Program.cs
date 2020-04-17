using System;
using Microsoft.Extensions.DependencyInjection;
using Pets.DataAccess;
using Pets.Formatters;
using Pets.GroupingTransformers;
using Newtonsoft.Json;

namespace Pets
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        
        static void Main(string[] args)
        {
            RegisterServices();

            var service = _serviceProvider.GetService<IPetService>();

            var petDetails = service.GetPetDetails();
            Console.Write(petDetails.Result);
            
            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IPetService, PetService>();
            collection.AddScoped<IPetGroupingTransformer, PetGroupingTransformer>();
            collection.AddScoped<IOutputFormatter, OutputFormatter>(); 
            collection.AddScoped<IPersonRepository, PersonRepository>();
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
