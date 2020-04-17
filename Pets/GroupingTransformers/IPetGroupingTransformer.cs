using System.Collections.Generic;
using System.Linq;
using Pets.Domain;

namespace Pets.GroupingTransformers
{
    public interface IPetGroupingTransformer
    {
        IEnumerable<IGrouping<string, string>> GroupSpecifiedPetTypeByOwnersGender(IEnumerable<Person> ownersAndTheirPets, PetType petType);
    }
}