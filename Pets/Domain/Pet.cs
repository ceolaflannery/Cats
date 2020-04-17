using System;
using System.Text;

namespace Pets.Domain
{
    public class Pet
    {
        public Pet(string name, PetType petType)
        {
            if (!IsValid(name, petType))
                throw new Exception(GetErrors(name, petType));

            Name = name;
            PetType = petType;
        }

        public string Name { get; set; }
        public PetType PetType { get; set; }

        public bool IsCat => PetType == PetType.Cat;

        private bool IsValid(string name, PetType petType)
        {
            return string.IsNullOrWhiteSpace(GetErrors(name, petType));
        }

        private string GetErrors(string name, PetType petType)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(name))
                errors.AppendLine("Pets need a name");
            if (petType == PetType.Unknown)
                errors.AppendLine("Pets need a type specified");

            return errors.ToString();
        }
    }
}