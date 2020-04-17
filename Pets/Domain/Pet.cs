using System;
using System.Text;
using Newtonsoft.Json;

namespace Pets.Domain
{
    public class Pet
    {
        [JsonConstructor]
        public Pet(string name, PetType petType)
        {
            Name = name;
            PetType = petType;
        }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty("type", Required = Required.Always)]
        public PetType PetType { get; set; }

        public bool IsCat => PetType == PetType.Cat;
    }
}