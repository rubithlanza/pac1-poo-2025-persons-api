using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{

    [ApiController]
    [Route("api/statistics")]

    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService) 
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("counts")]
        public async Task<ActionResult<ResponseDto<StatisticsDtos>>> GetCounts()
        {
            var response = await _statisticsService.GetCounts();

            return StatusCode(response.StatusCode, new ResponseDto<StatisticsDtos>
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }
    }
}
