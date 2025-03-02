using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController: ControllerBase 
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        //[HttpGet]
        //public async Task<ActionResult<ResponseDto<List<CountryDtos>>>> GetList()
        //{
        //    var response = await _countriesService.GetListAsync();

        //    return StatusCode(response.StatusCode, new
        //    {
        //        response.Status,
        //        response.Message,
        //        response.Data
        //    });
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ResponseDto<List<CountryDtos>>>> GetOne(Guid id)
        //{
        //    var response = await _countriesService.GetOneByIdAsync();

        //    return StatusCode(response.StatusCode, new
        //    {
        //        response.Status,
        //        response.Message,
        //        response.Data
        //    });
        //}

        //Obtener lista
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CountryDtos>>>> GetList()
        {
            var response = await _countriesService.GetListAsync();

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        //Obtener uno
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<CountryDtos>>> GetOne(Guid id)
        {
            var response = await _countriesService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }


        //Crear
        [HttpPost]
        public async Task<ActionResult<ResponseDto<CountryActionResponseDto>>> Post([FromBody] CountryCreateDto dto)
        {
            var response = await _countriesService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        //Editar
        [HttpPut("{id}")] // Put porque se edita todo
        public async Task<ActionResult<ResponseDto<CountryActionResponseDto>>> Edit([FromBody] CountryEditDto dto, Guid id)
        {
            var response = await _countriesService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, response);
        }

        //Eliminar
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<CountryActionResponseDto>>> Delete(Guid id)
        {
            var response = await _countriesService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }

    }
}
