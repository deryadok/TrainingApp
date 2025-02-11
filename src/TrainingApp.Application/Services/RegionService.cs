using AutoMapper;
using TrainingApp.Application.Dtos.Region;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Constants;
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

        public async Task<ApiResponse<Guid>> Add(RegionInsertDto regionDto)
        {
            var region = _mapper.Map<Region>(regionDto);

            var result = await _regionRepository.AddRegionAsync(region);

            return ResponseHelper<Guid>.GetResponse(result, true);
        }

        public async Task<ApiResponse<bool>> Update(RegionUpdateDto regionDto)
        {
            var region = _mapper.Map<Region>(regionDto);

            var result = await _regionRepository.UpdateRegionAsync(region);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.UpdatedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> Delete(Guid regionId)
        {
            var result = await _regionRepository.DeleteRegionAsync(regionId);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.DeletedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<RegionDto>> GetRegionById(Guid id)
        {
            var returnModel = new RegionDto();

            var region = await _regionRepository.GetRegionByIdAsync(id);

            returnModel = _mapper.Map<RegionDto>(region);

            bool success = region != null;

            return ResponseHelper<RegionDto>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }


        public async Task<ApiResponse<IEnumerable<RegionDto>>> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();

            var returnModel = _mapper.Map<IEnumerable<RegionDto>>(regions);

            bool success = returnModel != null && returnModel.Any();

            return ResponseHelper<IEnumerable<RegionDto>>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }
    }
}