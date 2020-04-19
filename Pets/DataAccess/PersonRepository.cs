using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pets.Domain;
using Pets.Helpers;

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
            catch (JsonSerializationException ex)
            {
                throw LoggingHelper.LogErrorAndCreateException<JsonSerializationException>("Issue serialising the dataset", ex);
            }
            catch (Exception ex)
            {
                throw LoggingHelper.LogErrorAndCreateException<Exception>("Unknow error happened while retrieving data", ex);
            }
        }
    }
}
