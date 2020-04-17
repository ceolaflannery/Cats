using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pets.Domain;

namespace Pets.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        public async Task<IEnumerable<Person>> GetPeopleAndTheirPets()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetStringAsync("http://agl-developer-test.azurewebsites.net/people.json");

                    return JsonConvert.DeserializeObject<IEnumerable<Person>>(result);
                }
            }
            catch (JsonSerializationException ex)
            {
                // Log issue detail
                throw new Exception("Issue serialising the dataset");
            }
            catch (Exception ex)
            {
                // Log issue detail
                throw new Exception("Unknow error happened while retrieving data");
            }
        }
    }
}
