using System.Collections.Generic;
using System.Threading.Tasks;
using Pets.Domain;

namespace Pets.DataAccess
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPeopleAndTheirPets();
    }
}