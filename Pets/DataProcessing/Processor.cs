using System;
using System.Collections.Generic;
using System.Linq;
using Pets.Domain;
using Pets.Helpers;

namespace Pets.DataProcessing
{
    public class Processor : IProcessor
    {
        public IEnumerable<IGrouping<string, string>> GroupPetNamesByOwnersGenderForSpecifiedPetType(IEnumerable<Person> people, PetType specifiedPetType)
        {
            ThrowExceptionIfArgumentsInvalid(people, specifiedPetType);

            try
            {
                return people
                    .Where(x => x.HasPets)
                    .SelectMany(petOwner => petOwner.Pets, (petOwner, pet) => new { petOwner, pet })
                    .Where(ownerAndPet => ownerAndPet.pet.PetType == specifiedPetType)
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
            catch (Exception ex)
            {
                throw LoggingHelper.LogErrorAndCreateException<Exception>("Error occured grouping pets by their owner's gender", ex);
            }
        }

        private static void ThrowExceptionIfArgumentsInvalid(IEnumerable<Person> people, PetType specifiedPetType)
        {
            if (people == null || !people.Any())
                throw LoggingHelper.LogErrorAndCreateException<ArgumentException>("Unable to group pet names when no owners have been specified");

            if (specifiedPetType == PetType.Unknown)
                throw LoggingHelper.LogErrorAndCreateException<ArgumentException>("Pet type must be specified");
        }
    }
}
