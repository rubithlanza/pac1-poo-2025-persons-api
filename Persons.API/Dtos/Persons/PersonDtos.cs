using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Persons
{
    public class PersonDtos
    {
      
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string DNI { get; set; }

       
        public string Gender { get; set; }

        public string CountryId { get; set; }

        //11 de marzo
        //Lo primero que vamos hacer para hacer la edicion
        public List<FamilyMemberCreateDto> FamilyGroup { get; set; }

    }
}
