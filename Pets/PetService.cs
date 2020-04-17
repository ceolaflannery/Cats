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
        public IPetGroupingTransformer _petGroupingTransformer;
        private readonly IOutputFormatter _stringFormatter;

        public PetService(IPersonRepository repository, IPetGroupingTransformer petGroupingTransformer, IOutputFormatter stringFormatter)
        {
            _repository = repository;
            _petGroupingTransformer = petGroupingTransformer;
            _stringFormatter = stringFormatter;
        }

        public async Task<string> GetPetDetails()
        {
            var peopleAndTheirPets = await _repository.GetPeopleAndTheirPets();

            var catsByOwnersGender = _petGroupingTransformer.GroupSpecifiedPetTypeByOwnersGender(peopleAndTheirPets, PetType.Cat);

            return _stringFormatter.FormatAsHeaderAndSubPoints(catsByOwnersGender);
        }
    }
}
