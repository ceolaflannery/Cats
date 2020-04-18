using System.Collections.Generic;
using System.Linq;
using Pets.Domain;

namespace Pets.DataProcessing
{
    public interface IProcessor
    {
        IEnumerable<IGrouping<string, string>> GroupPetNamesByOwnersGenderForSpecifiedPetType(IEnumerable<Person> ownersAndTheirPets, PetType petType);
    }
}