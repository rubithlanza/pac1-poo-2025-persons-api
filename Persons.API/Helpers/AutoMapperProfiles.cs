using AutoMapper;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Countries;
using Persons.API.Dtos.Persons;

namespace Persons.API.Helpers
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles()
        {
            //para mappear los datos 
            CreateMap<PersonEntity, PersonDtos>(); //Que todas las propiedades que se llamen entre igual que se llamen 
            //solas 
            CreateMap<PersonEntity, PersonActionResponseDto>();
            CreateMap<PersonCreateDto, PersonEntity>();
            CreateMap<PersonsEditDto, PersonEntity>();


            CreateMap<CountryEntity, CountryDtos>();
            CreateMap<CountryEntity, CountryActionResponseDto>();
            CreateMap<CountryCreateDto, CountryEntity>();
            CreateMap<CountryEditDto, CountryEntity>();

            CreateMap<FamilyMemberCreateDto, FamilyMemberEntity>().ReverseMap();
            //CreateMap<FamilyMemberEntity, FamilyMemberCreateDto>();
            //Para que vaya en ambos sentidos usaremos la opcion de reversa 


        }
    }
}
