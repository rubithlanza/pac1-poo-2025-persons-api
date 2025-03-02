using Microsoft.AspNetCore.Mvc;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/persons")] //siempre va api y desde el nombre y siempre en minuscula
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PersonDtos>>>> GetList()
        {
            var response = await _personsService.GetListAsync();
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data

            });
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PersonDtos>>>GetOne(Guid id)
        {
            var response = await _personsService.GetOneByIdAsync(id);
            //En el caso que no este el get o otro es por que no esta en el interface 

            return StatusCode(response.StatusCode, response);
        }


        [HttpPost]
        public async Task<ActionResult<ResponseDto<PersonEntity>>> Post([FromBody]PersonCreateDto dto)
        {
            var response = await _personsService.CreateAsync (dto);

                return StatusCode(response.StatusCode, new 
                {
                    response.Status,
                    response.Message,
                    response.Data
                });
        }

        //public async Task<ActionResult<ResponseDto<PersonDtos>>> EditAsync (PersonsEditDto dto, Guid id)
        //{
        //    var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

        //    if(personEntity == null)
        //    {
        //        return new ActionResult<ResponseDto<PersonDtos>>
        //        {
        //            StatusCode = HttpStatusCode.NOT_FOUND,
        //            status = false,
        //            Message = "Registro no encontrado"
        //        };
        //    }

        //} Los datos son corregidos el dia 25 de febrero del edit 

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Edit([FromBody] PersonsEditDto dto, Guid id)
        {
            var response = await _personsService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Delete (Guid id)
        {
            var response = await _personsService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }




    }
}

