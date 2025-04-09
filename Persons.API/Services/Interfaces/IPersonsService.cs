using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;

namespace Persons.API.Services.Interfaces
{
    public interface IPersonsService // colocar la interface en vez de clase 
    {
        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto person); //Esto vendra desde la clase 
        // PersonService y lo haremos con el ctrl mas punto  
        Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonsEditDto dto, string id);
        Task<ResponseDto<PaginationDto<List<PersonDtos>>>> GetListAsync(
            //3 de marzo
            string searchTerm = "",
            int page = 1, 
            int pageSize = 0
            );
        Task<ResponseDto<PersonDtos>> GetOneByIdAsync(string id);
    }
}
