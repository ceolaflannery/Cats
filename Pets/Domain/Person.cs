using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.Domain
{
    public class Person
    {
        public Person(string gender, List<Pet> pets)
        {
            if (!IsValid(gender))
                throw new Exception(GetErrors(gender));

            Gender = gender;
            Pets = pets;
        }

        public string Gender { get; set; }
        public List<Pet> Pets { get; set; }

        public bool HasPets => Pets != null && Pets.Count > 0;

        private bool IsValid(string gender)
        {
            return string.IsNullOrWhiteSpace(GetErrors(gender));
        }

        private string GetErrors(string gender)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(gender))
                errors.AppendLine("Gender needs to be specified for owner");

            return errors.ToString();
        }
    }
}
