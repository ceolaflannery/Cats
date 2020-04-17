using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Pets.Domain
{
    public class Person
    {
        [JsonConstructor]
        public Person(string gender, List<Pet> pets)
        {
            Gender = gender;
            Pets = pets;
        }

        [JsonProperty("gender", Required = Required.Always)]
        public string Gender { get; set; }

        [JsonProperty("pets", Required = Required.AllowNull)]
        public List<Pet> Pets { get; set; }

        public bool HasPets => Pets != null && Pets.Count > 0;
    }
}
