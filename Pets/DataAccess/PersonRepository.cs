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
        private readonly string Url = "http://agl-developer-test.azurewebsites.net/people.json";

        public async Task<IEnumerable<Person>> GetPeople()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetStringAsync(Url);

                    return JsonConvert.DeserializeObject<IEnumerable<Person>>(result);
                }
            }
            catch (JsonSerializationException)
            {
                // Log issue detail
                throw new Exception("Issue serialising the dataset");
            }
            catch (Exception)
            {
                // Log issue detail
                throw new Exception("Unknow error happened while retrieving data");
            }
        }
    }
}
