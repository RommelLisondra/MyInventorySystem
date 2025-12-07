using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.ApplicationService.ServiceContract
{
    public interface ILocationService : IDisposable
    {
        Task<IEnumerable<LocationDto>> GetAll();
        Task<LocationDto> GetById(int id);
        Task CreateLocation(LocationDto LocationDto, string createdBy);
        Task UpdateLocation(LocationDto LocationDto, string editedBy);
        Task<IEnumerable<LocationDto>> GetLocationsByLocationCodeAsync(string locationCode);
        Task<LocationDto?> GetLocationByIdAsync(int id);
        Task<IEnumerable<LocationDto>> SearchLocationsAsync(string? Name);
        Task<PagedResponse<IEnumerable<LocationDto>>> GetAllPaged(int pageNumber, int pageSize);
    }
}
