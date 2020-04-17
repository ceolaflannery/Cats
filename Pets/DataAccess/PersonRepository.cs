using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Pets.Domain;

namespace Pets.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository() { }

        public async Task<IEnumerable<Person>> GetPeopleAndTheirPets()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync("http://agl-developer-test.azurewebsites.net/people.json");
                
                return JsonSerializer.Deserialize<IEnumerable<Person>>(result, GetDeserializeOptions());
            }
        }

        private static JsonSerializerOptions GetDeserializeOptions()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return options;
        }
    }
}
