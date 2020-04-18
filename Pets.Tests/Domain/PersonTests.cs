using System;
using Pets.Domain;
using Xunit;

namespace Pets.Tests.Domain
{
    public class PersonTests
    {
        private Person _sut;

        public PersonTests()
        {
            _sut = new Person("", null);
        }

        [Fact]
        public void HasPets_WhenPetsIsNull_ShouldReturnFalse()
        {
        }

        [Fact]
        public void HasPets_WhenPetsIsAnEmptyList_ShouldReturnFalse()
        {
        }

        [Fact]
        public void HasPets_WhenOnePet_ShouldReturnTrue()
        {
        }

        [Fact]
        public void HasPets_WhenMultiplePets_ShouldReturnTrue()
        {
        }
    }
}
