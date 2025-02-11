using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos.Region;
using TrainingApp.Application.Services;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.RequestModel;

namespace TrainingApp.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Insert([FromBody] ApiRequest<RegionInsertDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _regionService.Add(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<Guid>.GetResponse(true, ex.Message));
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ApiRequest<RegionUpdateDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _regionService.Update(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] ApiRequest<Guid> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _regionService.Delete(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetRegionById")]
        public async Task<IActionResult> GetRegionById([FromQuery] Guid userId)
        {
            try
            {
                var response = await _regionService.GetRegionById(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            try
            {
                var response = await _regionService.GetAllRegions();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }
    }
}
