using AutoMapper;
using TrainingApp.Application.Dtos;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.Response;

namespace TrainingApp.Application.Services
{
    public class RegionService
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionService(IRegionRepository regionRepository,
            IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Guid>> AddRegion(RegionDto regionDto)
        {
            var region = _mapper.Map<Region>(regionDto);

            var result = await _regionRepository.AddRegionAsync(region);

            return ResponseHelper<Guid>.GetResponse(result, true);
        }
    }
}
