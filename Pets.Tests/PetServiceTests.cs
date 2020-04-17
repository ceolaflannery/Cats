using System;
using System.Threading.Tasks;
using Pets.DataAccess;
using Pets.Formatters;
using Pets.GroupingTransformers;
using Xunit;
using Moq;
using System.Collections.Generic;
using Pets.Domain;
using System.Linq;
using Moq.Language.Flow;

namespace Pets.Tests
{
    public class PetServiceTests
    {
        private readonly Mock<IPersonRepository> _repository = new Mock<IPersonRepository>();
        private readonly Mock<IPetGroupingTransformer> _petGroupingTransformer = new Mock<IPetGroupingTransformer>();
        private readonly Mock<IOutputFormatter> _stringFormatter = new Mock<IOutputFormatter>();
        private readonly PetService sut;

        public PetServiceTests()
        {
            sut = new PetService(_repository.Object, _petGroupingTransformer.Object, _stringFormatter.Object);
        }

        [Fact]
        public void GetPetDetails_WhenRepositoryReturnsValidResponse_ShouldReturnTransformedAndFormattedResponse()
        {
            var people = OnePersonOnePet();
            var group = GroupsOfPetNamesByGender();

            SetupPersonRepositoryToReturnValid(people);
            SetupTransformerToReturnValidGrouping(people, group);

            var result = sut.GetPetDetails();

            _petGroupingTransformer.Verify(x => x.GroupSpecifiedPetTypeByOwnersGender(people, PetType.Cat), Times.Once);
            _stringFormatter.Verify(x => x.FormatAsHeaderAndSubPoints(group), Times.Once);
        }

        private void SetupTransformerToReturnValidGrouping(List<Person> people, IEnumerable<IGrouping<string, string>> group) =>
            _petGroupingTransformer.Setup(x => x.GroupSpecifiedPetTypeByOwnersGender(people, PetType.Cat)).Returns(group);
        

        private static IEnumerable<IGrouping<string, string>> GroupsOfPetNamesByGender()
        {
            var petNames = new List<string> { "Trent", "Pigmy" };
            return petNames.GroupBy(o => "somestring");
        }

        private static List<Person> OnePersonOnePet() =>
                new List<Person> { new Person("F", new List<Pet> { new Pet("Rufus", PetType.Cat) }) };
        

        private IReturnsResult<IPersonRepository> SetupPersonRepositoryToReturnValid(List<Person> people) =>
                _repository.Setup(x => x.GetPeopleAndTheirPets())
                        .Returns(Task.FromResult<IEnumerable<Person>>(people));
        
    }
}
