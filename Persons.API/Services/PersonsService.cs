using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Services.Interfaces;
using Persons.API.Constants;
using Persons.API.Dtos.Persons;
using Microsoft.EntityFrameworkCore;
using Persons.API.Dtos.Common;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
//12-02-2025
namespace Persons.API.Services
{
    public class PersonsService : IPersonsService //Nos colococamos y le daremos un click y punto para colocar los demas datas
        // interface 
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;
        //Colocado el 3 de marzo
        private readonly int PAGE_SIZE; //Colocamos solo el el nombre por que con el numero no se puede cambiar 
        private readonly int PaGE_SIZE_LIMIT;

        public PersonsService(PersonsDbContext context, IMapper mapper, IConfiguration configuration)
            //Sistema de inyeccion de dependencia para que esten conf para nosotros 
        {
           _context = context;
           _mapper = mapper;
            //Colocado el 3 de marzo es para la configuracion de pagina 
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PaGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");

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

            //6 de marzo

            using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    //se paso el 6 de marzo era lo misma pero solo lo pasos ahi 
                    var personEntity = _mapper.Map<PersonEntity>(dto);

                    //4 de abril
                    personEntity.Id = Guid.NewGuid().ToString();

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

                    //Para obtener el grupo familiar
                    if (dto.Family != null && dto.Family.Count() > 1) 
                    {
                        var familyGroup = _mapper.Map<List<FamilyMemberEntity>>(dto.Family);
                        familyGroup = familyGroup.Select(m => new FamilyMemberEntity 
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            PersonId = personEntity.Id,
                            RelationShip = m.RelationShip,
                        }).ToList();
                        _context.FamilyGroup.AddRange(familyGroup);
                        await _context.SaveChangesAsync();

                    }
                    

                    transaction.Commit(); //Guarda todos los cambios de la base de datos

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
                catch (Exception e) //Error que nosotros no controlamos y con sqlexcepcion es para algo mas especifico
                {
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                    return new ResponseDto<PersonActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.INTERNAL_SERVER_ERROR,
                        Status = false,
                        Message = "Error Interno en el servidor, contacte al admin",
                    }; 
                }

            }

            
        }

        public async Task<ResponseDto<PaginationDto<List<PersonDtos>>>> GetListAsync(
            string searchTerm = "",
            int page = 1,
            int pageSize = 0
            )
        {
            //3 de marzo
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;
            int startIndex = (page - 1) * pageSize; //Nos va servir para definir el index de la paginacion 

            //Nos va servir para que ponamos hacer varias operaciones antes de la consulta
            IQueryable<PersonEntity> personQuery = _context.Persons;

            if (!string.IsNullOrEmpty(searchTerm)) //Si el termino de busca es diferente de nulo 
            {
                personQuery = personQuery.Where(x => (x.DNI + " " + x.FirstName + " " + x.LastName).Contains(searchTerm)); // Cuando venga entonces el valor no esta vacio y esto buscara un solo campo si no coloco parentesis 
            }

            int totalRows = await personQuery.CountAsync(); //Solo se va basar con los que encuentre ahi 

            var personsEntity = await personQuery.OrderBy(x => x.LastName).Skip(startIndex).Take(pageSize).ToListAsync();

           // var personEntity = await _context.Persons.ToListAsync(); comentada el 3 de marzo 
           // var personsDto = new List<PersonDtos>();
           var personsDto = _mapper.Map<List<PersonDtos>>(personsEntity);    

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

            return new ResponseDto<PaginationDto<List<PersonDtos>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message =personsEntity.Count() > 0 ? "Registros encontrados" : "No se encontro el registro", 
                Data = new PaginationDto<List<PersonDtos>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = personsDto,
                    HasNextPage = startIndex + pageSize < PaGE_SIZE_LIMIT && page < (int)Math
                    .Ceiling((double)(totalRows / pageSize)),
                    HasPreviousPage = page > 1,
                }
            };
        }

        public async Task<ResponseDto<PersonDtos>>GetOneByIdAsync(string id)
        {
            var personEntity = await _context.Persons
                //11 de Marzo
                .Include(x => x.FamilyGroup)
                .FirstOrDefaultAsync(person => person.Id == id);
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

        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonsEditDto dto, string id)
        {
            //12 de marzo
            using (var transaction= await _context.Database.BeginTransactionAsync()) 
            {
                try
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

                    //12 de marzo para el family group
                    if (dto.Family !=  null && dto.Family.Count > 0 )
                    {
                        var oldFamilyGroup = await _context.FamilyGroup.Where(fg => fg.PersonId == id).ToListAsync();

                        if (oldFamilyGroup.Count > 0)
                        {
                            _context.RemoveRange(oldFamilyGroup); //Para eliminar una lista del registro
                            await _context.SaveChangesAsync();
                        }

                        var newFamilyGroup = dto.Family /*_mapper.Map<List<FamilyMemberEntity>> Para que no usar doble codigo*/
                            .Select(fg => new FamilyMemberEntity
                            {
                                Id = Guid.NewGuid().ToString(),
                                 FirstName = fg.FirstName,
                                 LastName = fg.LastName,
                                 PersonId = id, 
                                 RelationShip = fg.RelationShip,

                            }).ToList();
                        _context.AddRange(newFamilyGroup);
                        await _context.SaveChangesAsync();
                    }

                   await transaction.CommitAsync();

                    return new ResponseDto<PersonActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = true,
                        Message = "Registro editado corretamente",
                        Data = _mapper.Map<PersonActionResponseDto>(personEntity)
                    };
                }
                catch (Exception) 
                {
                    await transaction.RollbackAsync(); //Para que cuando encuentre un error que se vuelva a la base de datos 
                    return new ResponseDto<PersonActionResponseDto>
                    {
                        StatusCode = HttpStatusCode.INTERNAL_SERVER_ERROR,
                        Status = false,
                        Message = "Se produjo un error al editar el registro"
                    };
                }
            }
          

        }

        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(
            string id)
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

