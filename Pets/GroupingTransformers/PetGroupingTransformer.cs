using System.Collections.Generic;
using System.Linq;
using Pets.Domain;

namespace Pets.GroupingTransformers
{
    public class PetGroupingTransformer : IPetGroupingTransformer
    {
        public IEnumerable<IGrouping<string, string>> GroupSpecifiedPetTypeByOwnersGender(IEnumerable<Person> ownersAndTheirPets, PetType petType)
        {
            return ownersAndTheirPets
                        .Where(x => x.HasPets)
                        .SelectMany(petOwner => petOwner.Pets, (petOwner, pet) => new { petOwner, pet })
                        .Where(ownerAndPet => ownerAndPet.pet.PetType == petType)
                        .Select(ownerAndPet =>
                                new
                                {
                                    OwnerGender = ownerAndPet.petOwner.Gender,
                                    Pet = ownerAndPet.pet.Name
                                }
                        )
                        .OrderBy(x => x.Pet)
                        .Distinct()
                        .GroupBy(x => x.OwnerGender, x => x.Pet);
        }
    }
}
