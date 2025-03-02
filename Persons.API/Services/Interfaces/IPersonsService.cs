using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;

namespace Persons.API.Services.Interfaces
{
    public interface IPersonsService // colocar la interface en vez de clase 
    {
        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto person); //Esto vendra desde la clase 
        // PersonService y lo haremos con el ctrl mas punto  
        Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(Guid id);
        Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonsEditDto dto, Guid id);
        Task<ResponseDto<List<PersonDtos>>> GetListAsync();
        Task<ResponseDto<PersonDtos>> GetOneByIdAsync(Guid id);
    }
}
