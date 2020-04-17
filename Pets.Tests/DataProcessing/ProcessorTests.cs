using System;
using System.Collections.Generic;
using System.Linq;
using Pets.Domain;
using Pets.DataProcessing;
using Xunit;
using Pets.Tests.Mocks;

namespace Pets.Tests.GroupingTransformers
{
    public class ProcessorTests
    {
        private readonly Processor sut;

        public ProcessorTests()
        {
            sut = new Processor();
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenMultiplePeopleHavePetType_ShouldReturnPetNamesGroupedByOwnersGender()
        {
            List<Person> people = PeopleWithCats;

            var result = sut.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Cat);

            Assert.Equal(ValidGrouping(), result);
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenNoPersonHasPets_ShouldReturnEmpty()
        {
            List<Person> people = PersonWithNoPet;

            var result = sut.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Dog);

            Assert.Equal(EmptyGroup(), result);
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenPersonHasPetsButNotTheSpecifiedType_ShouldThrowException()
        {
            List<Person> people = OnePersonWithFish;

            var result = sut.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Dog);

            Assert.Equal(EmptyGroup(), result);
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenSpecifiedPetTypeIsUnknown_ShouldThrowException()
        {
            List<Person> people = OnePersonWithFish;

            Assert.Throws<ArgumentException>(() => sut.GroupPetNamesByOwnersGenderForSpecifiedPetType(people, PetType.Unknown));
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenPeopleAreNotSpecified_ShouldThrowException()
        {
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenMultiplePetNamesForAGender_ShouldBeReturnedInAlphabeticalOrder()
        {
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenPeopleHaveMultiplePetTypes_ShouldOnlyReturnNamesForSpecifiedPetType()
        {
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenHavePeopleOfDifferentGendersButOneGenderHasNoPetOfPetType_ShouldOnlyReturnGroupingForGenderThatHasPetType()
        {
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_WhenPeopleIn2GenderGroupsOwnAPetWithTheSameName_ShouldHavePetNameInBothGroups()
        {
        }

        [Fact]
        public void GroupPetNamesByOwnersGenderForSpecifiedPetType_When2DifferentPeopleOfTheSameGenderOwnAPetWithTheSameName_ShouldHavePetNameAppearingOnlyOnceInGroupForThatGender()
        {
        }

        #region Setup Test Data
        private static List<Person> PeopleWithCats =>
                new List<Person> {
                    new Person("M", new List<Pet> { new Pet("rusty", PetType.Cat) }),
                    new Person("F", new List<Pet> { new Pet("Hulk", PetType.Cat) }),
                    new Person("M", new List<Pet> { new Pet("Cruz", PetType.Cat), new Pet("Ellie", PetType.Cat) })
                };


        private static List<Person> OnePersonWithFish =>
                new List<Person> { new Person("M", new List<Pet> { new Pet("rusty", PetType.Fish) }) };

        private static List<Person> PersonWithNoPet =>
                new List<Person> { new Person("F", null) };


        private static IEnumerable<IGrouping<string, string>> ValidGrouping()
        {
            return new List<MockGrouping<string, string>>
                            {
                                new MockGrouping<string, string>("M", new string[] { "Cruz", "Ellie", "rusty" }),
                                new MockGrouping<string, string>("F", new string[] { "Hulk" })
                            };
        }

        private static IEnumerable<IGrouping<string, string>> EmptyGroup()
        {
            var petNames = new List<string>();
            return petNames.GroupBy(o => "");
        }
        #endregion
    }
}