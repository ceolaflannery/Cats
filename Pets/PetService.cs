using System;
using System.Linq;
using System.Threading.Tasks;
using Pets.DataAccess;
using Pets.DataProcessing;
using Pets.Domain;
using Pets.Formatters;

namespace Pets
{
    public class PetService : IPetService
    {
        private readonly IPersonRepository _repository;
        private readonly IProcessor _processor;
        private readonly IOutputFormatter _outputFormatter;

        public PetService(IPersonRepository repository, IProcessor processor, IOutputFormatter outputFormatter)
        {
            _repository = repository;
            _processor = processor;
            _outputFormatter = outputFormatter;
        }

        public async Task<string> GetPetDetails()
        {
            try
            {
                var people = await _repository.GetPeople();
                if (people == null)
                    return "No people found";

                var catsByOwnersGender = _processor.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Cat);
                if (catsByOwnersGender == null || catsByOwnersGender.Count() == 0)
                    return "No cats found";

                return _outputFormatter.FormatAsHeaderAndSubPoints(catsByOwnersGender);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
