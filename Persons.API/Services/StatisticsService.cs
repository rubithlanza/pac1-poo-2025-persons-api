using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class StatisticsService : IStatisticsService
    {
        public readonly PersonsDbContext _context;
        public StatisticsService(PersonsDbContext context)
        {
            _context = context;

        }

        public async Task<ResponseDto<StatisticsDtos>> GetCounts()
        {
            var statistics = new StatisticsDtos();

            statistics.CountriesCount = await _context.countries.CountAsync();

            statistics.PersonsCount = await _context.Persons.CountAsync();

            return new ResponseDto<StatisticsDtos> 
            { 
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Datos obtenidos correctamente",
                Data = statistics
            };
        }
    }
}
