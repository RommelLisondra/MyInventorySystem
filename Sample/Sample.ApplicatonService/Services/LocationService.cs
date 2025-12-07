using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.ApplicationService.ServiceContract;
using Sample.ApplicationService.DTOs;
using Sample.Common.Logger;
using Sample.Infrastructure;

namespace Sample.ApplicationService.Services
{
    public class LocationService : ILocationService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(LocationService));

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDto>> GetAll()
        {
            try
            {
                var location = await _unitOfWork.LocationRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<Domain.Entities.Location>, IEnumerable<LocationDto>>(location);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAll", ex);
                throw;
            }
        }

        public async Task<PagedResponse<IEnumerable<LocationDto>>> GetAllPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.LocationRepository.GetAllAsync(pageNumber, pageSize);

                var dtoData = _mapper.Map<IEnumerable<LocationDto>>(result.Data);

                return new PagedResponse<IEnumerable<LocationDto>>
                {
                    Data = dtoData,
                    PageNumber = result.PageNumber,
                    PageSize = result.PageSize,
                    TotalRecords = result.TotalRecords,
                    TotalPages = result.TotalPages
                };
            }
            catch (Exception ex)
            {
                logCentral.Error("GetAllPaged", ex);
                throw;
            }
        }

        public async Task<LocationDto> GetById(int id)
        {
            try
            {
                return _mapper.Map<Domain.Entities.Location, LocationDto>(await _unitOfWork.LocationRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("GetById", ex);
                throw;
            }
        }

        public async Task CreateLocation(LocationDto locationDto, string createdBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var location = Domain.Entities.Location.Create(
                    _mapper.Map<LocationDto, Domain.Entities.Location>(locationDto), createdBy);

                await _unitOfWork.LocationRepository.AddAsync(location);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("CreateLocation", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task UpdateLocation(LocationDto locationDto, string editedBy)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var location = Domain.Entities.Location.Update(
                    _mapper.Map<LocationDto, Domain.Entities.Location>(locationDto), editedBy);

                await _unitOfWork.LocationRepository.UpdateAsync(location);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                logCentral.Error("UpdateLocation", ex);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<LocationDto>> GetLocationsByLocationCodeAsync(string locationCode)
        {
            try
            {
                var locations = await _unitOfWork.LocationRepository.FindAsync(e => e.LocationCode == locationCode);
                return _mapper.Map<IEnumerable<LocationDto>>(locations);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetLocationsByLocationCodeAsync", ex);
                throw;
            }
        }

        public async Task<LocationDto?> GetLocationByIdAsync(int id)
        {
            try
            {
                var locations = await _unitOfWork.LocationRepository.FindAsync(e => e.id == id);
                var location = locations.FirstOrDefault();
                return location == null ? null : _mapper.Map<LocationDto>(location);
            }
            catch (Exception ex)
            {
                logCentral.Error("GetLocationByIdAsync", ex);
                throw;
            }
        }

        public async Task<IEnumerable<LocationDto>> SearchLocationsAsync(string? Name)
        {
            try
            {
                // Normalize null values
                Name ??= string.Empty;

                // Perform case-insensitive search
                var location = await _unitOfWork.LocationRepository.FindAsync(e =>
                    (string.IsNullOrEmpty(Name) || EF.Functions.Like(e.Name ?? string.Empty, $"%{Name}%"))
                );

                return _mapper.Map<IEnumerable<LocationDto>>(location);
            }
            catch (Exception ex)
            {
                logCentral.Error("SearchLocationsAsync", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
