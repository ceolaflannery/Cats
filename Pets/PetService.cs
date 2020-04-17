using System;
using System.Threading.Tasks;
using Pets.DataAccess;
using Pets.Domain;
using Pets.Formatters;
using Pets.GroupingTransformers;

namespace Pets
{
    public class PetService : IPetService
    {
        private readonly IPersonRepository _repository;
        private readonly IPetGroupingTransformer _petGroupingTransformer;
        private readonly IOutputFormatter _stringFormatter;

        public PetService(IPersonRepository repository, IPetGroupingTransformer petGroupingTransformer, IOutputFormatter stringFormatter)
        {
            _repository = repository;
            _petGroupingTransformer = petGroupingTransformer;
            _stringFormatter = stringFormatter;
        }

        public async Task<string> GetPetDetails()
        {
            try
            {
                var peopleAndTheirPets = await _repository.GetPeopleAndTheirPets();
                if (peopleAndTheirPets == null)
                    return "No people retrieved";

                var catsByOwnersGender = _petGroupingTransformer.GroupSpecifiedPetTypeByOwnersGender(peopleAndTheirPets, PetType.Cat);

                return _stringFormatter.FormatAsHeaderAndSubPoints(catsByOwnersGender);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
