using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Services.Interfaces;
using Persons.API.Constants;
using Persons.API.Dtos.Persons;
using Microsoft.EntityFrameworkCore;
using Persons.API.Dtos.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
//12-02-2025
namespace Persons.API.Services
{
    public class PersonsService : IPersonsService //Nos colococamos y le daremos un click y punto para colocar los demas datas
        // interface 
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;

        public PersonsService(PersonsDbContext context, IMapper mapper)
            //Sistema de inyeccion de dependencia para que esten conf para nosotros 
        {
           _context = context;
           _mapper = mapper;
        }
        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto) //Me saldra un error y darle click en el foco y la primera opcion o ctrl + punto
        { // Cuando vamos agregar async es por que es asincrono, o por lo menos algo lo sera 

            //var personEntity = new PersonEntity
            //{
            //    Id = Guid.NewGuid(),
            //    FirstName = dto.FirstName,
            //    LastName = dto.LastName,
            //    DNI = dto.DNI,
            //    Gender = dto.Gender,
            //};

            var personEntity = _mapper.Map<PersonEntity>(dto);

            //27 de febrero
            var countryEntity = await _context.countries.FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (countryEntity == null) 
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais no existe"
                };
            }
            _context.Persons.Add(personEntity);
            await _context.SaveChangesAsync(); //Para cambiar los datos 

            //var  response = new PersonActionResponseDto
            //{
            //    Id = personEntity.Id,
            //    FirstName = personEntity.FirstName,
            //    LastName = personEntity.LastName,

            //};

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro Creado Correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };
        }

        public async Task<ResponseDto<List<PersonDtos>>> GetListAsync()
        {
            var personEntity = await _context.Persons.ToListAsync();
           // var personsDto = new List<PersonDtos>();
           var personsDto = _mapper.Map<List<PersonDtos>>(personEntity);    

            //foreach (var person in personEntity)
            //{
            //    personsDto.Add(new PersonDtos
            //    {
            //        Id = person.Id,
            //        FirstName = person.FirstName,
            //        LastName = person.LastName,
            //        DNI= person.DNI,
            //        Gender = person.Gender,
            //    });
            //}

            return new ResponseDto<List<PersonDtos>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message =personEntity.Count() > 0 ? "Registros encontrados" : "No se encontro el registro", 
                Data = personsDto
            };
        }

        public async Task<ResponseDto<PersonDtos>>GetOneByIdAsync(Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(person => person.Id == id);
            //En esta opcion sera el primero que encunetre o un valor por defecto


            if (personEntity is null)
            {
                return new ResponseDto<PersonDtos>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false, // Cuando hubo un error
                    Message = "Registro no encontrado!"
                };
            }//En caso que no encontremos nada, tendremos un valor por defecto


            //Si encontramos algo 
            return new ResponseDto<PersonDtos>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",

                //Vamos a hacer un proceso en el cual es algo manual y no es una buena practica 
                //Data = new PersonDtos
                //{
                //    Id = personEntity.Id,
                //    FirstName = personEntity.FirstName,
                //    LastName = personEntity.LastName,
                //    DNI = personEntity.DNI,
                //    Gender = personEntity.Gender,
                //}

                //Nueva forma
                Data = _mapper.Map<PersonDtos>(personEntity)
            };

        }

        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonsEditDto dto, Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }
         
            //27 de febrero
            var countryEntity = await _context.countries.FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (countryEntity == null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais no existe"
                };
            }

            //_mapper.Map<PersonsEditDto, PersonEntity>(dto, personEntity);
            _mapper.Map<PersonsEditDto, PersonEntity>(dto, personEntity);

            _context.Persons.Update(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado corretamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };

        }

        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(
            Guid id)
        {
            var personEntity = await _context.Persons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };

        }
    }
}

