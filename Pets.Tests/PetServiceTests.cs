using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Moq.Language.Flow;
using Pets.DataAccess;
using Pets.DataProcessing;
using Pets.Domain;
using Pets.Formatters;
using Pets.Tests.Mocks;
using Xunit;

namespace Pets.Tests
{
    public class PetServiceTests
    {
        private readonly Mock<IPersonRepository> _repository = new Mock<IPersonRepository>();
        private readonly Mock<IProcessor> _processor = new Mock<IProcessor>();
        private readonly Mock<IOutputFormatter> _outputFormatter = new Mock<IOutputFormatter>();
        private readonly PetService sut;

        public PetServiceTests()
        {
            sut = new PetService(_repository.Object, _processor.Object, _outputFormatter.Object);
        }

        [Fact]
        public void GetPetDetails_WhenRepositoryReturnsValidResponse_ShouldReturnResponseOutputByFormatter()
        {
            var peopleReturnedFromRepo = ValidPeopleWithPets();
            var petNameGroupingsReturnedByProcessor = ValidGrouping();

            SetupPersonRepositoryToReturnValid(peopleReturnedFromRepo);
            SetupProcessorToReturnValidGrouping(peopleReturnedFromRepo, petNameGroupingsReturnedByProcessor);
            SetupFormatterToReturnValidGrouping(petNameGroupingsReturnedByProcessor, ValidOutputString);

            var result = sut.GetPetDetails();

            VerifyProcessorWasCalledWithReturnedFromRepoAndCatAsType(peopleReturnedFromRepo);
            VerifyFormatterWasCalledWithProcessorOutput(petNameGroupingsReturnedByProcessor);

            Assert.Equal(ValidOutputString, result.Result);
        }

        [Fact]
        public void GetPetDetails_WhenRepositoryThrowsException_ShouldReturnMessageABdNotReThrow()
        {
            var message = "This is an Exceptional message";
            SetupPersonRepositoryToThrowException(new Exception(message));

            var result = sut.GetPetDetails();

            Assert.Equal(message, result.Result);
        }

        [Fact]
        public void GetPetDetails_WhenRepositoryReturnsNull_ShouldReturnMessageAndNotCallProcessorAndFormatter()
        {
            SetupPersonRepositoryToReturnNull();

            var result = sut.GetPetDetails();

            VerifyRepositoryWasCalled();
            VerifyProcessorWasNotCalled();
            VerifyFormatterWasNotCalled();

            Assert.Equal("No people found", result.Result);
        }

        [Fact]
        public void GetPetDetails_WhenRepositoryReturnsEmptyList_ShouldReturnMessageAndNotCallProcessorAndFormatter()
        {
        }

        [Fact]
        public void GetPetDetails_WhenProcessorReturnsNull_ShouldReturnMessageAndNotCallFormatter()
        {

        }

        [Fact]
        public void GetPetDetails_WhenProcessorReturnsEmpty_ShouldReturnMessageAndNotCallFormatter()
        {

        }

        #region Setup Data
        private readonly string ValidOutputString = "Valid string";

        private static List<Person> ValidPeopleWithPets() =>
            new List<Person>
                {
                    new Person("F", new List<Pet> { new Pet("Rufus", PetType.Cat) }),
                    new Person("M", new List<Pet> { new Pet("Alexander", PetType.Cat), new Pet("Bongo", PetType.Dog) }),
                    new Person("M", new List<Pet> { new Pet("Rufus", PetType.Fish) })
                };

        private static IEnumerable<IGrouping<string, string>> ValidGrouping() =>
            new List<MockGrouping<string, string>>
                {
                    new MockGrouping<string, string>("M", new string[] { "Cruz", "Ellie", "rusty" }),
                    new MockGrouping<string, string>("F", new string[] { "Hulk" })
                };

        #endregion

        #region Setup Mock Calls
        private void SetupPersonRepositoryToReturnValid(List<Person> people) =>
            _repository.Setup(x => x.GetPeople()).Returns(Task.FromResult<IEnumerable<Person>>(people));

        private void SetupPersonRepositoryToReturnNull() =>
            _repository.Setup(x => x.GetPeople()).Returns(Task.FromResult<IEnumerable<Person>>(null));

        private void SetupPersonRepositoryToThrowException(Exception exception) =>
            _repository.Setup(x => x.GetPeople()).Throws(exception);

        private void SetupProcessorToReturnValidGrouping(List<Person> people, IEnumerable<IGrouping<string, string>> group) =>
            _processor.Setup(x => x.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Cat)).Returns(group);

        private void SetupFormatterToReturnValidGrouping(IEnumerable<IGrouping<string, string>> grouping, string output) =>
            _outputFormatter.Setup(x => x.FormatAsHeaderAndSubPoints(grouping)).Returns(output);
        #endregion

        #region Verify Mock Calls
        private void VerifyRepositoryWasCalled() =>
            _repository.Verify(x => x.GetPeople(), Times.Once);

        private void VerifyProcessorWasCalledWithReturnedFromRepoAndCatAsType(List<Person> people) =>
            _processor.Verify(x => x.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Cat), Times.Once);

        private void VerifyFormatterWasCalledWithProcessorOutput(IEnumerable<IGrouping<string, string>> group) =>
            _outputFormatter.Verify(x => x.FormatAsHeaderAndSubPoints(group), Times.Once);

        private void VerifyProcessorWasNotCalled() =>
            _processor.Verify(x => x.GroupPetNamesByOwnersGenderForSpecifiedPetType(It.IsAny<List<Person>>(), It.IsAny<PetType>()), Times.Never);

        private void VerifyFormatterWasNotCalled() =>
            _outputFormatter.Verify(x => x.FormatAsHeaderAndSubPoints(It.IsAny<IEnumerable<IGrouping<string, string>>>()), Times.Never);
        #endregion
    }
}
