using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos;
using TrainingApp.Application.Services;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.RequestModel;

namespace TrainingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        private readonly RegionService _regionService;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionService = new RegionService(regionRepository, mapper);
        }

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] ApiRequest<RegionDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = _regionService.AddRegion(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<Guid>.GetResponse(true, ex.Message));
            }
        }
    }
}
